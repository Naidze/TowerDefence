import React, { Component } from 'react';
import { Button } from 'reactstrap';

export default class TowerUpgrades extends Component {

  render () {
    return (
        <div className="PlayerSpace__GameMenu__">
            <Button onClick={() => this.props.changeAttackMode('closest')} className="ml-2" color="secondary">closest</Button>{' '}
            <Button onClick={() => this.props.changeAttackMode('furthest')} color="secondary">furthest</Button>{' '}
            <Button onClick={() => this.props.changeAttackMode('weakest')} color="secondary">weakest</Button>{' '}
            <Button onClick={() => this.props.changeAttackMode('strongest')} color="secondary">strongest</Button>{' '}
        </div>
    );
  }
}
