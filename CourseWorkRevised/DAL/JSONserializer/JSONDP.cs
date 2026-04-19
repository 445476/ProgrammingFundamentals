using DAL.Entities;
using DAL.Exceptions;
using System;
using System.IO;
using System.Text.Json;

namespace DAL.Json
{
    public class JsonDataProvider
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _options;

        public JsonDataProvider(string filePath = "database.json")
        {
            _filePath = filePath;

            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

            EnsureFileExists();
        }

//cehk if the fil e exsists
        private void EnsureFileExists()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    var empty = new DataContainerEntity();
                    string json = JsonSerializer.Serialize(empty, _options);
                    File.WriteAllText(_filePath, json);
                }
            }
            catch (Exception ex)
            {
                throw new DalException("Could not create JSON storage file.", ex);
            }
        }

//desearilize data
        public DataContainerEntity Load()
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                var container = JsonSerializer.Deserialize<DataContainerEntity>(json, _options);

                return container ?? new DataContainerEntity();
            }
            catch (Exception ex)
            {
                throw new DalException("Could not load data from JSON.", ex);
            }
        }

//serialize data
        public void Save(DataContainerEntity data)
        {
            try
            {
                string json = JsonSerializer.Serialize(data, _options);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new DalException("Could not save data to JSON.", ex);
            }
        }
    }
}