using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment04
{
    class Program
    {
        static void Main(string[] args)
        {
            HashMap<StringKey, Item> hashMap = new HashMap<StringKey, Item>(5);

            var itemLines = File.ReadAllLines("ItemData.txt");

            foreach (var line in itemLines)
            {
                string name = line.Split(',')[0];
                int gold = Int32.Parse(line.Split(',')[1]);
                double weight = Double.Parse(line.Split(',')[2]);

                StringKey keyName = new StringKey(name);
                Item item = new Item(name, gold, weight);
                hashMap.Put(keyName, item);
            }

            foreach (var item in hashMap.table)
            {
                if (item != null)
                {
                    if (item.Value.GoldPieces == 0)
                    {
                        hashMap.Remove(item.Key);
                    }
                }
            }
        }
    }
}
