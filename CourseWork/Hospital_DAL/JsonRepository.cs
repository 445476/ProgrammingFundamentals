using Hospital_BLL.Interfaces;
using System.Text.Json;

namespace Hospital_DAL
{
    public class JsonRepository<T> : IRepository<T> where T : class
    {
        // filename is determined by T
        private readonly string _filePath;

        public JsonRepository(string filename)
        {
            // creating data folder
            string dataPath = "Data";
            Directory.CreateDirectory(dataPath);
            _filePath = Path.Combine(dataPath, filename);
        }

        private List<T> LoadAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();
            }
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                // desearialization
                return JsonSerializer.Deserialize<List<T>>(jsonString) ?? new List<T>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing data from {_filePath}: {ex.Message}");
                return new List<T>();
            }
        }

        private void SaveAll(List<T> items)
        {
            // converting list to JSON
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(items, options);

            // writing to file
            File.WriteAllText(_filePath, jsonString);
        }

        public T GetById(Guid id)
        {
            return LoadAll().FirstOrDefault(item =>
                (Guid)item.GetType().GetProperty("Id")?.GetValue(item) == id);
        }

        public IEnumerable<T> GetAll()
        {
            return LoadAll();
        }

        public void Add(T item)
        {
            List<T> items = LoadAll();

            var idProp = item.GetType().GetProperty("Id");
            if (idProp != null)
            {
                Guid newId = (Guid)idProp.GetValue(item);
                if (items.Any(i => (Guid)i.GetType().GetProperty("Id")?.GetValue(i) == newId))
                {
                    throw new InvalidOperationException($"Item with ID {newId} already exists.");
                }
            }

            items.Add(item);
            SaveAll(items);
        }

        public void Update(T item)
        {
            List<T> items = LoadAll();
            var idProp = item.GetType().GetProperty("Id");
            if (idProp == null) return;

            Guid itemId = (Guid)idProp.GetValue(item);
            int index = items.FindIndex(i => (Guid)i.GetType().GetProperty("Id")?.GetValue(i) == itemId);

            if (index != -1)
            {
                items[index] = item; 
                SaveAll(items);
            }
            else
            {
                throw new ArgumentException($"Item with ID {itemId} not found for update.");
            }
        }

        public void Delete(Guid id)
        {
            List<T> items = LoadAll();
            int initialCount = items.Count;

            items.RemoveAll(item =>
                (Guid)item.GetType().GetProperty("Id")?.GetValue(item) == id);

            if (items.Count == initialCount)
            {
                throw new ArgumentException($"Item with ID {id} not found for deletion.");
            }

            SaveAll(items);
        }
    }
}
