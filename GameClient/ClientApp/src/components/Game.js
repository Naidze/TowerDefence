import React, { Component } from 'react';
import { HubConnectionBuilder } from '@aspnet/signalr';
import MinionHandler from '../minions/MinionHandler';

export class Game extends Component {

  minionHandler = undefined;

  constructor(props) {
    super(props);

    this.state = {
      hubConnection: null,
      name: '',
      started: false,
      countdown: 10,
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

      this.state.hubConnection.on('tick', (wut) => {
        console.log(wut)
      });
    });
  }

  startGame() {
    this.minionHandler = new MinionHandler(document.getElementById('playerCanvas'));
  }

  render() {
    return (
      <div>
        <h1 className="mb-3">{this.state.started ? (this.state.countdown > 0 ? `Game starts in ${this.state.countdown}` : 'Game in progress') : 'Waiting for players...'}</h1>
        {this.state.started &&
          <div>
            <div className="canvases">
              <canvas id="playerCanvas" width="600" height="400" style={{ border: '1px solid #000000' }}>
              </canvas>
              <canvas id="opponentCanvas" width="600" height="400" style={{ border: '1px solid #000000' }}>
              </canvas>
            </div>
          </div>
        }
      </div>
    );
  }
}
