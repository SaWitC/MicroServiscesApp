using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceApi.ViewModel
{
    public class CreateQuestVM
    {
        public string ImgPath { get; set; }
        [Required]
        [MaxLength(1000), MinLength(3)]
        public string QuestText { get; set; }
        [Required]
        [MaxLength(100), MinLength(1)]
        public string Right_answer { get; set; }
        public List<string> LeftAnswers { get; set; }
        public string HelpText { get; set; }
    }
}
