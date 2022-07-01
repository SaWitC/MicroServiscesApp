using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceApi.Models
{
    public class simpleModel
    {
        public int Id { get; set; }
        public string LogoImagPath { get; set; }
        [Required]
        [MaxLength(100), MinLength(3)]
        public string Title { get; set; }
       // public System.Collections.Generic.IEnumerable<Quest> Quests { get; set; }
        public bool ActiveHelps { get; set; }
        public string AvtorId { get; set; }
        public int QuestsCount { get; set; }
        [Required]
        [MaxLength(2000), MinLength(200)]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
