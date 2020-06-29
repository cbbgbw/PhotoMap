import React, { useState } from "react";
import { Button, FormGroup, FormControl, FormLabel } from "react-bootstrap";
import style from "./SignUpPage.module.scss";
import { PostUser, AuthUser, IsTokenAvailable } from "../../Scripts/requests";
import { Redirect } from "react-router-dom";

export default function SignUpPage() {
  const [firstName, setFirstname] = useState("");
  const [lastName, setLastName] = useState("");
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  function validateForm() {
    return login.length > 0 && password.length > 0;
  }

  async function handleSubmit(event) {
    event.preventDefault();
    await PostUser({
      FirstName: firstName,
      LastName: lastName,
      Login: login,
      Password: password,
    });

    await AuthUser({ Login: login, Password: password });
    if (IsTokenAvailable()) {
      setIsLoggedIn(true);
    }
  }

  return (
    <>
      {isLoggedIn ? <Redirect to="/app" /> : null}
      <div className={style.Login}>
        <form onSubmit={handleSubmit}>
          <FormGroup controlId="firstName" bsSize="large">
            <FormLabel>FirstName</FormLabel>
            <FormControl
              autoFocus
              type="text"
              value={firstName}
              onChange={(e) => setFirstname(e.target.value)}
            />
          </FormGroup>
          <FormGroup controlId="lastName" bsSize="large">
            <FormLabel>LastName</FormLabel>
            <FormControl
              autoFocus
              type="text"
              value={lastName}
              onChange={(e) => setLastName(e.target.value)}
            />
          </FormGroup>
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
            SignUp
          </Button>
        </form>
      </div>
    </>
  );
}
