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
            List<Item> backpack = new List<Item>();


            double totalWeight = 0;
            double totalSold = 0;
            const double MAX_WEIGHT = 30;

            var itemLines = File.ReadAllLines("ItemData.txt");
            var lootItems = File.ReadAllLines("adventureLoot.txt");

            foreach (var line in itemLines)
            {
                string name = line.Split(',')[0];
                int gold = Int32.Parse(line.Split(',')[1]);
                double weight = Double.Parse(line.Split(',')[2].Trim().Replace(".", ","));

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

            foreach (var item in lootItems)
            {
                StringKey keyName = new StringKey(item);

                if (hashMap.Get(keyName) != null)
                {
                    Item itemToAdd = new Item(hashMap.Get(keyName).Name, hashMap.Get(keyName).GoldPieces, hashMap.Get(keyName).Weight);

                    if ((totalWeight + itemToAdd.Weight) <= MAX_WEIGHT)
                    {
                        totalWeight += itemToAdd.Weight;
                        backpack.Add(itemToAdd);
                        Console.WriteLine(String.Format("You have picked up a {0}", itemToAdd.Name));
                    }
                    else
                    {
                        Console.WriteLine(String.Format("You cannot pick up the {0}, you are already carrying {1}KG and it weights {2}KG.", itemToAdd.Name, totalWeight, itemToAdd.Weight));
                    }
                }
                else
                {
                    Console.WriteLine(String.Format("You find an unknown item that is not in your loot table, you leave it alone. - {0}", keyName));
                }
            }

            foreach (var item in lootItems)
            {
                if (backpack.Exists(x => x.Name.Equals(item)))
                {
                    var itemAdded = backpack.Where(bk => bk.Name == item).First();

                    if (itemAdded != null)
                    {
                        var totalItems = backpack.Where(bk => bk.Name == item).Count();

                        Console.WriteLine(String.Format("{0}, {1}GP {2}KG - Quantity: {3} - Subtotal: {4}GP", itemAdded.Name, itemAdded.GoldPieces, itemAdded.Weight, totalItems, (totalItems * itemAdded.GoldPieces)));
                        totalSold += totalItems * itemAdded.GoldPieces;
                        backpack.RemoveAll(bk => bk.Name.Equals(itemAdded.Name));
                    }
                }
            }

            Console.WriteLine(String.Format("Total value of sold loot items: {0}GP", totalSold));

            Console.ReadLine();
        }
    }
}
