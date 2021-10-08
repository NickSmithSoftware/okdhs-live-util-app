interface Address {
    streetAddress: string;
    addressLocality: string;
    addressRegion: string;
    postalCode: string;
}

export default class Pantry {
    constructor(public name: string, public address: Address, public telephone: string, public image: string) {}
}