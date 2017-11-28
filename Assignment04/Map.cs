using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment04
{
    interface IMap<K, V>
    {
        /// <summary>
        /// Return how many entries are actually used
        /// </summary>
        /// <returns></returns>
        int Size();

        /// <summary>
        /// Returns true if there are entries or false if there are none
        /// </summary>
        /// <returns></returns>
        Boolean IsEmpty();

        /// <summary>
        /// 
        /// </summary>
        void Clear();

        /// <summary>
        /// Retrieve the value associated with the key specified
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        V Get(K key);

        /// <summary>
        /// adds (or replaces) an entry
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        V Put(K key, V value);

        /// <summary>
        /// Remove the key specified
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        V Remove(K key);

        /// <summary>
        /// Returns an iterator of all keys
        /// </summary>
        /// <returns></returns>
        ICollection<K> Keys();

        /// <summary>
        /// Returns an iterator of all values
        /// </summary>
        /// <returns></returns>
        ICollection<V> Values();
    }
}
