import React, { useState } from "react";
import { Button, FormGroup, FormControl, FormLabel } from "react-bootstrap";
import style from "./LoginPage.module.scss";
import { AuthUser, IsTokenAvailable } from "../../Scripts/requests";
import { Redirect } from "react-router-dom";

export default function Login() {
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [isSignInRequested, setIsSignInRequested] = useState(false);

  function validateForm() {
    return login.length > 0 && password.length > 0;
  }

  async function handleSubmit(event) {
    event.preventDefault();
    await AuthUser({ Login: login, Password: password });
    if (IsTokenAvailable()) {
      setIsLoggedIn(true);
    }
  }

  async function handleSignInRequest() {
    setIsSignInRequested(true);
  }

  return (
    <>
      {isLoggedIn ? <Redirect to="/app" /> : null}
      {isSignInRequested ? <Redirect to="/signin" /> : null}
      <div className={style.Login}>
        <form onSubmit={handleSubmit}>
          <FormGroup controlId="login" bsSize="large">
            <FormLabel>Login</FormLabel>
            <FormControl
              autoFocus
              type="login"
              value={login}
              onChange={(e) => setLogin(e.target.value)}
            />
          </FormGroup>
          <FormGroup controlId="password" bsSize="large">
            <FormLabel>Password</FormLabel>
            <FormControl
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              type="password"
            />
          </FormGroup>
          <Button block bsSize="large" disabled={!validateForm()} type="submit">
            Login
          </Button>
          <Button block bsSize="large" onClick={() => handleSignInRequest()}>
            SignIn
          </Button>
        </form>
      </div>
    </>
  );
}
