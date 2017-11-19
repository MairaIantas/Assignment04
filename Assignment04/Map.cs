using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment04
{
    interface IMap<K, V>
    {
        int Size();
        Boolean IsEmpty();
        void Clear();
        V Get(K key);
        V Put(K key, V value);
        V Remove(K key);
        ICollection<K> Keys();
        ICollection<V> Values();
    }
}
