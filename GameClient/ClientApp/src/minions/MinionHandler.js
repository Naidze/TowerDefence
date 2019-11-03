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
        
        // var that = this;
        console.log('spawning ' + id + "   " + type);
        let x = 0;
        let y = 175;
        var minion = new Minion(id, 'images/minions/noob.png', x, y, type, 3);
        // var img = new Image();
        // img.src = 'images/minions/noob.png';

        // var animate = function() {
        //     that.context.clearRect(0, 0, that.canvas.width, that.canvas.height);  // clear canvas
        //     that.context.drawImage(img, x, y);                       // draw image at current position
        //     x += 4;
        //     if (x < 250) requestAnimationFrame(animate)
        // }

        // img.onload = animate;
        this.minions.push(minion);
    }

}