import { Injectable } from '@angular/core';
import { NgForm } from "@angular/forms";
import { Quest } from '../shared/Models/quest.model';
import { HttpClient } from "@angular/common/http"
import { LeftAnswer } from '../shared/Models/left-answer.model';


@Injectable({
  providedIn: 'root'
})
export class QuestService {

  constructor(private httpClient: HttpClient) { }

  FormData: Quest = new Quest();
  Quests: Quest[];

  Create(TestId: number, LeftAnsvers: string[]) {
    
    var data = this.FormData;
    data.LeftAnswers = LeftAnsvers;
    console.log(data);
    console.log(LeftAnsvers);

    return this.httpClient.post(`http://localhost:9410/api/Quests/${TestId}`, data);
  }

  Update(TestId: number) {
    if (this.FormData.Id > 0) {
      //return this.httpClient.post(``)
    }
  }

  Remove(QuestId: number) {

  }

  GetQuestsByTestId(TestId: number) {
    return this.httpClient.get(`http://localhost:9410/api/Quests/${TestId}`).subscribe(
      res => {
        this.Quests = res as Quest[];
        console.log(res as Quest[])
      },
      err => {
        console.log(err);
      })
  }
}
