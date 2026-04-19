using System.Collections.Generic;

//generic сollection for doctors and patients, i decided to not create base class for them as they only contain 3 shared fields

namespace BLL.Collections
{
    public class GenericCollection<T>
    {
        //using dictionary that will allow us to easily search everything by ID
        private readonly Dictionary<int, T> _items = new();

        public int Add(int id, T item)
        {
            _items[id] = item;
            return id;
        }

        public bool Remove(int id)
        {
            return _items.Remove(id);
        }

        public T? Get(int id)
        {
            _items.TryGetValue(id, out var result);
            return result;
        }

        //returns every entry
        public IEnumerable<KeyValuePair<int, T>> GetAll()
        {
            return _items;
        }
    }
}