import axios = require('axios');

const GEOCODE_API_KEY="00e25cda031b0617259fe727678dbdaa"
const GEOCODE_ENDPOINT="https://api.positionstack.com/v1/forward"

export async function geocodeAddress(address: string): Promise<string> {
    const params = {
      access_key: process.env.GEOCODE_API_KEY,
      query: address
    }
    if(address == undefined) return undefined;
    return (await axios.default.get(process.env.GEOCODE_ENDPOINT, {params})) as string;
}