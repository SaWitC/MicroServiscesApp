import { Component, OnInit } from '@angular/core';
import { TestServiceService } from "../../services/../../services/test-service.service"
import { QuestService } from "../../services/../../services/quest.service"
import { ActivatedRoute } from '@angular/router';
import { Quest, Quest2 } from '../../../shared/Models/quest.model';

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
  }

  RemoveQuest(id:number) {
    //var id = questId as number;
    this.QuestService.Remove(id).subscribe(res => {
      console.log("Deleted");
    }, err => {
      console.log(err)
    })
  }

  PutData(form: Quest2) {
    var quest = new Quest();
    quest.HelpText = form.HelpText;
    quest.Id = form.Id;
    quest.ImgPath = form.ImgPath;
    quest.QuestText = form.QuestText;
    quest.Right_answer = form.Right_answer;
    quest.TestId = form.TestId;

    this.QuestService.FormData = quest;
  }

}
