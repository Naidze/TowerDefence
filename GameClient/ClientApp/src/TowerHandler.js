import { distance } from "./utils";

export default class TowerHandler {

    distanceForClick = 40;
    images = [];

    constructor(types) {
        types.forEach(type => {
            this.images[type] = new Image();
            this.images[type].src = `images/towers/${type}.png`;
        })
    }

    render(context, towers) {
        towers.forEach(tower => {
            this.paint(context, tower);
        });
    }

    paint(context, tower) {
        var image = this.images[tower.name];
        context.drawImage(image, tower.position.x - image.width / 2, tower.position.y - image.height / 2);
    }

    selectTower(context, type, x, y, placeable) {
        var image = this.images[type];
        context.drawImage(image, x - image.width / 2, y - image.height / 2);
        context.beginPath();
        context.fillStyle = placeable ? "rgba(255, 255, 255, .3)" : "rgba(255, 0, 0, .3)";
        context.arc(x, y, 50, 0, Math.PI * 2, true);
        context.fill();
    }

    highlightTower(context, tower) {
        context.beginPath();
        context.fillStyle = "rgba(255, 255, 255, .3)";
        context.arc(tower.position.x, tower.position.y, tower.range, 0, Math.PI * 2, true);
        context.fill();
    }

    getClickedTower(x, y, towers) {
        var clickedTower = null;
        towers.forEach(tower => {
            if (distance(x, y, tower.position.x, tower.position.y) < this.distanceForClick) {
                clickedTower = tower;
            }
        });
        return clickedTower;
    }

}