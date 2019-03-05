using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleAnalyzer
{
    public class Word: IComparable
    {
        public Word(string name, int count)
        {
            Name = name;
            Count = count;
        }

        public string Name { get; set; }
        public int Count { get; set; }

        public int CompareTo(object obj)
        {
            Word word = obj as Word;

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
