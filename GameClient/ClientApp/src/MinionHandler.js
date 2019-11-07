export default class MinionHandler {

    images = [];

    constructor(types) {
        types.forEach(type => {
            this.images[type] = new Image();
            this.images[type].src = `images/minions/${type}.png`;
        })
    }

    render(context, minions) {
        minions.forEach(minion => {
            this.spawn(context, minion);
        });
    }

    spawn(context, minion) {
        var image = this.images[minion.name];
        context.drawImage(image, minion.position.x - image.width / 2, minion.position.y - image.height / 2);
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