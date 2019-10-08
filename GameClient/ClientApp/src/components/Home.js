import React, { Component } from 'react';
import { Button, Form, Input } from 'reactstrap';

export class Home extends Component {

  constructor(props) {
    super(props);

    this.state = {
      name: ''
    }
  }

  componentDidMount() {
    const name = window.sessionStorage.getItem("name");
    if (name) {
        this.props.history.push('/game');
        return;
    }
  }

  onNameChange = (event) => {
    this.setState({name: event.target.value});
  }

  onSubmit = (event) => {
    event.preventDefault();
    window.sessionStorage.setItem("name", this.state.name);
    this.props.history.push('/game');
  }

  render () {
    return (
      <div>
        <h1 className="mb-3">Start playing now!</h1>
        <Form onSubmit={this.onSubmit}>
          <Input className="mb-2" type="text" onChange={this.onNameChange} placeholder="Name"/>
          <Button block>Start</Button>
        </Form>
      </div>
    );
  }
}
