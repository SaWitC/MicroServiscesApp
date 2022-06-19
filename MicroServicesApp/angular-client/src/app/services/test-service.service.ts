import { Injectable } from '@angular/core';
import { NgForm } from "@angular/forms"
import { HttpClient } from '@angular/common/http'
import {Test} from "../shared/Models/test.model"

@Injectable({
  providedIn: 'root'
})
export class TestServiceService {

  constructor(private http: HttpClient) { }

  formData: Test = new Test();
  //list: Test[];

  createTest(form: NgForm) {
    return this.http.post("http://localhost:9410/api/Tests/api/Tests/CreateTest",this.formData);
  }

  GetTestes() {
    return this.http.get("http://localhost:9410/api/Tests");
  }
}
