using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Text.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace Program
{
    // Creating class 
    public class Product : IComparable<Product> //ISerializable
    {
        public string Name { get; set; }
        public string Code { get; set; } 
        public DateTime ManufactureDate { get; set; }
        public int ShelfLifeDays { get; set; } // how much days it can go before expire

        // default constructor
        public Product() { }

        //parametrized constructor
        public Product(string name, string code, DateTime manufactureDate, int shelfLifeDays)
        {
            Name = name;
            Code = code;
            ManufactureDate = manufactureDate;
            ShelfLifeDays = shelfLifeDays;
        }


        // finding the date at which the product will expire
        public DateTime GetExpirationDate()
        {
            return ManufactureDate.AddDays(ShelfLifeDays);
        }

        // check if expired, either take the date from class or take the date from today and compare to expdate
        public bool IsConsumable(DateTime? atDate = null)
        {
            var check = atDate ?? DateTime.Today;
            return check.Date <= GetExpirationDate().Date;
        }

        // ovrriding tostring so compiler correctly shows objects
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture,
                "Product(Name={0}, Code={1}, Mfg={2:yyyy-MM-dd}, ShelfLife={3}d, Exp={4:yyyy-MM-dd})",
                Name, Code, ManufactureDate, ShelfLifeDays, GetExpirationDate());
        }

        // IComparable realization
        public int CompareTo(Product other)
        {
            if (other == null) return 1;
            return string.Compare(this.Code, other.Code, StringComparison.OrdinalIgnoreCase);
        }

        // method for finding product by code
        public bool HasCode(string code) => string.Equals(Code, code, StringComparison.OrdinalIgnoreCase);
              // user custom serialization
       public string ToCustomString()
{
    return $"{Name}|{Code}|{ManufactureDate.Ticks}|{ShelfLifeDays}";
}

public static Product FromCustomString(string line)
{
    string[] parts = line.Split('|');

    return new Product(
        parts[0],
        parts[1],
        new DateTime(long.Parse(parts[2])),
        int.Parse(parts[3])
    );

}

    }
    public class Program 
    {
    static void Main()
    {
        Product[] original =
        {
            new Product("Bread", "1", DateTime.Today.AddDays(-1), 3),
            new Product("Milk", "2", DateTime.Today.AddDays(-2), 5),
            new Product("Eggs", "3", DateTime.Today.AddDays(-2), 5)
        };

        List<Product> originalList = new List<Product>(original);

        Console.WriteLine("=== ORIGINAL DATA ===");
        Print(original);


        // BIN
      //  BinaryFormatter bf = new BinaryFormatter();
      //  using (FileStream fs = new FileStream("bin.dat", FileMode.Create))
      //      bf.Serialize(fs, original);

      //  Product[] restoredBinary;
      //  using (FileStream fs = new FileStream("bin.dat", FileMode.Open))
     //       restoredBinary = (Product[])bf.Deserialize(fs);

     //   Print(restoredBinary);
     //   Compare(original, restoredBinary);


// XML
        XmlSerializer xml = new XmlSerializer(typeof(Product[]));
        using (FileStream fs = new FileStream("xml.xml", FileMode.Create))
            xml.Serialize(fs, original);

        Product[] restoredXml;
        using (FileStream fs = new FileStream("xml.xml", FileMode.Open))
            restoredXml = (Product[])xml.Deserialize(fs);

        Print(restoredXml);
        Compare(original, restoredXml);

//JSON
        string json = JsonSerializer.Serialize(original, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("json.json", json);

        Product[] restoredJson =
            JsonSerializer.Deserialize<Product[]>(File.ReadAllText("json.json"));

        Print(restoredJson);
        Compare(original, restoredJson);

//custom
        using (StreamWriter sw = new StreamWriter("custom.txt"))
{
    foreach (var p in originalList)
        sw.WriteLine(p.ToCustomString());
}

List<Product> restoredCustomText = new List<Product>();

using (StreamReader sr = new StreamReader("custom.txt"))
{
    string line;
    while ((line = sr.ReadLine()) != null)
        restoredCustomText.Add(Product.FromCustomString(line));
}

Print(restoredCustomText);
Compare(originalList, restoredCustomText);

    static void Print(IEnumerable<Product> arr)
    {
        foreach (var p in arr)
            Console.WriteLine(p);
    }


static void Compare(ICollection<Product> a, ICollection<Product> b)
    {
        bool equal = true;

        if (a.Count != b.Count)
            equal = false;
        else
        {
            var e1 = a.GetEnumerator();
            var e2 = b.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext())
            {
                if (e1.Current.Name != e2.Current.Name ||
                    e1.Current.Code != e2.Current.Code ||
                    e1.Current.ManufactureDate != e2.Current.ManufactureDate ||
                    e1.Current.ShelfLifeDays != e2.Current.ShelfLifeDays)
                {
                    equal = false;
                    break;
                }
            }
        }

        Console.WriteLine(equal ? "Same" : "Not same");
    }
    }
    }
    }