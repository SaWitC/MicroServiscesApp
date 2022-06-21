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
  list: Test[];
  listByAvtor: Test[];


  createTest(form: NgForm) {


    //var myJSONString = JSON.stringify(this.formData.Description);
    //var myEscapedJSONString = this.formData.Description.replace(/\\n/g, "\\n")
    //  .replace(/\\'/g, "\\'")
    //  .replace(/\\"/g, '\\"')
    //  .replace(/\\&/g, "\\&")
    //  .replace(/\\r/g, "\\r")
    //  .replace(/\\t/g, "\\t")
    //  .replace(/\\b/g, "\\b")
    //  .replace(/\\f/g, "\\f");



    console.log(this.formData)

    var x = this.formData.Description.split('\n');
    console.log("=======================")
    console.log(x);
    console.log(this.formData);
    return this.http.post("http://localhost:9410/api/Tests/api/Tests/CreateTest",this.formData);
  }

  GetTestes(page:number) {
    return this.http.get(`http://localhost:9410/api/Tests/GetTests/${page}`).toPromise()
      .then(res => {
        console.log("res");
        this.list = [];

        //console.log(res as Test[]);
        this.list = res as Test[];
      });
  }



  getTestById(id: number) {
    return this.http.get(`http://localhost:9410/api/Tests/${id}`);
  }

  refreshList() {
    this.http.get(`http://localhost:9410/api/Tests/GetTests/0`)
      .toPromise().then(res => {
        this.list = res as Test[];

        console.log(res as Test[]);
      });
  }

  GetTestsByAvtor(page:number) {
    this.http.get(`http://localhost:9410/api/Tests/GetTestsByAvtor/${page}`)
      .toPromise().then(
      res => {
          this.listByAvtor = res as Test[];
          console.log(res as Test[]);
      })
      .catch(
      err => {
        console.log(err);

      });
  }


}
