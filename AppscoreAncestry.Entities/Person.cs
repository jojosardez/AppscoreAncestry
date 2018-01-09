namespace AppscoreAncestry.Entities
{
    public class Person
    {
        public int id { get; set; }
        public string name { get; set; }
        public char gender { get; set; }
        public int? father_id { get; set; }
        public int? mother_id { get; set; }
        public int place_id { get; set; }
        public int level { get; set; }
    }
}
