using System.Text.Json;

namespace PlainTextObjectStore;
public class JsonObjectWriter<T> where T: IRecord
{
    private string _folderName;

    private string FullFileName(string key) =>
        Path.Combine(_folderName, $"{key}.json");

    public JsonObjectWriter(string folderName)
    {
        _folderName = folderName;

        if(!Directory.Exists(_folderName))
            Directory.CreateDirectory(_folderName);
    }

    public void Write(T record)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(record, options);
        File.WriteAllText(FullFileName(record.Key), jsonString);
    }

    //public T Read(string key)
    //{
    //    string fullName = Path.Combine(_folderName, $"{key}.json");
    //    T record = JsonSerializer.Deserialize<T>(fullName) ?? default(T);
    //}

    private bool IsJsonFile(string fileName)
    {
        var fileInfo = new FileInfo(fileName);
        return fileInfo.Extension.ToLower() == "json";
    }

    public IEnumerable<T> ReadAll()
    {
        List<T> allRecords = new();

        var fileNames = Directory.EnumerateFiles(_folderName).Where(fn => IsJsonFile(fn));

        foreach (var fileName in fileNames)
        {
            var recordText = File.ReadAllText(fileName);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            T record = JsonSerializer.Deserialize<T>(recordText);
            allRecords.Add(record);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }

        return allRecords;
    }

    public void Delete(T record)
    {
        // TODO - check if file exists
        File.Delete(FullFileName(record.Key));
    }
}
