const dde = require('node-dde');

let client;

const onAdvise = (service, topic, item, text) => {

}

export const initialize = () => {
    client = dde.createClient('myapp', 'mytopic');

    client.on('advise', onAdvise);
    
    client.connect();
    client.startAdvise('myitem');
}

export const disconnect = () => {
    client ? client.disconnect() : client = null;
}

const send = () => {
    
}