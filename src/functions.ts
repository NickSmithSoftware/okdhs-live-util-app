declare global {
    interface Window {
        "API": {
            minimizeWindow: () => void,
            closeWindow: () => void,
            searchPantryCollection: (query?: string) => Promise<Pantry[]>,
            getPantryCount: () => number,
            setPantryCount: () => Promise<void>,
            geocodeAddress: (address: string) => Promise<any>
        }
    }
}

export function minimizeWindow() : void {
    window.API.minimizeWindow();
    console.log("Minimized window")
}

export function closeWindow() : void {
    window.API.closeWindow();
    console.log("Closed Window")
}

import Pantry from './models/pantry';
export async function searchPantryCollection(query?: string|undefined) : Promise<Pantry[]> {
    try {
        console.log("getting pantry collection results...")
        return await window.API.searchPantryCollection(query);
    } catch {
        console.error("Could not search pantry collection!!")
    }
}

export function setPantryCount() : void {
    window.API.setPantryCount();
}

export function getPantryCount() : number {
    return window.API.getPantryCount();
}

export async function geocodeAddress(address: string) : Promise<any> {
    try {
        console.log("[FIND-PANTRY.JS] GEOCODING ADDRESS")
        return await window.API.geocodeAddress(address);
    } catch {
        console.error("COULD NOT GEOCODE ADDRESS");
    }
}