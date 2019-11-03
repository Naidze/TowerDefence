import React, { Component } from 'react';
import { HubConnectionBuilder } from '@aspnet/signalr';
import MinionHandler from '../MinionHandler';

export class Game extends Component {

  playerCanvas = undefined;
  playerContext = undefined;
  otherCanvas = undefined;
  otherContext = undefined;
  minionHandler = undefined;

  constructor(props) {
    super(props);

    this.state = {
      hubConnection: null,
      name: '',
      started: false,
      countdown: 10,
      wave: 0,
    }
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

      this.state.hubConnection.on('tick', (wave, playersState) => this.setState({ wave }, this.tick(playersState)));
    });
  }

  startGame() {
    this.playerCanvas = document.querySelector('.playerCanvas');
    this.playerContext = this.playerCanvas.getContext('2d');
    this.otherCanvas = document.querySelector('.opponentCanvas');
    this.otherContext = this.otherCanvas.getContext('2d');
    this.minionHandler = new MinionHandler();
  }

  tick(state) {
    var playerState = state.filter(user => user.name === this.state.name)[0];
    this.playerContext.clearRect(0, 0, this.playerCanvas.width, this.playerCanvas.height);
    this.minionHandler.render(this.playerContext, playerState.minions);
  }

  render() {
    return (
      <div>
        {!this.state.started && <h1 className="mb-1">Waiting for players..</h1>}
        {this.state.started &&
          <div>
            <h1 className="mb-1">Wave {this.state.wave}</h1>
            <div className="canvases">
              <canvas className="playerCanvas" width="600" height="400" style={{ border: '1px solid #000000' }}>
              </canvas>
              <canvas className="opponentCanvas" width="600" height="400" style={{ border: '1px solid #000000' }}>
              </canvas>
            </div>
          </div>
        }
      </div>
    );
  }
}
