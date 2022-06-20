using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ResourceApi.Models;

namespace ResourceApi.ViewModel
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public string ImgPath { get; set; }
        [Required]
        [MaxLength(1000), MinLength(3)]
        public string QuestText { get; set; }
        public string HelpText { get; set; }
        public int TestId { get; set; }
        
    }
}
