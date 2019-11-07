/* eslint-disable jsx-a11y/accessible-emoji */
import React, { Component } from 'react';
import { HubConnectionBuilder } from '@aspnet/signalr';
import MinionHandler from '../MinionHandler';
import TowerHandler from '../TowerHandler';
import { distance, distanceToLineSegment } from '../utils';
import Tower from './Tower';
import TowerUpgrades from './TowerUpgrades';

export class Game extends Component {

  minTowerDistance = 45;
  minRoadDistance = 25;

  towerTypes = ["soldier", "archer"];

  gameMap = undefined;

  playerCanvas = undefined;
  playerContext = undefined;
  opponentCanvas = undefined;
  opponentContext = undefined;

  minionHandler = undefined;
  towerHandler = undefined;

  name = '';
  player = {};
  opponent = {};

  constructor(props) {
    super(props);

    this.state = {
      hubConnection: null,

      started: false,

      wave: 1,

      selectedTower: undefined,
      upgradingTower: undefined,
      placeable: undefined,
      mouseX: 0,
      mouseY: 0,
    }

    this.selectTower = this.selectTower.bind(this);
    this.handleCanvasMouseMove = this.handleCanvasMouseMove.bind(this);
    this.handleCanvasMouseLeave = this.handleCanvasMouseLeave.bind(this);
    this.handleCanvasClick = this.handleCanvasClick.bind(this);
    this.changeAttackMode = this.changeAttackMode.bind(this);
  }

  componentDidMount() {
    this.name = window.sessionStorage.getItem("name");
    if (!this.name) {
      this.props.history.push('/');
      return;
    }

    const hubConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:62984/game")
      .build();

    this.setState({ hubConnection }, () => {
      this.state.hubConnection
        .start()
        .then(() => {
          console.log('Connection started!')
          this.state.hubConnection
            .invoke('changeName', this.name)
            .catch(err => console.error(err));

          this.state.hubConnection
            .invoke('askForMap')
            .catch(err => console.error(err));
        })
        .catch(err => console.log('Error while establishing connection :('));

      this.state.hubConnection.on('gameFull', () => {
        sessionStorage.removeItem('name');
        this.props.history.push('/');
      });

      this.state.hubConnection.on('getConnectionId', (id) => {
        this.id = id;
      });

      this.state.hubConnection.on('getMap', (map) => {
        this.gameMap = map;
      });

      this.state.hubConnection.on('gameStarting', () => {
        this.setState({ started: true }, () => this.startGame());
      });

      this.state.hubConnection.on('gameStopping', () => {
        this.setState({ started: false });
      });

      this.state.hubConnection.on('spawnMinion', (id, type) => {
        this.minionHandler.spawn(id, type);
      });

      this.state.hubConnection.on('tick', (wave, gameState) => {
        this.setState({ wave });
        this.player = gameState.filter(user => user.id === this.id)[0];
        this.opponent = gameState.filter(user => user.id !== this.id)[0];
        if (this.player && this.opponent) {
          this.tick();
        }
      });
    });
  }

  startGame() {
    this.playerCanvas = document.querySelector('.playerCanvas');
    this.playerContext = this.playerCanvas.getContext('2d');
    this.opponentCanvas = document.querySelector('.opponentCanvas');
    this.opponentContext = this.opponentCanvas.getContext('2d');

    this.minionHandler = new MinionHandler();
    this.towerHandler = new TowerHandler(this.towerTypes);
  }

  tick() {
    if (!this.playerContext) {
      this.startGame();
    }
    this.handleState(this.playerCanvas, this.playerContext, this.player);
    this.handleState(this.opponentCanvas, this.opponentContext, this.opponent);

    if (this.state.selectedTower) {
      this.setState({ placeable: this.isPlaceable() }, () => {
        this.towerHandler.selectTower(this.playerContext, this.state.selectedTower, this.state.mouseX, this.state.mouseY, this.state.placeable);
      })
    }

    if (this.state.upgradingTower) {
      var upgradingTower = this.player.towers.filter(tower => tower.id === this.state.upgradingTower.id)[0];
      this.setState({ upgradingTower });
    }
  }

  handleState(canvas, context, state) {
    context.clearRect(0, 0, canvas.width, canvas.height);
    this.minionHandler.render(context, state.minions);
    this.towerHandler.render(context, state.towers);
  }

  selectTower(selectedTower) {
    if (this.state.selectedTower === selectedTower) {
      this.setState({ selectedTower: undefined });
    } else {
      this.setState({ selectedTower });
    }
  }

  isPlaceable() {
    var placeable = true;
    this.player.towers.forEach(tower => {
      if (distance(this.state.mouseX, this.state.mouseY, tower.position.x, tower.position.y) < this.minTowerDistance) {
        placeable = false;
      }
    })
    if (!placeable) {
      return false;
    }
    for (var i = 0; i < this.gameMap.length - 1; i++) {
      var pointA = this.gameMap[i];
      var pointB = this.gameMap[i + 1];
      if (distanceToLineSegment(this.state.mouseX, this.state.mouseY, pointA.x, pointA.y, pointB.x, pointB.y) < this.minRoadDistance) {
        return false;
      }
    }
    return true;
  }

  handleCanvasClick(event) {
    if (this.state.selectedTower && this.state.placeable) {
      this.state.hubConnection
        .invoke('placeTower', this.name, this.state.selectedTower, event.nativeEvent.offsetX, event.nativeEvent.offsetY)
        .catch(err => console.error(err));
    }

    if (this.state.upgradingTower) {
      this.setState({ upgradingTower: null })
    }

    if (!this.state.selectedTower) {
      var clickedTower = this.towerHandler.getClickedTower(event.nativeEvent.offsetX, event.nativeEvent.offsetY, this.player.towers);
      if (clickedTower) {
        console.log(clickedTower)
        this.setState({ upgradingTower: clickedTower })
      }
    }
  }

  handleCanvasMouseMove(event) {
    if (this.state.selectedTower) {
      this.setState({ mouseX: event.nativeEvent.offsetX, mouseY: event.nativeEvent.offsetY })
    }
  }

  handleCanvasMouseLeave() {
    this.setState({ selectedTower: undefined });
  }

  changeAttackMode(mode) {
    this.state.hubConnection
      .invoke('changeAttackMode', this.name, this.state.upgradingTower.id, mode)
      .catch(err => console.error(err));
  }

  render() {
    var towers = this.towerTypes.map(type =>
      <Tower name={type} click={this.selectTower} key={type} />
    );
    return (
      <div>
        {!this.state.started && <h1 className="mb-1">Waiting for players..</h1>}
        {this.state.started &&
          <div>
            <h1 className="mb-1">Wave {this.state.wave}</h1>
            <div className="canvases">
              <div className="PlayerSpace">
                <p>{this.player.health}❤️   {this.player.money}💰</p>
                <canvas onClick={this.handleCanvasClick} onMouseMove={this.handleCanvasMouseMove} onMouseLeave={this.handleCanvasMouseLeave} className="playerCanvas" width="600" height="400" style={{ border: '1px solid #000000' }}></canvas>
                <div className="PlayerSpace__GameMenu">
                  {this.state.upgradingTower ? <TowerUpgrades tower={this.state.upgradingTower} changeAttackMode={this.changeAttackMode} /> : towers}
                </div>
              </div>
              <div>
                <p>{this.opponent.health}❤️</p>
                <canvas className="opponentCanvas" width="600" height="400" style={{ border: '1px solid #000000' }}></canvas>
              </div>
            </div>
          </div>
        }
      </div>
    );
  }
}
