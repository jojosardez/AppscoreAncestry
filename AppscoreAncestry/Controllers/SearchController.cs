using AppscoreAncestry.Entities;
using AppscoreAncestry.Models;
using AppscoreAncestry.Services;
using System.Web.Mvc;

namespace AppscoreAncestry.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPersonSearchService service;

        public SearchController(IPersonSearchService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult SearchBasic()
        {
            var model = new SearchModel
            {
                Name = string.Empty,
                pageNum = 1,
                SearchResults = new PersonView[0]
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult SearchBasic(SearchModel model, string pageNumber)
        {
            if (!string.IsNullOrEmpty(pageNumber))
            {
                model.pageNum = int.Parse(pageNumber);
            }

            model.SearchResults = service.Search(
                model.Name,
                GetSelectedGender(model.GenderMale, model.GenderFemale),
                model.pageNum);
            return View(model);
        }

        private Gender GetSelectedGender(bool male, bool female)
        {
            if ((male && female) || (!male && !female))
                return Gender.Male | Gender.Female;
            return male ? Gender.Male : Gender.Female;
        }
    }
}