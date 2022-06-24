import { HttpClient } from '@angular/common/http';
import { Router } from "@angular/router";

import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from "../../services/auth.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  invalidLogin: boolean = true;

  constructor(
    private http: HttpClient,
    private router: Router,
    public auth: AuthService) { }

  ngOnInit(): void {
    this.auth.invalidLogin = false;
  }

  
  
  login(form: NgForm) {
   this.auth.login(form);
  }

}
