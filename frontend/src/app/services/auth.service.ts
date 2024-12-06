import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from "@angular/common/http";
import {
  catchError,
  Observable,
  of,
  switchMap,
  throwError
} from "rxjs";
import {AuthResult, LoginDto, RefreshTokenDto, TokenStored, VerifyToken} from "../models/auth/user";
import {environment} from "../../environments/environment";


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.rootUrl}/api/Auth`;
  private tokenKey = 'access_token';
  constructor(private http:HttpClient) { }

  getToken(): TokenStored | undefined{
    let token:TokenStored | undefined;
    let token_l = localStorage.getItem(this.tokenKey);
    if (token_l) {
      try {
        token = JSON.parse(token_l) as TokenStored;
      }
      catch (e) {
        token = undefined;
      }
    }
    return token;
  }

  logout() {
    localStorage.removeItem(this.tokenKey)
  }

  setToken(data: AuthResult): void {
    const tokenStored : TokenStored = {
      token:data.token!,
      expirationDate:data.expirationDate!,
      refreshToken:data.refreshToken!
    }
    localStorage.setItem(this.tokenKey, JSON.stringify(tokenStored));
  }

  refreshToken(): Observable<AuthResult> {
    const val = this.getToken()
    if(!val || !val.token || !val.refreshToken)
    {
      this.logout()
      return throwError(() => new HttpErrorResponse({
        error:{success:false,errors:["No unauthenticated token Found - ClientSide"]}
      }));
    }
    const obj : RefreshTokenDto = {
      token: val.token,
      refreshToken: val.refreshToken
    }
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    const url = `${this.apiUrl}/refreshToken`
    return this.handleAuthResult(this.http.post<AuthResult>(`${url}`, obj, { headers }));
  }

  verifyToken(): Observable<VerifyToken>{
    const val = this.getToken()
    if(!val || !val.token || !val.refreshToken)
    {
      return of({success:false,errors:["No unauthenticated token Found - ClientSide"]})
    }
    const obj : RefreshTokenDto = {
      token: val.token,
      refreshToken: val.refreshToken
    }
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    const url = `${this.apiUrl}/verifyToken`
    return this.http.post<VerifyToken>(`${url}`, obj, { headers });
  }

  private handleAuthResult(res:Observable<AuthResult>):Observable<AuthResult>{
    return res.pipe(
      switchMap( response => {
        if(response.success){
          this.setToken(response)
          return of(response)
        }
        this.logout()
        return  throwError(() => new HttpErrorResponse({
          error:response
        }));
      }),
      catchError(error => {
        this.logout()
        return throwError(() => error);
      })
    );
  }

  login(obj:LoginDto) : Observable<AuthResult>{
    const url = `${this.apiUrl}/login`
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.handleAuthResult(this.http.post<AuthResult>(`${url}`, obj, { headers }));
  }
}
