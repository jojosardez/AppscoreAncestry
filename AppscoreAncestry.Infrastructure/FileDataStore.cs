using Newtonsoft.Json;
using System.IO;

namespace AppscoreAncestry.Infrastructure
{
    public class FileDataStore<T> : IDataStore<T>
    {
        private readonly string path;

        public FileDataStore(string path)
        {
            this.path = path;
        }

        public T Get()
        {
            string content = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
