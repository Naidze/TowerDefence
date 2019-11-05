import { distance } from "./utils";

export default class TowerHandler {

    distanceForClick = 40;
    archeryRange = undefined;

    constructor() {
        this.archeryRange = new Image();
        this.archeryRange.src = `images/towers/archery_range.png`;
        this.archeryRange.style.opacity = "0.5";
    }

    render(context, towers) {
        towers.forEach(tower => {
            this.paint(context, tower);
        });
    }

    paint(context, tower) {
        context.drawImage(this.archeryRange, tower.position.x - this.archeryRange.width / 2, tower.position.y - this.archeryRange.height / 2);
    }

    selectTower(context, x, y, placeable) {
        context.drawImage(this.archeryRange, x - this.archeryRange.width / 2, y - this.archeryRange.height / 2);
        context.beginPath();
        context.fillStyle = placeable ? "rgba(255, 255, 255, .3)" : "rgba(255, 0, 0, .3)";
        context.arc(x, y, 50, 0, Math.PI * 2, true);
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