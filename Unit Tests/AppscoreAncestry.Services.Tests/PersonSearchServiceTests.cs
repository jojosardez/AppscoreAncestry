using AppscoreAncestry.Entities;
using AppscoreAncestry.Infrastructure;
using AppscoreAncestry.Services.Tests.TestData;
using Moq;
using NUnit.Framework;
using StructureMap.AutoMocking.Moq;

namespace AppscoreAncestry.Services.Tests
{
    [TestFixture()]
    public class PersonSearchServiceTests : MoqAutoMocker<PersonSearchService>
    {
        [TestCase("", Gender.Male, 1, 5, 5)]
        [TestCase("", Gender.Male, 2, 5, 0)]
        [TestCase("", Gender.Female, 1, 5, 5)]
        [TestCase("", Gender.Female, 2, 5, 5)]
        [TestCase("", Gender.Female, 3, 5, 0)]
        [TestCase("JOBS", Gender.Male, 1, 10, 3)]
        [TestCase("JoBs", Gender.Female, 1, 10, 4)]
        [TestCase("jobs", null, 1, 10, 7)]
        public void WhenSearchingPeople(string name, Gender? gender, int pageNum, int pageSize, int resultCount)
        {
            // Arrange
            Mock.Get(Get<IDataStore<Place[]>>())
                .Setup(ds => ds.Get())
                .Returns(TestPlaces.GetPlaces());
            Mock.Get(Get<IDataStore<Person[]>>())
                .Setup(ds => ds.Get())
                .Returns(TestPeople.GetPeople());
            PersonView[] result = null;

            // Act
            result = ClassUnderTest.Search(name, gender.HasValue ? gender.Value : Gender.Male | Gender.Female, pageNum, pageSize);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(resultCount, result.Length);
        }
    }
}
