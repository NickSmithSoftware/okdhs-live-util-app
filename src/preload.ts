import {ipcRenderer, contextBridge} from 'electron';

contextBridge.exposeInMainWorld("API", {
    minimizeWindow: () => ipcRenderer.send("minimize-window"),
    closeWindow: () => ipcRenderer.send("close-window"),
    setPantryCount: () => ipcRenderer.send("set-pantry-count"),
    getCurrentDate: () => ipcRenderer.invoke("get-current-date"),
    searchPantryCollection: (query?: string|undefined) => { return ipcRenderer.invoke("search-pantry-collection", query)},
    getPantryCount: () => { return ipcRenderer.invoke("get-pantry-count")},
    geocodeAddress: (address: string) => { return ipcRenderer.invoke("geocode-address", address)}
});