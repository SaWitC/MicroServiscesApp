import { Component, OnInit } from '@angular/core';
import { TestServiceService } from "../../services/../../services/test-service.service"
import { QuestService } from "../../services/../../services/quest.service"

import { Test } from "../../../shared/Models/test.model";
import { NgForm } from '@angular/forms';
import { Quest } from '../../../shared/Models/quest.model';
import { LeftAnswer } from '../../../shared/Models/left-answer.model';

@Component({
  selector: 'app-quest-form',
  templateUrl: './quest-form.component.html',
  styleUrls: ['./quest-form.component.css']
})
export class QuestFormComponent implements OnInit {

  constructor(public TestService: TestServiceService,
    public QuestService: QuestService) { }

  ngOnInit(): void {

  }

  Submit(form: NgForm, TestId: number) {

    var LeftAnsvers: string[];
    LeftAnsvers = [];
    console.log(form.value.Left_answer1)
    LeftAnsvers.push(form.value.Left_answer1);
    LeftAnsvers.push(form.value.Left_answer2);
    LeftAnsvers.push(form.value.Left_answer3);
    console.log(LeftAnsvers);
   // this.QuestService.FormData.LeftAnswer = LeftAnsvers;


    this.QuestService.Create(TestId, LeftAnsvers).subscribe(res => {
      console.log(res)
      this.QuestService.GetQuestsByTestId(TestId);
      form.reset();
    },
      err => {
        console.log(err);
      })
  }


}
