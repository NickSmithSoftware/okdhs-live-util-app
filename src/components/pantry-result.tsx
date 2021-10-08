import * as React from 'react';

import Pantry from '../models/pantry';

interface Props {
    result: Pantry;
}

export const PantryResult = ({result} : Props) => {
    return (
        <li className="card w-75 text-dark my-2 mx-2 rounded no-border shadow-lg">
            <div className="card-header text-light bg-primary rounded-top">
                {result.name}
            </div>
            <div className="card-body text-dark">
                <h5 className="card-title text-dark">{result.telephone}</h5>
                <p className="card-text text-dark">{result.address.streetAddress + ", " + result.address.addressLocality + ", " + result.address.addressRegion + ", " + result.address.postalCode}</p>
            </div>
        </li>
    )
}