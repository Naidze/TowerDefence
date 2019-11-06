/* eslint-disable jsx-a11y/accessible-emoji */
import React, { Component } from 'react';
import { HubConnectionBuilder } from '@aspnet/signalr';
import MinionHandler from '../MinionHandler';
import TowerHandler from '../TowerHandler';
import { distance, distanceToLineSegment } from '../utils';

export class Game extends Component {

  minTowerDistance = 45;
  minRoadDistance = 25;

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
      placeable: undefined,
      mouseX: 0,
      mouseY: 0,
    }

    this.selectTower = this.selectTower.bind(this);
    this.handleCanvasMouseMove = this.handleCanvasMouseMove.bind(this);
    this.handleCanvasMouseLeave = this.handleCanvasMouseLeave.bind(this);
    this.handleCanvasClick = this.handleCanvasClick.bind(this);
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
    this.towerHandler = new TowerHandler();
  }

  tick() {
    if (!this.playerContext) {
      this.startGame();
    }
    this.handleState(this.playerCanvas, this.playerContext, this.player);
    this.handleState(this.opponentCanvas, this.opponentContext, this.opponent);

    if (this.state.selectedTower) {
      this.setState({ placeable: this.isPlaceable() }, () => {
        this.towerHandler.selectTower(this.playerContext, this.state.mouseX, this.state.mouseY, this.state.placeable);
      })
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
        .invoke('placeTower', this.name, 'archery_range', event.nativeEvent.offsetX, event.nativeEvent.offsetY)
        .catch(err => console.error(err));
    }

    if (!this.state.selectedTower) {
      console.log(this.towerHandler.getClickedTower(event.nativeEvent.offsetX, event.nativeEvent.offsetY, this.player.towers))
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

  render() {
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
                  <div className="PlayerSpace__GameMenu__Tower">
                    <img onClick={() => this.selectTower('archer')} src={process.env.PUBLIC_URL + "/images/towers/archery_range.png"} alt="" />
                  </div>
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
