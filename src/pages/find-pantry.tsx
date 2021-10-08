import React, {useState, useEffect} from 'react';
import {Link} from 'react-router-dom';

import { searchPantryCollection, getPantryCount, geocodeAddress, setPantryCount } from '../functions';
import {Layout} from '../components/layout';
import { PantryResult } from '../components/pantry-result';

export const FindPantry: React.FC = () => {

    const [results, setResults] = useState([]);
    const [address, setAddress] = useState('');
    const [geoData, setGeoData] = useState({});
    const [pantryCount, updatePantryCount] = useState(0);
    const [index, setIndex] = useState(0);

    //onComponentDidMount / initialization
    useEffect(() => {
        searchPantryCollection().then((e) => {
            setResults(e);
        })
        setPantryCount();
    }, [])

    //onPantryCount change
    useEffect(() => {
        updatePantryCount(getPantryCount());
    }, [getPantryCount])

    return (
        <Layout>
            <Link to="/resources"> Go Back </Link>
            <p className="text-dark">API Testing - Each press of More Results asynchronously pulls 4 more results from an online database.</p>
            {/* <h3>Page: {(index+1) % (Math.ceil(pantryCount/4) + 1)} of {Math.ceil(pantryCount/4)} -- Index {(index*4)%pantryCount} through {((index+1) * 4 - 1)%pantryCount}.</h3> */}
            {/* <button className="btn btn-warning"onClick={() => geocodeAddress(address).then((response) => {
                setGeoData(response);
                console.log(geoData);
            })}>
                Submit
            </button> */}
            <div className="container-fluid d-flex flex-column justify-content-center">
            <form className="d-flex flex-row px-2">
                <input type="text" className="form-control w-50 mx-2" value={address} onChange={(e) => {
                    setAddress(e.target.value)
                }} />
                <button type="button" className="btn btn-success mx-2 no-shadow" onClick={() => searchPantryCollection(address == undefined || address == '' ? undefined : address).then((e) => {
                    setResults(e);
                    setIndex(index+1);
                })}>
                    search city or more results
                </button>
            </form>
            <ul> 
                {results.map((result, index) => {
                    return <PantryResult key={index} result={result} />
                })}
            </ul>
            </div>
        </Layout>
    )
}

