import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Game } from './components/Game';
import MinionFactory from './flyweight/MinionFactory';

export default class App extends Component {
  static displayName = App.name;

  render() {
    // var canvas = document.createElement("CANVAS");
    // const context = canvas.getContext('2d');
    // const minionFactory = new MinionFactory();
    // var iterations = 100000;
    // console.log(window.performance.memory)

    // console.time('Flyweight');
    // for(var i = 0; i < iterations; i++ ){
    //     minionFactory.getMinion('noob').draw(context, 1, 1, 20, 20);
    // };
    // console.timeEnd('Flyweight')
    // console.log(window.performance.memory)
    
    // console.time('No Flyweight');
    // for(var i = 0; i < iterations; i++ ){
    //     minionFactory.getNoFlyweightMinion('noob').draw(context, 1, 1, 20, 20);
    // };
    // console.timeEnd('No Flyweight')
    // console.log(window.performance.memory)
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/game' component={Game} />
      </Layout>
    );
  }
}
