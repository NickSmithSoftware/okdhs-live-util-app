import { app, BrowserWindow, ipcMain, nativeTheme } from 'electron';
import path = require('path');

import {collections, connectToDatabase, disconnectFromDatabase} from './services/database-service';
import {geocodeAddress} from './services/geocode-service';
import Pantry from './models/pantry';


declare const MAIN_WINDOW_WEBPACK_ENTRY: string;
declare const MAIN_WINDOW_PRELOAD_WEBPACK_ENTRY: string;

// Handle creating/removing shortcuts on Windows when installing/uninstalling.
if (require('electron-squirrel-startup')) { // eslint-disable-line global-require
  app.quit();
}


// total documents in pantriesCollection
let pantriesCount: number;

let win: BrowserWindow; // variable containing the main window
const createWindow = () => {
  // Create the browser window.
  nativeTheme.themeSource = "dark";
  win = new BrowserWindow({
    title: "OKDHS Live Utility",
    minWidth: 1080,
    minHeight: 720,
    frame: false,
    autoHideMenuBar: true,
    show: false,
    backgroundColor: "#343a40",
    webPreferences: {
      nodeIntegration: false,
      preload: MAIN_WINDOW_PRELOAD_WEBPACK_ENTRY,
      defaultFontFamily: {sansSerif: "sansSerif"},
      contextIsolation: true
    }
  });

  // and load the index.html of the app.
  win.loadURL(MAIN_WINDOW_WEBPACK_ENTRY);

  //show application when ready
  win.once('ready-to-show', () => {
    win.show();
  });

  //connect to database and count documents
  connectToDatabase();

  // Open the DevTools.
  win.webContents.openDevTools();
};



//searches pantries collection from resources database 

let i = 0, lastQuery: string|undefined;
async function searchPantryCollection(query?: string) : Promise<Pantry[]> {
  const isUndefined = query == undefined;
  
  if(!isUndefined && query != lastQuery) {
    i = 0;
    lastQuery = query;
  }

  // const city = isUndefined ? undefined : query.replace(/[^a-zA-Z]+/g, '');
  // console.log(city);
  // const zip = isUndefined ? undefined : query.replace(/[^0-9]{5}/g, '');
  // console.log(zip);

  const skipNumber = ((i++) * 4) % (pantriesCount == undefined ? 1000000000 : pantriesCount);
  return (await collections.pantries.find(!isUndefined ? {$text: {$search: query, $caseSensitive: false}} : {}).skip(skipNumber).project({_id: 0, name: 1, telephone: 1, address: {streetAddress:1, addressLocality:1, addressRegion:1, postalCode:1}, image: 1}).limit(4).toArray()) as Pantry[];
}

async function setPantryCount() {
  pantriesCount = await collections.pantries.countDocuments();
}



// *** APP LISTENERS *** //




app.on('ready', createWindow);

app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') {
    disconnectFromDatabase();
    app.quit();
  }
});

app.on('activate', () => {
  if (BrowserWindow.getAllWindows().length === 0) {
    createWindow();
  }
});




// *** CONTEXT BRIDGE LISTENERS *** //




/* 
      one-way communication - listeners
*/

ipcMain.on("close-window", () => {
win.close();
  disconnectFromDatabase();
  app.quit();
});

ipcMain.on("minimize-window", () => {
win.minimize();
});

/* 
      two-way communication - handlers
*/

ipcMain.handle("get-current-date", () => {
  const date = new Date().toISOString();
  return date;
})

ipcMain.handle("search-pantry-collection", (event, query?: string|undefined) => {
  return query ? searchPantryCollection(query) : searchPantryCollection();
})

ipcMain.handle("set-pantry-count", () => {
  setPantryCount().then(()=> console.log(pantriesCount))
})

ipcMain.handle("get-pantry-count", () => {
  return pantriesCount;
})

ipcMain.handle("geocode-address", (event: Electron.IpcMainEvent, address:string) => {
  return geocodeAddress(address);
})