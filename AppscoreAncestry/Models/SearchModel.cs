using System.ComponentModel.DataAnnotations;
using AppscoreAncestry.Entities;

namespace AppscoreAncestry.Models
{
    public class SearchModel
    {
        public string Name { get; set; }
        [Display(Name = "Male")]
        public bool GenderMale { get; set; }
        [Display(Name = "Female")]
        public bool GenderFemale { get; set; }
        [Display(Name = "Ancestors")]
        public bool AncestryAncestors { get; set; }
        [Display(Name = "Descendants")]
        public bool AncestryDescendants { get; set; }
        public PersonView[] SearchResults { get; set; }
        public int pageNum { get; set; }
    }
}