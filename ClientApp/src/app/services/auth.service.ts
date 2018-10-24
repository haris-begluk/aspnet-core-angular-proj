import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import * as auth0 from 'auth0-js';
import Auth0Lock from 'auth0-lock';


(window as any).global = window;

@Injectable()
export class Auth {

  auth0 = new auth0.WebAuth({
    clientID: 'FuBO6u4oLe4cSGIVnGlYrPj0Kfk27lIc',
    domain: 'haris-begluk.eu.auth0.com',
    responseType: 'token id_token',
    redirectUri: 'https://localhost:5001/vehicles',
    scope: 'openid'
  });
  lock = new Auth0Lock(
    'FuBO6u4oLe4cSGIVnGlYrPj0Kfk27lIc',
    'haris-begluk.eu.auth0.com',
    {}
  );

  constructor(public router: Router) {
    // Add callback for lock `authenticated` event
    this.lock.on("authenticated", (authResult) => {
    this.setSession(authResult);
    });
  }

  public login(): void {
    this.auth0.authorize(); 
    this.setSession(auth0);
  } 

    private setSession(authResult): void {
    // Set the time that the Access Token will expire at
    const expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());
    localStorage.setItem('access_token', authResult.accessToken);
    localStorage.setItem('id_token', authResult.idToken);
    localStorage.setItem('expires_at', expiresAt);
  } 

  public logout(): void {
    // Remove tokens and expiry time from localStorage
    localStorage.removeItem('access_token');
    localStorage.removeItem('id_token');
    localStorage.removeItem('expires_at');
    // Go back to the home route
    this.router.navigate(['/']);
  }

  public isAuthenticated(): boolean {
    // Check whether the current time is past the
    // Access Token's expiry time
    const expiresAt = JSON.parse(localStorage.getItem('expires_at') || '{}');
    return new Date().getTime() < expiresAt;
  }

}