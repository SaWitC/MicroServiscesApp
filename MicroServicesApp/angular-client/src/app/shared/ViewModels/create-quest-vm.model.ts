export class CreateQuestVM {
  id: number;
  quest: {
    ImgPath: string;
    QuestText: string;
    HelpText:string;
  }
}


//Quest quest, int ? id


//public string ImgPath { get; set; }
//[Required]
//  [MaxLength(1000), MinLength(3)]
//public string QuestText { get; set; }
//public string HelpText { get; set; }

//[ForeignKey("test")]
//public int TestId { get; set; }    
//public TestModel test { get; set; }
