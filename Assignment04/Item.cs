using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment04
{
    /// <summary>
    /// Contains the name, value, and weight of the found item (how much the item is worth in gold pieces). 
    /// </summary>
    public class Item : IComparable<Item>
    {
        public string Name { get; private set; }
        public int GoldPieces { get; private set; }
        public Double Weight { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="goldPieces"></param>
        /// <param name="weight"></param>
        public Item(string name, int goldPieces, double weight)
        {
            this.Name = name;
            this.GoldPieces = goldPieces;
            this.Weight = weight;
        }

        /// <summary>
        /// Returns this instance of String; no actual conversion is performed.
        /// </summary>
        /// <returns>The current string.</returns>
        public override string ToString() => String.Format("{0} {1} {2}", Name, GoldPieces, Weight);

        /// <summary>
        /// Compares this instance with a specified String object and indicates 
        /// whether this instance precedes, follows, or appears in the same position 
        /// in the sort order as the specified string.
        /// </summary>
        /// <param name="keyName">The string to compare with this instance.</param>
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, 
        /// follows, or appears in the same position in the sort order as the strB parameter.
        /// Value Condition Less than zero This instance precedes strB. 
        /// Zero This instance has the same position in the sort order as strB. 
        /// Greater than zero This instance follows strB.-or- strB is null.</returns>
        public int CompareTo(Item item) => this.Name.CompareTo(item.Name.ToString());
    }
}
