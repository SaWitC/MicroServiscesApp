import { Component, OnInit } from '@angular/core';
import { TestServiceService } from "../../../services/test-service.service";
import { NgForm } from "@angular/forms"
import {Test} from "../../../shared/Models/test.model"

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})

export class DetailsComponent implements OnInit {

  constructor(public test: TestServiceService) { }
  page: number = 1;


  ngOnInit(): void {
    this.test.refreshList();
    console.log(this.test.list);
  }

  PutData(form: Test) {
    this.test.formData = form;
  }
  Update(form: NgForm) {

  }

  First() {
    this.page = 0;
    this.loadTestes()
  }

  loadTestes() {
    this.test.GetTestes(this.page);
    console.log("page " + this.page);
    this.page++;

    //this.test.GetTestes(this.page).subscribe(res => {
    //  console.log(res);
    //  if (res == null) {
    //    this.page --;
    //  }
    //  else {
    //    this.page++;
    //  }
    //},
    //  err => {
    //    console.log(err);
    //    this.page--;
    //  });
  }

}
