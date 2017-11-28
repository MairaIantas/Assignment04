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
                double w = Double.Parse(line.Split(',')[2]);

                //Console.WriteLine(String.Format("{0} {1} {2}", name, gold, w));

                StringKey keyName = new StringKey(name);
                Item item = new Item(name, gold, w);
                hashMap.Put(keyName, item);

                Console.WriteLine(hashMap.Get(keyName));
            }

            //foreach (var item in hashMap.Keys())
            //{
            //    if (hashMap.Get(item).GoldPieces == 0)
            //    {
            //        hashMap.Remove(item);
            //    }
            //}


            //var iterator = hashMap.Keys();

            //while (iterator.hasNext())
            //{
            //    System.out.println(iterator.next());
            //}

            while (hashMap.Keys().MoveNext())
            {
                if (hashMap.Keys().Current != null)
                {

                }
            }

            do
            {
                if (hashMap.Keys().Current != null)
                {

                }

            } while (hashMap.Keys().MoveNext());
        }
    }
}
