using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

// Generic JSON read/write helper
// Repositories use this instead of touching System.Text.Json or file paths directly 

namespace Evidence_Locker.Data.Context
{
    // <typeparam name="T">The entity type being persisted.</typeparam>
    public class DataStore<T> 
    {
        private readonly string _filePath;

        // WriteIndented makes the saved JSON human-readable
        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true
        };

        public DataStore(string filePath)
        {
            _filePath = filePath;
        }

        // No file yet — return an empty list rather than throwing, so the app works fine on a completely fresh machine with no data folder present
        public List<T> Load()
        {
            if (!File.Exists(_filePath))
                return new List<T>();

            string json = File.ReadAllText(_filePath);

            if (string.IsNullOrWhiteSpace(json))
                return new List<T>();

            return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new List<T>();
        }

        public void Save(List<T> items)
        {
            string json = JsonSerializer.Serialize(items, _options);
            File.WriteAllText(_filePath, json);
        }
    }
}

