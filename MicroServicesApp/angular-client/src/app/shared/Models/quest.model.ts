import { LeftAnswer } from "./left-answer.model";

export class Quest {

  Id: number = 0;
  ImgPath: string = "";
  QuestText: string = "";
  HelpText: string;
  TestId: number = 0;
  Right_answer: string;
  LeftAnswers: string[]
}


//public int Id { get; set; }  
//public string ImgPath { get; set; }
//[MaxLength(1000), MinLength(3)]
//public string QuestText { get; set; }
//public string HelpText { get; set; }

//[ForeignKey("test")]
//public int TestId { get; set; }    
//public TestModel test { get; set; }
