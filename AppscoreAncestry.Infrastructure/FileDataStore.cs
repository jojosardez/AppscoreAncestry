using Newtonsoft.Json;
using System.IO;

namespace AppscoreAncestry.Infrastructure
{
    public class FileDataStore : IDataStore
    {
        private readonly string path;

        public FileDataStore(string path)
        {
            this.path = path;
        }

        public T Get<T>()
        {
            string content = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
