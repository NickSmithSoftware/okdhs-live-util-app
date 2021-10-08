import * as React from 'react';
import * as ReactDOM from 'react-dom';
import {HashRouter, Switch, Route} from 'react-router-dom';


import './custom.scss';
import { Home } from './pages/home';
import { Resources } from './pages/resources';
import { FindPantry } from './pages/find-pantry';
import { PageNotFound } from './pages/page-not-found';


function render() {
  ReactDOM.render((
    <HashRouter>
      <Switch>
        <Route path="/" exact component={Home} />
        <Route path="/resources" component={Resources} />
        <Route path="/find-pantry" component={FindPantry} />
        <Route path="/404" component={PageNotFound} />
      </Switch>
    </HashRouter>
  ), document.getElementById('app'));
}

render();