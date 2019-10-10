import React, { Component } from 'react';
import { HubConnectionBuilder } from '@aspnet/signalr';

export class Game extends Component {

  constructor(props) {
    super(props);

    this.state = {
        hubConnection: null,
        name: ''
      }
  }

  componentDidMount() {
    const name = window.sessionStorage.getItem("name");
    if (!name) {
        this.props.history.push('/');
        return;
    }

    const hubConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:62988/game")
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
  
        this.state.hubConnection.on('sendToAll', (nick, receivedMessage) => {
          const text = `${nick}: ${receivedMessage}`;
          const messages = this.state.messages.concat([text]);
          console.log(text, messages);
          this.setState({ messages });
        });

        this.state.hubConnection.on('gameStarting', () => {
            console.log('game is starting')
          });
      });
  }

  render () {
    return (
      <div>
        <h1 className="mb-3">Waiting for players...</h1>
      </div>
    );
  }
}
