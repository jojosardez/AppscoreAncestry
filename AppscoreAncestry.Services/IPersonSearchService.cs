using AppscoreAncestry.Entities;

namespace AppscoreAncestry.Services
{
    public interface IPersonSearchService
    {
        PersonView[] Search(string name, Gender gender, int pageNum, int pageSize = 10);
        PersonView[] AncestrySearch(string name, Gender gender, Ancestry anchestry);
    }
}
