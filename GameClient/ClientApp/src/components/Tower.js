import React, { Component } from 'react';

export default class Tower extends Component {

  render () {
    return (
        <div className="PlayerSpace__GameMenu__Tower">
            <img onClick={() => this.props.click(this.props.name)} src={process.env.PUBLIC_URL + "/images/towers/" + this.props.name + ".png"} alt="" />
        </div>
    );
  }
}
