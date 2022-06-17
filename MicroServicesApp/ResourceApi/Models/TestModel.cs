using System.ComponentModel.DataAnnotations;

namespace ResourceApi.Models
{
    public class TestModel
    {
        public int Id { get; set; }
        public string LogoImagPath { get; set; }
        [Required]
        public string Title { get; set; }   
        public System.Collections.Generic.IEnumerable<Quest> Quests { get; set; }
        public bool ActiveHelps { get; set; }
        [Required]
        public string AvtorId { get; set; }
        public byte QuestsCount { get; set; }= 0;


    }
}
