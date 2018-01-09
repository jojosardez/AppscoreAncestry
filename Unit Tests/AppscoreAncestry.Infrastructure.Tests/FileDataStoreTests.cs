using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace AppscoreAncestry.Infrastructure.Tests
{
    [TestFixture]
    public class FileDataStoreTests
    {
        [TestCase("Valid.json", 12345, "Test")]
        [TestCase("Invalid.json", 0, null)]
        public void WhenGettingFromDataStore(string fileName, int id, string name)
        {
            // Arrange
            string path =
                (new FileInfo((new Uri(Assembly.GetAssembly(typeof(FileDataStoreTests)).CodeBase).AbsolutePath)))
                .Directory.FullName;
            path = path.Replace("%20", " ");
            FileDataStore dataStore = new FileDataStore(path + "\\TestData\\" + fileName);
            Entity entity = null;
            Exception exception = null;

            // Act
            entity = dataStore.Get<Entity>();

            // Assert
            Assert.IsNotNull(entity);
            Assert.AreEqual(id, entity.Id);
            Assert.AreEqual(name, entity.Name);

        }

        [Test]
        public void WhenFileDoesNotExists()
        {
            // Arrange
            string path =
                (new FileInfo((new Uri(Assembly.GetAssembly(typeof(FileDataStoreTests)).CodeBase).AbsolutePath)))
                .Directory.FullName;
            path = path.Replace("%20", " ");
            FileDataStore dataStore = new FileDataStore(path + "\\TestData\\DoesNotExists.json");
            Entity entity = null;
            Exception exception = null;

            // Act
            exception = Assert.Catch(() =>
            {
                entity = dataStore.Get<Entity>();
            });

            // Assert
            Assert.IsNotNull(exception);
        }
    }
}
