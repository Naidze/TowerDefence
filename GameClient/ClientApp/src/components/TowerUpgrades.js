import React, { Component } from 'react';
import { Button } from 'reactstrap';

export default class TowerUpgrades extends Component {

    attackModes = [
        'closest', 'furthest', 'weakest', 'strongest'
    ]

    upgrades = [
        'damage', 'rate', 'range'
    ]

    render() {
        var modes = this.attackModes.map(mode =>
            <Button key={mode}
                onClick={() => this.props.changeAttackMode(mode)} className="ml-2"
                color={this.props.tower.attackMode.name === mode ? "primary" : "secondary"}>
                {mode}
            </Button>
        )

        var upgrades = this.upgrades.map(upgrade =>
            <Button key={upgrade}
                onClick={() => this.props.upgrade(upgrade)}
                className="ml-2" color="info">
                {upgrade} ({this.props.tower.upgrades[upgrade] ? this.props.tower.upgrades[upgrade] : 0})
            </Button>
        )
        return (
            <div className="mt-2 mb-2">
                <div>
                    {modes}
                </div>
                <div className="mt-2">
                    {upgrades}
                </div>
                <div className="mt-2">
                    <Button onClick={() => this.props.sell()} className="ml-2" color="danger">
                        Sell
                    </Button>
                </div>
            </div>
        );
    }
}
