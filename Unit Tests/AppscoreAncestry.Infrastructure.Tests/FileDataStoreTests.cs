using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace AppscoreAncestry.Infrastructure.Tests
{
    [TestFixture]
    public class FileDataStoreTests
    {
        [TestCase(true, "Valid.json", 12345, "Test")]
        [TestCase(false, "Invalid.json", 0, null)]
        public void WhenGettingFromDataStore(bool isValid, string fileName, int id, string name)
        {
            // Arrange
            CreateTextFile(isValid);
            FileDataStore<Entity> dataStore = new FileDataStore<Entity>(fileName);
            Entity entity = null;
            Exception exception = null;

            // Act
            entity = dataStore.Get();

            // Assert
            Assert.IsNotNull(entity);
            Assert.AreEqual(id, entity.Id);
            Assert.AreEqual(name, entity.Name);
        }

        [Test]
        public void WhenFileDoesNotExists()
        {
            // Arrange
            FileDataStore<Entity> dataStore = new FileDataStore<Entity>("DoesNotExists.json");
            Entity entity = null;
            Exception exception = null;

            // Act
            exception = Assert.Catch(() =>
            {
                entity = dataStore.Get();
            });

            // Assert
            Assert.IsNotNull(exception);
        }

        private void CreateTextFile(bool isValid)
        {
            using (var fs = new FileStream(isValid ? "Valid.json" : "Invalid.json", FileMode.Create))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(isValid ? "{\"Id\": 12345,\"Name\": \"Test\"}" : "{\"aa\": \"bb\",\"cc\": 1}");
                sw.Flush();
            }
        }
    }
}
