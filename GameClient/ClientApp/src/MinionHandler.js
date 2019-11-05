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
    }

}