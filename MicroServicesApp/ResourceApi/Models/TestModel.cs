using System;
using System.ComponentModel.DataAnnotations;

namespace ResourceApi.Models
{
    public class TestModel
    {
        public TestModel(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
        public string LogoImagPath { get; set; }
        [Required]
        [MaxLength(100),MinLength(3)]
        public string Title { get; set; }   
        public System.Collections.Generic.IEnumerable<Quest> Quests { get; set; }
        public bool ActiveHelps { get; set; }
        public string AvtorId { get; set; }
        public int QuestsCount { get; set; }
        [Required]
        [MaxLength(2000),MinLength(200)]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
