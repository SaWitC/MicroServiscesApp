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

  invalidLogin:boolean=true;

  constructor(
    private http: HttpClient,
    private jwtHelperService: JwtHelperService,
    private router:Router) { }

  

  isAuhtenticated(): boolean {
    var token: string = "";
    token += localStorage.getItem("jwt");
    if (token && !this.jwtHelperService.isTokenExpired(token)) {
      return true;
    } else {
      return false;
    }

  }

  logOut() {
    localStorage.removeItem("jwt");
    this.invalidLogin = true;
  }


  login(form: NgForm) {
    const credentails = {
      userName: form.value.userName,
      password: form.value.password
    }

    this.http.post("http://localhost:46574/api/Auth/api/Auth/Login", credentails).subscribe(
      response => {
        //const token = (<any>response).token;
        const token: string = (<any>response).token;
        localStorage.setItem("jwt", token);
        this.invalidLogin = false;
        this.router.navigate(["/"]);
        console.log("ok");
      },
      err => {
        this.invalidLogin = true;
        console.log("error");
      });
  }
}
