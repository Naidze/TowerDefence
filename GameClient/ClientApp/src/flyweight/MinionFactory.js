import Minion from "./Minion";

export default class MinionFactory {

    minions;

    constructor() {
        this.minions = [];
    }

    getMinion(type) {
        const minion =  this.minions.find(minion => minion.type === type);
        if (minion) {
            return minion;
        } else {
            const newMinion = new Minion(type);
            this.minions.push(newMinion);
            return newMinion;
        }
    }

    getNoFlyweightMinion(type) {
        return new Minion(type);
    }

}