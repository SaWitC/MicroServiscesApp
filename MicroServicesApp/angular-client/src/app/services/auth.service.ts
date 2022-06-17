import { Injectable,Inject } from '@angular/core';
import { Observable } from 'rxjs';
//import { Token } from "@angular/compiler";
import { HttpClient } from '@angular/common/http'
import { IdentityApi_path } from "../app-injection-tokens"
import { JwtHelperService } from "@auth0/angular-jwt"
import { Router } from "@angular/router"
import { tap } from "rxjs/operators";
import {NgForm} from "@angular/forms"

import {Token} from"../Models/Token";

export const acces_Token_Key = "ResourceAccesToken";

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  constructor(
    private http: HttpClient,
    @Inject(IdentityApi_path) private apiUrl: string,
    private jwtHelper: JwtHelperService,
    private router:Router) { }

  

  //isAutenticated(): boolean {
  //  var token: string = "";
  //   token+= localStorage.getItem(acces_Token_Key);
  //  return token!=null&& !this.jwtHelper.isTokenExpired(token);
  //}

  //logout():void {
  //  localStorage.removeItem(acces_Token_Key);
  //  this.router.navigate(['']);
  //}
}
