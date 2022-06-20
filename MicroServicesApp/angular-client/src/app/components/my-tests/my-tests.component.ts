import { Component, OnInit } from '@angular/core';
import { TestServiceService } from "../../services/test-service.service"
import {Test} from "../../shared/Models/test.model"

@Component({
  selector: 'app-my-tests',
  templateUrl: './my-tests.component.html',
  styleUrls: ['./my-tests.component.css']
})
export class MyTestsComponent implements OnInit {

  constructor(public testService: TestServiceService) { }
  private page:number=0;

  ngOnInit(): void {
    this.testService.GetTestsByAvtor(this.page);
  }

  NextPage() {
    this.page++;
    this.testService.GetTestsByAvtor(this.page);
  }

  PrewPage() {
    this.page--;
    this.testService.GetTestsByAvtor(this.page);
  }

  FirstPage() {
    this.page=0;
    this.testService.GetTestsByAvtor(this.page);
  }

  PutData(form: Test) {
    this.testService.formData = form;
  }

}
