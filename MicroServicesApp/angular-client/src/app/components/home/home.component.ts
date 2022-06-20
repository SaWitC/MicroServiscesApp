import { Component, OnInit } from '@angular/core';
import { NgForm } from "@angular/forms";
import { Router } from "@angular/router"
import {JwtHelperService} from "@auth0/angular-jwt";
import { AuthService } from "../../services/auth.service";
import {TestServiceService} from "../../services/test-service.service"

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  invalidLogin: boolean=true;

  constructor(private router: Router,
    public testService: TestServiceService,
    private auth: AuthService) { }


  ngOnInit(): void {
    this.invalidLogin = !this.auth.isAuhtenticated();
  }

  //isAuhtenticated():boolean {
  //  var token: string = "";
  //  token+= localStorage.getItem("jwt");
  //  if (token&&!this.jwtHelperService.isTokenExpired(token)) {
  //    return true;
  //  } else {
  //    return false;
  //  }

  //}

  logOut() {
    this.auth.logOut();
    this.invalidLogin = true;
  }

}
