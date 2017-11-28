using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment04
{
    public class HashMap<K, V> : IMap<K, V>
    {
        /// <summary>
        /// 
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// Value greater 0 and less than 1 that indicate how full the HashMap can become before being extended
        /// </summary>
        public double LoadFactor { get; set; }

        /// <summary>
        /// How many entries can be used before the HashMap is extended
        /// Calculated by current table size* loadFactor
        /// </summary>
        public int Threshold { get; set; }

        /// <summary>
        /// Sentinel ‘Entry K, V>’
        /// Used when removing entries to flag spots that used to have entries
        /// Consists of a null Key and null Value
        /// </summary>
        public Entry<K, V> Available { get; set; }

        /// <summary>
        /// Size of HashMap array if one not specified
        /// </summary>
        const int DEFAULT_CAPACITY = 11;

        /// <summary>
        /// Default factor specifying the percent of entries that 
        /// can be used before extending the array
        /// </summary>
        const double DEFAULT_LOADFACTOR = 0.75;

        /// <summary>
        /// An array
        /// The actual hashmap itself
        /// Consists of Entry K V 
        /// Set to the DEFAULT_CAPACITY or specified capacity
        /// </summary>
        public Entry<K, V>[] table;

        /// <summary>
        /// Constructor
        /// </summary>
        public HashMap()
        {
            this.LoadFactor = DEFAULT_LOADFACTOR;
            this.Threshold = DEFAULT_CAPACITY * (Int32)DEFAULT_LOADFACTOR;
            table = new Entry<K, V>[DEFAULT_CAPACITY];
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public HashMap(int initialCapacity)
        {
            this.LoadFactor = DEFAULT_LOADFACTOR;
            this.Threshold = initialCapacity * (Int32)DEFAULT_LOADFACTOR;
            table = new Entry<K, V>[initialCapacity];
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public HashMap(int initialCapacity, double loadFactor)
        {
            if (!(loadFactor > 0 && loadFactor < 1))
            {
                throw new ArgumentException("Load Factor can't be less than 0 or greater than 1.");
            }

            this.LoadFactor = loadFactor;
            this.Threshold = initialCapacity * (Int32)loadFactor;
            table = new Entry<K, V>[initialCapacity];
        }

        /// <summary>
        /// Contains a the number of used HashMap entries
        /// Incremented on a put(key, value) for a new entry
        /// Decremented on a successful remove(key)
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return this.size;
        }

        /// <summary>
        /// Returns if the HashTable is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return Size() <= 0;
        }

        /// <summary>
        /// Sets all entries in the HashMap to null
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = null;
            }
            this.size = 0;
        }

        /// <summary>
        /// Gets the value associated with the passed key
        /// </summary>
        /// <param name="key"></param>
        /// <returns> Returns null if the passed key not in the HashMap</returns>
        public V Get(K key)
        {
            return findMatchingBucket(key) >= 0 ? table[findMatchingBucket(key)].Value : default(V);
        }

        /// <summary>
        /// Attempts to either add a new entry or replace the key already on the HashMap
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public V Put(K key, V value)
        {
            if (key == null || value == null)
            {
                throw new ArgumentNullException("Key and Value cannot be null.");
            }

            if (size >= Threshold)
            {
                rehash();
            }

            V removed = Remove(key);

            Entry<K, V> entry = new Entry<K, V>(key, value);

            table[findBucket(key)] = entry;

            size++;

            return entry.Value;
        }

        /// <summary>
        /// Removes the key and value from the HashMap for the key specified
        /// Replaces the HashMap entry with the available sentinel
        /// If the key is not found in the HashMap null is returned
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public V Remove(K key)
        {
            V value = default(V);

            int index = findMatchingBucket(key);

            if (index >= 0)
            {
                value = table[index].Value;
                table[index] = null;

                size--;
            }
            return value;
        }

        /// <summary>
        /// Collections of Keys
        /// </summary>
        /// <returns></returns>
        public ICollection<K> Keys()
        {
            List<K> list = new List<K>();

            foreach (Entry<K, V> entry in table)
            {
                list.Add(entry.Key);
            }

            return list;
        }

        /// <summary>
        /// Collection of Values
        /// </summary>
        /// <returns></returns>
        public ICollection<V> Values()
        {
            List<V> list = new List<V>();

            foreach (Entry<K, V> entry in table)
            {
                list.Add(entry.Value);
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int findBucket(K key)
        {
            if (Size() > Threshold)
            {
                throw new EntryPointNotFoundException();
            }

            return new StringKey(key.ToString()).GetHashCode() % table.Length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int findMatchingBucket(K key)
        {
            int currentIndex = findBucket(key);

            return table[currentIndex] == null ? -1 : currentIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        private void rehash()
        {
            size = 0;

            int newCapacitySize = resize();

            Entry<K, V>[] tableCopy = new Entry<K, V>[newCapacitySize];

            this.Threshold = (int)(LoadFactor * newCapacitySize);

            for (int i = 0; i < tableCopy.Length; i++)
            {
                Entry<K, V> entry = tableCopy[i];

                if (entry != null)
                {
                    Put(entry.Key, entry.Value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int resize()
        {
            Boolean isPrime = false;

            int currentPrime;
            int sqrtValue;

            currentPrime = (2 * table.Length) + 1;

            while (!isPrime)
            {
                isPrime = true;
                sqrtValue = (int)Math.Sqrt(currentPrime);

                for (int i = 3; i <= sqrtValue; i++)
                {
                    if (currentPrime % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (!isPrime)
                {
                    currentPrime += 2;
                }
            }
            return currentPrime;
        }
    }
}

/// <summary>
/// Inner Class
/// </summary>
/// <typeparam name="K"></typeparam>
/// <typeparam name="V"></typeparam>
public class Entry<K, V>
{
    /// <summary>
    /// Reference to Object
    /// </summary>
    public K Key { get; private set; }

    /// <summary>
    /// Reference to Value object
    /// </summary>
    public V Value { get; set; }

    public Entry(K key, V value)
    {
        this.Key = key;
        this.Value = value;
    }

    public override String ToString()
    {
        return string.Format("{0}:{1}", this.Key, this.Value);
    }
}

