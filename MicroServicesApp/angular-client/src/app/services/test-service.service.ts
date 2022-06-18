import { Injectable } from '@angular/core';
import { NgForm } from "@angular/forms"
import { HttpClient } from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class TestServiceService {

  constructor(private http:HttpClient) { }

  createTest(form: NgForm) {
    this.http.post("http://localhost:9410/api/Tests/api/Tests/CreateTest",form);
  }
}
