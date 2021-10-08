import * as mongoDB from 'mongodb';
import * as dotenv from 'dotenv';

export const collections: { pantries?: mongoDB.Collection } = {};

let client: mongoDB.MongoClient | undefined;
const DB_CONN_STRING="mongodb+srv://Admin:nicem2me@okdhslive.qctg1.mongodb.net/test?authSource=admin&replicaSet=atlas-dmm2v7-shard-0&readPreference=primary&appname=MongoDB%20Compass&ssl=true"
const DB_NAME="resources"
const PANTRIES_COLLECTION_NAME="pantries"

export async function connectToDatabase() : Promise<void> {
    dotenv.config();

    client = new mongoDB.MongoClient(DB_CONN_STRING);

    await client.connect();
    
    const db: mongoDB.Db = client.db(DB_NAME);

    const pantriesCollection: mongoDB.Collection = db.collection(PANTRIES_COLLECTION_NAME);

    collections.pantries = pantriesCollection;

    console.log(`Successfully connected to database: ${db.databaseName}`);
}

export async function disconnectFromDatabase() : Promise<void> {
    await client.close();

    console.log("Client Disconnected")
}