namespace ResourceApi.Models
{
    public class TestModel
    {
        public int Id { get; set; }
        public string LogoImageName { get; set; }
        public string Title { get; set; }   
        public System.Collections.Generic.IEnumerable<Quest> Quests { get; set; }
        public bool ActiveHelps { get; set; }
        public string AvtorId { get; set; }
        

    }
}
