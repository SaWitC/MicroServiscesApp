import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../services/auth.service";
import { FormGroup, NgForm } from "@angular/forms"
import { Route, Router } from "@angular/router";
/*import { } from""*/


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  reactiveForm: FormGroup;
  invalidOperation: boolean = false;
  IsPassvordConfirmed: boolean = true;


  constructor(public authService: AuthService,
    private router: Router) { }

  IsEmptyMessage: boolean = true;

  ngOnInit(): void {
  }

  /*  get f() { return this.reactiveForm.controls }*/

  Register(form: NgForm) {
    this.IsPassvordConfirmed = false;
    console.log("1");

    if (form.value.ConfirmPassword == form.value.Password) {
      console.log("2");
      this.IsPassvordConfirmed = true;
      this.authService.Register(form);
    }
    this.IsEmptyMessage=this.authService.ErrorMessage == "" ? true : false;
  }

}
