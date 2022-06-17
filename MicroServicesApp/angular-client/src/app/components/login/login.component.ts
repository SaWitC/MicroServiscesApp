import { HttpClient } from '@angular/common/http';
import { Router } from "@angular/router";

import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {


  invalidLogin: boolean = true

  constructor(
    private http: HttpClient,
    private router: Router) { }

  ngOnInit(): void {
  }

  login(form: NgForm) {
    const credentails = {
      userName: form.value.userName,
      password: form.value.password
    }

    this.http.post("http://localhost:46574/api/Auth/api/Auth/Login",credentails).subscribe(
      response => {
        //const token = (<any>response).token;
        const token:string = (<any>response).token;
        localStorage.setItem("jwt", token);
        console.log(response);
        this.invalidLogin = false;
        //this.router.navigate(["/"]);
        console.log("1");
        
      },
      err => {
        this.invalidLogin = true;
        console.log("2");
        console.log(err);

      });
    console.log("3");
    console.log("jwt + "+localStorage.getItem("jwt"));

    

  }

}
