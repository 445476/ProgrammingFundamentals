using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;
using System;
using Models.Lab3.Core;
using Models;

namespace IOLib

{
    public class FileRepository : IRepository<Person> // Використовуємо IRepository<Person> для роботи з усіма Person
        {
        // === МЕТОД SAVE (OCP: Не знає про поля Student чи Dentist) ===
        public void Save(Person[] persons, string path)
        {
            // Використовуємо потоки для роботи з файлами (вимога 1)
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var sw = new StreamWriter(fs, Encoding.UTF8))
            {
                foreach (var p in persons)
                {
                    if (p == null) continue;

                    // 1. Заголовок (ТипНазва)
                    string header = $"{p.GetType().Name} {SanitizeName(p.FirstName + p.LastName)}";

                    sw.WriteLine(header);
                    sw.WriteLine("{");

                    // 2. Серіалізація: Поліморфний виклик (OCP)
                    sw.Write(p.ToPersistenceString());

                    sw.WriteLine("};");
                }
            }
        }

        // === МЕТОД LOAD (Залишається складним через factory-логіку) ===
        public Person[] Load(string path)
        {
            if (!File.Exists(path)) return new Person[0];

            var lines = File.ReadAllLines(path, Encoding.UTF8);

            // Використання масиву для результату (вимога: не використовувати колекції)
            Person[] result = new Person[0];

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                if (string.IsNullOrEmpty(line) || line == "{") continue;

                // 1. Читання заголовка ("Student VasiaPupkin")
                var headerMatch = Regex.Match(line, @"^(?<type>\w+)\s+(?<name>\w+)$");
                if (!headerMatch.Success) continue;
                string type = headerMatch.Groups["type"].Value;

                // 2. Читання блоку атрибутів до "};"
                i++; // Переходимо на рядок після "{"
                var attrLines = new List<string>(); // *Тимчасова* колекція для парсингу блоку
                while (i < lines.Length && !lines[i].Trim().EndsWith("};"))
                {
                    attrLines.Add(lines[i].Trim());
                    i++;
                }
                if (i < lines.Length && lines[i].Trim().EndsWith("};"))
                {
                    var last = lines[i].Trim();
                    var before = last.Substring(0, last.Length - 2).Trim();
                    if (!string.IsNullOrEmpty(before)) attrLines.Add(before);
                }

                // 3. Парсинг атрибутів у словник (для зручного доступу)
                var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (var l in attrLines)
                {
                    // Regex для формату "key": "value", або "key": "value"
                    var m = Regex.Match(l, @"^\s*""(?<k>[^""]+)""\s*:\s*""(?<v>[^""]+)""\s*,?\s*$");
                    if (m.Success) dict[m.Groups["k"].Value] = m.Groups["v"].Value;
                }

                // 4. Створення об'єкта (Factory logic)
                Person newPerson = null;

                // Всі об'єкти вимагають fn, ln, які ми отримуємо зі словника
                dict.TryGetValue("firstname", out string fn);
                dict.TryGetValue("lastname", out string ln);
                dict.TryGetValue("gender", out string gender);
                Gender gn = Gender.Male;
                Enum.TryParse<Gender>(gender, true, out gn);

                try
                {
                    if (string.Equals(type, "Student", StringComparison.OrdinalIgnoreCase))
                    {
                        // ... парсинг усіх полів ...
                        dict.TryGetValue("course", out string courseS);
                        dict.TryGetValue("studentId", out string studentId);
                        dict.TryGetValue("gradeAvg", out string gradeAvgS);
                        dict.TryGetValue("idCode", out string idCode);

                        if (int.TryParse(courseS, out int course) &&
                            double.TryParse(gradeAvgS, out double gradeAvg)) 
                        {
                            newPerson = new Student(fn, ln, course, studentId, gn, gradeAvg, idCode);
                        }
                    }
                    else if (string.Equals(type, "Storyteller", StringComparison.OrdinalIgnoreCase))
                    {
                        dict.TryGetValue("speciality", out string spec);
                        newPerson = new Storyteller(fn, ln, spec, gn);
                    }
                    else if (string.Equals(type, "Dentist", StringComparison.OrdinalIgnoreCase))
                    {
                        dict.TryGetValue("patientsTreated", out string ptS);
                        if (int.TryParse(ptS, out int pt))
                            newPerson = new Dentist(fn, ln, int.Parse(ptS), gn);
                    }
                }
                catch (ArgumentException ex)
                {
                    // Обробка помилок валідації при створенні об'єкта (некоректні дані у файлі)
                    Console.WriteLine($"Error loading {type}: {ex.Message}. Record skipped.");
                }


                if (newPerson != null)
                {
                    // Допоміжний метод Append<T> для розширення масиву
                    result = Append(result, newPerson);
                }
            }

            return result;
        }

        // Helper: append to array (no collections)
        private static Person[] Append(Person[] arr, Person item)
        {
            var newArr = new Person[arr.Length + 1];
            for (int i = 0; i < arr.Length; i++) newArr[i] = arr[i];
            newArr[arr.Length] = item;
            return newArr;
        }

        private static string SanitizeName(string s)
        {
            if (string.IsNullOrEmpty(s)) return "Obj";
            return Regex.Replace(s, @"\s+", "");
        }
    }
}