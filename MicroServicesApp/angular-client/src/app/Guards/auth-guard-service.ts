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

    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    this.route.navigate(["Login"]);
    return false;
  }
}
