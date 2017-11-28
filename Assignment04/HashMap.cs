﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment04
{
    public class HashMap<K, V> : IMap<K, V>
    {
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
        public Entry<K, V>[] table = new Entry<K, V>[DEFAULT_CAPACITY];

        /// <summary>
        /// Constructor
        /// </summary>
        public HashMap()
        {
            this.LoadFactor = DEFAULT_LOADFACTOR;
            this.Threshold = Int32.Parse(Math.Round(DEFAULT_CAPACITY * DEFAULT_LOADFACTOR).ToString());
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public HashMap(int initialCapacity)
        {
            this.LoadFactor = DEFAULT_LOADFACTOR;
            this.Threshold = Int32.Parse(Math.Round(initialCapacity * DEFAULT_LOADFACTOR).ToString());
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
            this.Threshold = Int32.Parse(Math.Round(initialCapacity * this.LoadFactor).ToString());
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
            return Size() == 0;
        }

        /// <summary>
        /// Sets all entries in the HashMap to null
        /// </summary>
        public void Clear()
        {
            this.table = null;
            this.size = 0;
        }

        /// <summary>
        /// Gets the value associated with the passed key
        /// </summary>
        /// <param name="key"></param>
        /// <returns> Returns null if the passed key not in the HashMap</returns>
        public V Get(K key)
        {
            return findMatchingBucket(key) == -1 ? default(V) : table[findMatchingBucket(key)].Value;
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
                throw new ArgumentNullException();
            }

            if (size >= Threshold)
            {
                rehash();
            }

            V removed = Remove(key);

            table[findBucket(key)] = new Entry<K, V>(key, value);
            size++;

            return removed;
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
            V returnValue = default(V);

            int index = findMatchingBucket(key);

            if (index != -1)
            {
                returnValue = table[index].Value;
                table[index] = null;

                size--;
            }
            return returnValue;
        }

        /// <summary>
        /// Collections of Keys
        /// </summary>
        /// <returns></returns>
        public ICollection<K> Keys()
        {
            List<K> list = new List<K>();

            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    K key = table[i].Key;
                    list.Add(key);
                }
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

            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    V values = table[i].Value;
                    list.Add(values);
                }
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

            return key.GetHashCode() % table.Length;
        }

        private int findMatchingBucket(K key)
        {
            int currentIndex = findBucket(key);

            return table[currentIndex] == null ? -1 : currentIndex;
        }

        private void rehash()
        {
            size = 0;

            int newCapacitySize = resize();

            Entry<K, V>[] tableCopy = new Entry<K, V>[newCapacitySize];

            this.Threshold = (int)(LoadFactor * newCapacitySize);

            for (int i = 0; i < tableCopy.Length; i++)
            {
                Entry<K, V>[] entry = tableCopy;

                if (entry != null)
                {
                    //Put(entry.Key, entry.GetValue);
                }
            }
        }
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

