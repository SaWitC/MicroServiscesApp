using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceApi.Models
{
    public class Quest
    {
        [Key]
        public int Id { get; set; }  
        public string ImgPath { get; set; }
        [Required]
        [MaxLength(1000),MinLength(3)]
        public string QuestText { get; set; }
        [Required]
        [MaxLength(100),MinLength(1)]
        public string Right_answer { get; set; }
        public List<LeftAnswer> LeftAnswers { get; set; }
        public string HelpText { get; set; }
        
        [ForeignKey("test")]
        public int TestId { get; set; }    
        public TestModel test { get; set; }

        //public bool IsUsed { get; set; }
    }
}
