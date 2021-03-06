import { Component, OnInit } from '@angular/core';

import { NgForm } from '@angular/forms';
/*import { Test } from "src/app/shared/Models/test.model"*/
import { TestServiceService } from "../../services/test-service.service"
import { AuthService } from "../../services/auth.service"
import { Route, Router } from '@angular/router'
import { BooleanLiteral } from 'typescript';
/*import {HttpClient} from "@angular/common/http"*/

@Component({
  selector: 'app-test-form',
  templateUrl: './test-form.component.html',
  styleUrls: ['./test-form.component.css']
})
export class TestFormComponent implements OnInit {

  //list:Test[];

  constructor(
    private router: Router,
    public testService: TestServiceService,
    private auth:AuthService) { }

  ngOnInit(): void {
    this.auth.isAuhtenticated();
  }
  ClearImg() {
    this.testService.formData.LogoImagPath = "";
  }

  Create(form: NgForm) {
      this.testService.createTest(form).subscribe(
        res => {
          console.log(res);
          form.reset();
          this.router.navigate([["/"]]);
        },
        err => {
          console.log(err);
        });
    
  }
}
