import React from 'react';
import { Link } from 'react-router-dom';
import {Layout} from '../components/layout';

export const Home: React.FC = ()  => {
    return (
        <Layout>
            <h1>Home</h1>
            <Link to="/resources">Resources</Link>
            <Link to="/ims">IMS</Link>
        </Layout>
    )
}