import Minion from './Minion';

export default class MinionHandler {

    minions = undefined;
    canvas = undefined;
    context = undefined;

    constructor(canvas) {
        this.minions = [];
        this.canvas = canvas;
        this.context = canvas.getContext('2d');
    }

    spawn(id, type) {
        console.log('spawning ' + id + "   " + type);
        var minion = new Minion(id, 0, 175, type, 3);
        var img = new Image();
        img.src = 'images/minions/noob.png';
        img.onload = function () {
            this.context.drawImage(img, 0, 175);
        }.bind(this);
        this.minions.push(minion);
    }

}