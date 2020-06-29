import React from "react";
import style from "./App.module.scss";
import { IsTokenAvailable } from "./Scripts/requests";
import {
  BrowserRouter as Router,
  Route,
  Switch,
  Redirect,
} from "react-router-dom";
import MapPage from "./Components/MapPage/MapPage";
import LoginPage from "./Components/LoginPage/LoginPage";
import SignIn from "./Components/SignUpPage/SignUpPage";

export default class App extends React.Component {
  render() {
    return (
      <Router>
        <Route path="/">
          {IsTokenAvailable() ? (
            <Redirect to="/app" />
          ) : (
            <Redirect to="/login" />
          )}
        </Route>
        <Switch>
          <Route path="/app">
            <MapPage />
          </Route>
          <Route path="/login">
            <LoginPage />
          </Route>
          <Route path="/signin">
            <SignIn />
          </Route>
        </Switch>
      </Router>
    );
  }
}
