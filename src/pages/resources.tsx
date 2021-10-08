import * as React from 'react';
import { Link } from 'react-router-dom';
import { Layout } from '../components/layout';

export const Resources: React.FC = () => {
    return (
        <Layout>
            <h1>Resources</h1>
            <Link to="/">Home</Link><br />
            <Link to="/find-pantry">Find Pantry</Link>
        </Layout>
    )
}