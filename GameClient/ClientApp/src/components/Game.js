/* eslint-disable jsx-a11y/accessible-emoji */
import React, { Component } from 'react';
import { HubConnectionBuilder } from '@aspnet/signalr';
import MinionHandler from '../MinionHandler';
import TowerHandler from '../TowerHandler';

export class Game extends Component {

  halfTowerHeight = 17.5;
  halfTowerWidth = 20;

  playerCanvas = undefined;
  playerContext = undefined;
  opponentCanvas = undefined;
  opponentContext = undefined;

  minionHandler = undefined;
  towerHandler = undefined;

  constructor(props) {
    super(props);

    this.state = {
      hubConnection: null,

      name: '',
      started: false,

      wave: 0,
      player: {},
      opponent: {},

      selectedTower: undefined,
      mouseX: 0,
      mouseY: 0,
    }

    this.selectTower = this.selectTower.bind(this);
    this.handleCanvasMouseMove = this.handleCanvasMouseMove.bind(this);
    this.handleCanvasMouseLeave = this.handleCanvasMouseLeave.bind(this);
    this.handleCanvasClick = this.handleCanvasClick.bind(this);
  }

  componentDidMount() {
    const name = window.sessionStorage.getItem("name");
    if (!name) {
      this.props.history.push('/');
      return;
    }

    const hubConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:62984/game")
      .build();

    this.setState({ hubConnection, name }, () => {
      this.state.hubConnection
        .start()
        .then(() => {
          console.log('Connection started!')
          this.state.hubConnection
            .invoke('changeName', this.state.name)
            .catch(err => console.error(err));
        })
        .catch(err => console.log('Error while establishing connection :('));

      this.state.hubConnection.on('gameStarting', () => {
        this.setState({ started: true }, () => this.startGame());
      });

      this.state.hubConnection.on('spawnMinion', (id, type) => {
        this.minionHandler.spawn(id, type);
      });

      this.state.hubConnection.on('tick', (wave, gameState) =>
        this.setState({
          wave,
          player: gameState.filter(user => user.name === this.state.name)[0],
          opponent: gameState.filter(user => user.name !== this.state.name)[0]
        }, () => this.tick()));
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
    this.handleState(this.playerCanvas, this.playerContext, this.state.player);
    this.handleState(this.opponentCanvas, this.opponentContext, this.state.opponent);

    if (this.state.selectedTower) {
      this.towerHandler.selectTower(this.playerContext, this.state.mouseX, this.state.mouseY);
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

  handleCanvasClick(event) {
    if (this.state.selectedTower) {
      this.state.hubConnection
        .invoke('placeTower', this.state.name, 'archery_range', this.state.mouseX - this.halfTowerWidth, this.state.mouseY - this.halfTowerHeight)
        .catch(err => console.error(err));
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
                <p>{this.state.player.health}❤️</p>
                <canvas onClick={this.handleCanvasClick} onMouseMove={this.handleCanvasMouseMove} onMouseLeave={this.handleCanvasMouseLeave} className="playerCanvas" width="600" height="400" style={{ border: '1px solid #000000' }}></canvas>
                <div className="PlayerSpace__GameMenu">
                  <div className="PlayerSpace__GameMenu__Tower">
                    <img onClick={() => this.selectTower('archer')} src={process.env.PUBLIC_URL + "/images/towers/archery_range.png"} alt="" />
                  </div>
                </div>
              </div>
              <div>
                <p>{this.state.opponent.health}❤️</p>
                <canvas className="opponentCanvas" width="600" height="400" style={{ border: '1px solid #000000' }}></canvas>
              </div>
            </div>
          </div>
        }
      </div>
    );
  }
}
