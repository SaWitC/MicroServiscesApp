using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceApi.Models
{
    public class LeftAnswer
    {
        [Required]
        public int Id { get; set; }
        public int QuestId { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }

        public simpleModel model {get;set;}
    }
}
