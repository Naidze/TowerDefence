export default class Minion {

    constructor(type) {
        this.type = type;
        this.image = new Image();
        this.image.src = `images/minions/${type}.png`;
    }

    draw(context, x, y, startingHealth, health) {
        context.drawImage(this.image, x - this.image.width / 2, y - this.image.height / 2);
        this.drawHealthBar(context, x, y, startingHealth, health);
    }

    drawHealthBar(context, x, y, startingHealth, health) {
        if (health !== startingHealth) {
            context.fillStyle = this.getColor(health, startingHealth);
            context.fillRect(x - 5, y - 30, (health / startingHealth) * 15, 5);
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