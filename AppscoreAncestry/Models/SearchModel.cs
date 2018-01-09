using AppscoreAncestry.Entities;
using System.ComponentModel.DataAnnotations;

namespace AppscoreAncestry.Models
{
    public class SearchModel
    {
        public string Name { get; set; }
        [Display(Name = "Male")]
        public bool GenderMale { get; set; }
        [Display(Name = "Female")]
        public bool GenderFemale { get; set; }
        public bool Ancestors { get; set; }
        public PersonView[] SearchResults { get; set; }
        public int pageNum { get; set; }
    }
}