using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subtitelsParser
{
    class WordContainer
    {
        public List<Word1> words = new List<Word1>();

        public bool Contain(string wd)
        {
            foreach (Word1 item in words)
            {
                if (item.Name == wd)
                {
                    return true;
                }
            }

            return false;
        }


        public void DeleteEll(string vd)
        {
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i].Name == vd)
                {
                    words.RemoveAt(i);
                    return;
                }
            }
        }

        public void Add(string name, int count)
        {
            words.Add(new Word1() {Name = name, Count = count });
        }

        public List<Word1> Returnsorted()
        {
            words.Sort();
            return words;
        }

    }
}
