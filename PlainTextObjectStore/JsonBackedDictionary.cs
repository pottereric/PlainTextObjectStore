using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace PlainTextObjectStore;
public class JsonBackedDictionary<T> : IStoredDictionary<T> where T: IRecord
{
    private Dictionary<string, T> _records = new();
    private JsonObjectWriter<T> _fileWriter;
    private string _folderName;

    public ICollection<string> Keys => _records.Keys;
    public ICollection<T> Values => _records.Values;
    public int Count => _records.Count;
    public bool IsReadOnly => false;

    public JsonBackedDictionary(string folderName)
    {
        _folderName = folderName;
        _fileWriter = new JsonObjectWriter<T>(_folderName);
        _records = _fileWriter.ReadAll().ToDictionary(r => r.Key);
    }

    public T this[string key]
    {
        get => _records[key];
        set
        {
            _records[key] = value;
            _fileWriter.Write(value);
        }
    }

    public void Add(T value) => Add(value.Key, value);

    public void Add(string key, T value)
    {
        _records.Add(key, value);
        _fileWriter.Write(value);
    }

    public bool ContainsKey(string key) => _records.ContainsKey(key);

    public bool Remove(string key)
    {
        T item = _records[key];
        if (_records.Remove(key))
        {
            _fileWriter.Delete(item);
            return true;
        }
        return false;
    }

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out T value) => _records.TryGetValue(key, out value);
    public void Add(KeyValuePair<string, T> item) => this.Add(item.Key, item.Value);
    public bool Contains(KeyValuePair<string, T> item) => _records.Contains(item);

    public void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex) 
        => ((ICollection)_records).CopyTo(array, arrayIndex);

    public bool Remove(KeyValuePair<string, T> item) => this.Remove(item.Key);


    public void Clear()
    {
        foreach (var item in _records.Values)
        {
            _fileWriter.Delete(item);
        }
    }

    IEnumerator<KeyValuePair<string, T>> IEnumerable<KeyValuePair<string, T>>.GetEnumerator() => _records.GetEnumerator();
    public IEnumerator GetEnumerator() => _records.GetEnumerator();

}

