import { Component, OnInit } from '@angular/core';
import { TestServiceService } from "../../services/../../services/test-service.service"
import { QuestService } from "../../services/../../services/quest.service"
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class InfoComponent implements OnInit {

  constructor(public TestService: TestServiceService,
    public QuestService: QuestService,
    private activateRoute: ActivatedRoute) { }

  Id: number;

  ngOnInit(): void {
    this.TestService.getTestById(this.Id);
    this.Id = this.activateRoute.snapshot.params['Id'];

    this.QuestService.GetQuestsByTestId(this.Id);

    
    //this.TestService.getTestById();
  }

}
