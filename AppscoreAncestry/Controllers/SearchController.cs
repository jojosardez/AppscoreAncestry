﻿using AppscoreAncestry.Entities;
using AppscoreAncestry.Models;
using AppscoreAncestry.Services;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult SearchBasic()
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
        public IActionResult SearchBasic(SearchModel model, string pageNumber)
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

        [HttpGet]
        public IActionResult SearchAdvance()
        {
            var model = new SearchModel
            {
                Name = string.Empty,
                Ancestors = true,
                SearchResults = new PersonView[0]
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult SearchAdvance(SearchModel model)
        {
            model.SearchResults = service.AncestrySearch(
                model.Name,
                GetSelectedGender(model.GenderMale, model.GenderFemale),
                model.Ancestors ? Ancestry.Ancestors : Ancestry.Descendants);
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