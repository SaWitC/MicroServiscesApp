import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { TestCompletedModel } from "src/app/shared/Models/test-completed-model.=model";
import { Test } from "../shared/Models/test.model";
import { Quest, Quest2 } from '../shared/Models/quest.model';
import { NextNotification } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class TestPassingServiceService {

  constructor(private http: HttpClient) { }

  CurrentTest: Test = new Test();
  Quests: Quest2[];
  CurrentQuest: Quest2 = new Quest2();
  Answers: string[];
  RightAnswerCount: number = 0;
  questCompleted: boolean = false;
  

  SaveResultat(model: TestCompletedModel) { }

  //GetResultat(page: number) { }

  PassingTest(questNumber: number, answer: string) {

    if (answer.toString() == this.CurrentQuest.Right_answer.toString())
      this.RightAnswerCount++;
    console.log(questNumber)
    if (this.Quests.length == questNumber) {
      this.questCompleted = true;
      return;
    }

    this.CurrentQuest = this.Quests[questNumber];
    this.Answers = [];

    this.Answers.push(this.CurrentQuest.Right_answer);
    for (var i of this.CurrentQuest.LeftAnswers) {
      this.Answers.push(i.Title);
    }
    this.Answers = this.Answers.sort((a, b) => 0.5 - Math.random());
  }

  StartTest(testId: number) {
    this.Quests = [];
    this.Answers = [];
    this.http.get(`http://localhost:9410/api/Quests/GetTestForPassing/${testId}`).subscribe(
      res => {
        this.Quests = res as Quest2[];
        console.log(this.Quests)
        this.CurrentQuest = this.Quests[0];
        this.Answers.push(this.CurrentQuest.Right_answer);
        for (var i of this.CurrentQuest.LeftAnswers) {
          this.Answers.push(i.Title);
        }
      },
      err => {
        console.log(err)
      });
  }
}


//export class TestCompletedModel {
//  Id: number;
//  TestId: number;
//  QuestCount: number;
//  AnsweredCorrectly: number;
//  TestStatus: boolean;
//  UserId: string;
//}
