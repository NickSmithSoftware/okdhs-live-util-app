/* eslint-disable @typescript-eslint/explicit-module-boundary-types */
import React from 'react';

interface Props {
    onClick: () => void,
    name: string;
}

export const IconButton = ({onClick, name}: Props) => {
    return (
        <button type="button" onClick={onClick} className="icon-button no-drag btn btn-link" id={name + "-button"}></button>
    )
};
