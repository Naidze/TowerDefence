import React, { Component } from 'react';
import { Button } from 'reactstrap';

export default class TowerUpgrades extends Component {

    attackModes = [
        'closest', 'furthest', 'weakest', 'strongest'
    ]

    render() {
        var modes = this.attackModes.map(mode =>
            <Button key={mode}
                onClick={() => this.props.changeAttackMode(mode)} className="ml-2"
                color={this.props.tower.attackMode.name === mode ? "primary" : "secondary"}>
                {mode}
            </Button>
        )
        return (
            <div>
                {modes}
            </div>
        );
    }
}
