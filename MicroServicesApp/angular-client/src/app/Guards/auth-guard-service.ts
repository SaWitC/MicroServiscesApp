import { Injectable } from "@angular/core";
import { Router, CanActivate } from "@angular/router"
import {JwtHelperService} from "@auth0/angular-jwt"
import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router/index";

@Injectable()
export class AuthGuard implements CanActivate{

  constructor(private route: Router,private jwtHelper:JwtHelperService) {

  }

/*  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): any { throw new Error("Not implemented"); }*/

  canActivate() {
    var token = localStorage.getItem("jwt");
    if (token != null) {
      console.log("1-");
      if (!this.jwtHelper.isTokenExpired(token)) {
        console.log("1");
        return true;
      }
    }
    this.route.navigate(["/Login"]);
    console.log("2");

    return false;
  }
}
