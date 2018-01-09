using AppscoreAncestry.Controllers;
using AppscoreAncestry.Entities;
using AppscoreAncestry.Models;
using AppscoreAncestry.Services;
using Moq;
using NUnit.Framework;
using StructureMap.AutoMocking.Moq;
using System.Web.Mvc;

namespace AppscoreAncestry.Tests
{
    [TestFixture]
    public class SearchControllerTests : MoqAutoMocker<SearchController>
    {
        [Test]
        public void WhenGettingBasicSearch()
        {
            // Arrange
            ViewResult result = null;

            // Act
            result = ClassUnderTest.SearchBasic() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOf<SearchModel>(result.Model);
            Assert.IsEmpty(((SearchModel) result.Model).Name);
            Assert.IsFalse(((SearchModel) result.Model).GenderMale);
            Assert.IsFalse(((SearchModel) result.Model).GenderFemale);
            Assert.AreEqual(1, ((SearchModel) result.Model).pageNum);
            Assert.IsNotNull(((SearchModel) result.Model).SearchResults);
            Assert.AreEqual(0, ((SearchModel) result.Model).SearchResults.Length);
        }

        [TestCase(1, null)]
        [TestCase(2, "2")]
        public void WhenPostingBasicSearch(int expectedPageNo, string pageNumber)
        {
            // Arrange
            Mock.Get(Get<IPersonSearchService>())
                .Setup(svc => svc.Search(It.IsAny<string>(), It.IsAny<Gender>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new[]
                {
                    new PersonView
                    {
                        Id = 1,
                        Name = "test",
                        Gender = Gender.Male,
                        BirthPlace = "test"
                    }
                });
            ViewResult result = null;
            SearchModel model = new SearchModel
            {
                Name = "test",
                GenderMale = true,
                GenderFemale = true,
                pageNum = 1
            };

            // Act
            result = ClassUnderTest.SearchBasic(model, pageNumber) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOf<SearchModel>(result.Model);
            Assert.AreEqual(model.Name, ((SearchModel) result.Model).Name);
            Assert.AreEqual(model.GenderMale, ((SearchModel) result.Model).GenderMale);
            Assert.AreEqual(model.GenderFemale, ((SearchModel) result.Model).GenderFemale);
            Assert.AreEqual(expectedPageNo, ((SearchModel) result.Model).pageNum);
            Assert.IsNotNull(((SearchModel) result.Model).SearchResults);
            Assert.AreEqual(1, ((SearchModel) result.Model).SearchResults.Length);
        }

        [Test]
        public void WhenGettingAdvanceSearch()
        {
            // Arrange
            ViewResult result = null;

            // Act
            result = ClassUnderTest.SearchAdvance() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOf<SearchModel>(result.Model);
            Assert.IsEmpty(((SearchModel) result.Model).Name);
            Assert.IsFalse(((SearchModel) result.Model).GenderMale);
            Assert.IsFalse(((SearchModel) result.Model).GenderFemale);
            Assert.IsTrue(((SearchModel) result.Model).Ancestors);
            Assert.IsNotNull(((SearchModel) result.Model).SearchResults);
            Assert.AreEqual(0, ((SearchModel) result.Model).SearchResults.Length);
        }

        [Test]
        public void WhenPostingAdvanceSearch()
        {
            // Arrange
            Mock.Get(Get<IPersonSearchService>())
                .Setup(svc => svc.AncestrySearch(It.IsAny<string>(), It.IsAny<Gender>(), It.IsAny<Ancestry>()))
                .Returns(new[]
                {
                    new PersonView
                    {
                        Id = 1,
                        Name = "test",
                        Gender = Gender.Male,
                        BirthPlace = "test"
                    }
                });
            ViewResult result = null;
            SearchModel model = new SearchModel
            {
                Name = "test",
                GenderMale = true,
                GenderFemale = true,
                Ancestors = false
            };

            // Act
            result = ClassUnderTest.SearchAdvance(model) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOf<SearchModel>(result.Model);
            Assert.AreEqual(model.Name, ((SearchModel) result.Model).Name);
            Assert.AreEqual(model.GenderMale, ((SearchModel) result.Model).GenderMale);
            Assert.AreEqual(model.GenderFemale, ((SearchModel) result.Model).GenderFemale);
            Assert.AreEqual(model.Ancestors, ((SearchModel) result.Model).Ancestors);
            Assert.IsNotNull(((SearchModel) result.Model).SearchResults);
            Assert.AreEqual(1, ((SearchModel) result.Model).SearchResults.Length);
        }
    }
}
