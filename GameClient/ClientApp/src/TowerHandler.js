export default class TowerHandler {

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
        context.drawImage(this.archeryRange, tower.position.x, tower.position.y);
    }

    selectTower(context, x, y, placeable) {
        context.drawImage(this.archeryRange, x - this.archeryRange.width / 2, y - this.archeryRange.width / 2);
        context.beginPath();
        context.fillStyle = placeable ? "rgba(255, 255, 255, .3)" : "rgba(255, 0, 0, .3)";
        context.arc(x, y, 50, 0, Math.PI * 2, true);
        context.fill();
    }

}