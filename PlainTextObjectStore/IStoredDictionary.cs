namespace PlainTextObjectStore;
public interface IStoredDictionary<T> : IDictionary<string, T> where T : IRecord { }
