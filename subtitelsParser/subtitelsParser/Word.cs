using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subtitelsParser
{
    class Word1:IComparable
    {
        public string Name { get; set; }
        public int Count { get; set; }

        public int CompareTo(object obj)
        {
            Word1 word = obj as Word1;

            if (word.Count > Count)
            {
                return 1;
            }
            if (word.Count < Count)
            {
                return -1;
            }

            return 0;
        }
    }
}
