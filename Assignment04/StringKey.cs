using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment04
{
    /// <summary>
    /// Contains the key information that will be used on our HashTable later on. 
    /// </summary>
    public class StringKey : IComparable<StringKey>
    {
        private String keyName { get; set; }
        private const double COEFFICIENT = 31;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="keyName"></param>
        public StringKey(string keyName)
        {
            this.keyName = keyName;
        }

        /// <summary>
        /// Returns the hash code for this string.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            int hashCode = 0;

            for (int i = 0; i < keyName.Length; i++)
            {
                hashCode += (int)(keyName[i] * Math.Pow(COEFFICIENT, i));
            }

            return Math.Abs(hashCode);
        }

        /// <summary>
        /// Determines whether this instance and a specified object, 
        /// which must also be a String object, have the same value.
        /// </summary>
        /// <param name="o">The string to compare to this instance.</param>
        /// <returns>true if obj is a String and its value is the same as this instance; otherwise, false. 
        /// If obj is null, the method returns false.</returns>
        public override bool Equals(object o)
        {
            if (this == o)
                return true;
            if (o == null || GetType() != o.GetType())
                return false;

            StringKey stringKey = (StringKey)o;
            return keyName == stringKey.GetKeyName();
        }

        /// <summary>
        /// Return the key name as string
        /// </summary>
        /// <returns></returns>
        public string GetKeyName() { return keyName.ToString(); }

        /// <summary>
        /// Returns this instance of String; no actual conversion is performed.
        /// </summary>
        /// <returns>The current string.</returns>
        public override string ToString() { return String.Format("{0} : {1}", this.keyName.ToString(), this.GetHashCode()); }

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
        public int CompareTo(StringKey keyName) => this.keyName.CompareTo(keyName.ToString());
    }
}
