import { Injectable } from '@angular/core';
import { NgForm } from "@angular/forms";
import { Quest, Quest2 } from '../shared/Models/quest.model';
import { HttpClient } from "@angular/common/http"
import { LeftAnswer } from '../shared/Models/left-answer.model';


@Injectable({
  providedIn: 'root'
})
export class QuestService {

  constructor(private httpClient: HttpClient) { }

  FormData: Quest = new Quest();
  Quests: Quest2[];

  Create(TestId: number, LeftAnsvers: string[]) {
    
    var data = this.FormData;
    data.LeftAnswers = LeftAnsvers;
    return this.httpClient.post(`http://localhost:9410/api/Quests/${TestId}`, data);
  }

  Update(QuestId: number, LeftAnsvers: string[]) {

    var data = this.FormData;
    data.LeftAnswers = LeftAnsvers;
    if (this.FormData.Id > 0) {
      return this.httpClient.patch(`http://localhost:9410/api/Quests/${QuestId}`, data);
    }
    return null;
  }

  Remove(QuestId: number) {
    return this.httpClient.delete(`http://localhost:9410/api/Quests/${QuestId}`)
  }

  GetQuestsByTestId(TestId: number) {
    return this.httpClient.get(`http://localhost:9410/api/Quests/${TestId}`).subscribe(
      res => {
        this.Quests = res as Quest2[];
        console.log(res as Quest2[])
      },
      err => {
        console.log(err);
      })
  }
}
