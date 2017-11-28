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
                StringKey keyName = new StringKey(line.Split(',')[0]);
                Item item = new Item(line.Split(',')[0], int.Parse(line.Split(',')[1]), Double.Parse(line.Split(',')[2]));
                hashMap.Put(keyName, item);
            }

            foreach (var item in hashMap.Keys())
            {
                if (hashMap.Get(item).GoldPieces == 0)
                {
                    hashMap.Remove(item);
                }
            }
        }
    }
}
