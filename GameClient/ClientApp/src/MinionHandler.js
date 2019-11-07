export default class MinionHandler {

    noobImage = undefined;

    constructor() {
        this.noobImage = new Image();
        this.noobImage.src = `images/minions/noob.png`;
    }

    render(context, minions) {
        minions.forEach(minion => {
            this.spawn(context, minion);
        });
    }

    spawn(context, minion) {
        context.drawImage(this.noobImage, minion.position.x - this.noobImage.width / 2, minion.position.y - this.noobImage.height / 2);
        this.drawHealthBar(context, minion);
    }

    drawHealthBar(context, minion) {
        if (minion.health !== minion.startingHealth) {
            context.fillStyle = this.getColor(minion.health, minion.startingHealth);
            context.fillRect(minion.position.x - 5, minion.position.y - 30, (minion.health / minion.startingHealth) * 15, 5);
        }
    }

    getColor(health, startingHealth) {
        var ratio = health / startingHealth;
        // green
        if (ratio > 0.66) {
            return "#006400";
        }
        // orange
        else if (ratio > 0.33) {
            return "#FFA500";
        }
        // red
        else {
            return "#FF0000";
        }
    }

}