import { Component, OnInit } from '@angular/core';
import { TestPassingServiceService } from "src/app/services/test-passing-service.service";
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-test-passing',
  templateUrl: './test-passing.component.html',
  styleUrls: ['./test-passing.component.css']
})
export class TestPassingComponent implements OnInit {

  constructor(public TestPassingServiceService: TestPassingServiceService,
    private activateRoute: ActivatedRoute  ) { }

  Id: number;
  questNumber: number=0;

  ngOnInit(): void {
    this.TestPassingServiceService.questCompleted = false;
    this.Id = this.activateRoute.snapshot.params['Id'];
    this.TestPassingServiceService.StartTest(this.Id);
  }

  ToAnswer(value: string) {
    this.questNumber++;
    this.TestPassingServiceService.PassingTest(this.questNumber, value);


  }

}
