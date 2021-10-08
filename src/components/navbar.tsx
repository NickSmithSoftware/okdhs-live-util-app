import React from 'react';

import {IconButton} from './icon-button';
import { closeWindow, minimizeWindow } from '../functions';

interface Props {
    className: string
}

export const Navbar: React.FC<Props> = () => {
    return (
        <nav className="drag navbar navbar-light bg-dark text-dark d-flex flex-row m-0 p-0" id="menu">
            <div className="container-fluid p-0 m-0 d-flex justify-content-between h-100">
                    <div id="module-links" className="">
                        <div className="text-light">Story: User - Geolocate Address - FindLocalPantry</div>
                        {/* <IconLink to="/" id="icon-home" onClick={() => {
                            // tbd
                        }}/>
                        <IconLink to="/settings" id="icon-settings" onClick={() => {
                            // tbd
                        }}/> */}
                    </div>
                    <div id="window-options" className="d-flex flex-row h-100 ">
                        <div className="hover-lighter h-100 d-flex align-middle">
                            <IconButton name="minimize" onClick={() => {
                                minimizeWindow();
                            }}/>
                        </div>
                        <div className=" h-100 hover-lighter d-flex align-middle">
                            <IconButton name="exit" onClick={() => {
                                closeWindow();
                            }}/>
                        </div>
                    </div>
            </div>
        </nav>
    )
}