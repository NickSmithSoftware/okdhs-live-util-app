import React from 'react';

import {Navbar} from './navbar';

import 'bootstrap';

export const Layout: React.FC = ( { children } ) => {
    return (
        <div id="layout" className="overflow-hidden">
            <Navbar className="drag" />
            <div id="layout-content" className="w-75 mx-auto text-center">
                { children }
            </div>
        </div>
    )
}