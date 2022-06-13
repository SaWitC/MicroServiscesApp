////[Key]
////        public int Id { get; set; }  
////        public string ImgPath { get; set; }
////[MaxLength(1000), MinLength(3)]
////        public string QuestText { get; set; }
////        public string HelpText { get; set; }

////[ForeignKey("test")]
////        public int TestId { get; set; }    
////        public TestModel test { get; set; }

export class Quest {
  id: number=0;
  imgPath: string="";
  QuestText: string="";
  HelpText: string="";
  TestId: number=0;
}
