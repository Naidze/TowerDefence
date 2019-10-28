import Minion from '../minions/Minion';

export function spawn(canvas, type) {
    console.log('spawning ' + type);
    var minion = new Minion(0, 0, type, 3);
    var img = new Image();
    img.src = 'images/minions/noob.png';
    const context = canvas.getContext('2d')
    img.onload = function (e) {
        context.drawImage(img, 50, 50);
    }
}