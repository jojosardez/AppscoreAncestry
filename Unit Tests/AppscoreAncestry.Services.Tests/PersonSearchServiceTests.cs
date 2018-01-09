﻿using System.Linq;
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
        [TestCase(" JOBS  ", Gender.Male, 1, 10, 3)]
        [TestCase(" JoBs", Gender.Female, 1, 10, 4)]
        [TestCase("jobs", null, 1, 10, 7)]
        [TestCase("arfarf", null, 1, 10, 0)]
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


        [TestCase(" SteVE Jobs  ", Gender.Male, Ancestry.Ancestors, 1, new[] {100})]
        [TestCase(" SteVE Jobs  ", Gender.Female, Ancestry.Ancestors, 1, new[] {101})]
        [TestCase("STEVE JOBS", Gender.Male | Gender.Female, Ancestry.Ancestors, 2, new[] {100, 101})]
        [TestCase("Steve Jobs", Gender.Male, Ancestry.Descendants, 1, new[] {300})]
        [TestCase("Steve Jobs", Gender.Female, Ancestry.Descendants, 3, new[] {301, 302, 303})]
        [TestCase("  Steve Jobs  ", null, Ancestry.Descendants, 4, new[] {300, 301, 302, 303})]
        [TestCase("Isabel Tyson", null, Ancestry.Ancestors, 6, new[] {100, 101, 210, 211, 312, 313})]
        [TestCase("Isabel Tyson", null, Ancestry.Descendants, 0, new int[0])]
        public void WhenSearchingAncestry(string name, Gender? gender, Ancestry ancestry, int resultCount,
            int[] expectedIds)
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
            result = ClassUnderTest.AncestrySearch(name, gender.HasValue ? gender.Value : Gender.Male | Gender.Female,
                ancestry);
            Assert.IsNotNull(result);
            Assert.AreEqual(resultCount, result.Length);
            Assert.IsTrue(Enumerable.SequenceEqual(expectedIds, result.Select(p => p.Id)));
            // Assert
        }
    }
}
