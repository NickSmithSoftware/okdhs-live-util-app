import * as React from 'react';
import {Link} from 'react-router-dom';

import {Layout} from '../components/layout';

export const PageNotFound:React.FC = ()  => {
    return (
        <Layout>
            <h1>PAGE NOT FOUND</h1>
            <Link to="/">Home</Link>
        </Layout>
    )
}