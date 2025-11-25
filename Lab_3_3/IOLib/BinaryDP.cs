/*

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace IOlib
{
public class BinaryProvider : DataProvider
{
public override void Save<T>(List<T> data, string filePath)
{
using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
var bf = new BinaryFormatter();
bf.Serialize(fs, data);
}


public override List<T> Load<T>(string filePath)
{
using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
var bf = new BinaryFormatter();
return (List<T>)bf.Deserialize(fs);
}
}
}
*/