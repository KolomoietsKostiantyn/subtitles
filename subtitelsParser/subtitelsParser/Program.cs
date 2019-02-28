using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace subtitelsParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string allText;
            using (StreamReader reader = new StreamReader("shrek-yify-english.srt"))
            {
                allText = reader.ReadToEnd();
            }


            string[] splitSubtitle = allText.Split(new char[] {' ', '.',',',':','?','!','\n','\r'}, StringSplitOptions.RemoveEmptyEntries);

            string numbers = "[0-9]+";
            Regex numbersRegex = new Regex(numbers);

            string dash = "[0-9]+";
            Regex dashRegex = new Regex(dash);

            string tag = "<.*>";
            Regex tagRegex = new Regex(tag);

            string open = ">";
            Regex openRegex = new Regex(open);

            string closed = "<";
            Regex closedRegex = new Regex(closed);

            string specialSymbols = "\u0022";
            Regex specialSymbolsRegex = new Regex(specialSymbols);


            List<string> withoutSing = new List<string>();
            foreach (var item in splitSubtitle)
            {
                string newString;
                newString = numbersRegex.Replace(item, string.Empty);
                newString = dashRegex.Replace(newString, string.Empty);
                newString = tagRegex.Replace(newString, string.Empty);
                newString = openRegex.Replace(newString, string.Empty);
                newString = closedRegex.Replace(newString, string.Empty);
                newString = specialSymbolsRegex.Replace(newString, string.Empty);
                newString = specialSymbolsRegex.Replace(newString, string.Empty);
                newString = newString.Replace("[", string.Empty);
                newString = newString.Replace("]", string.Empty);
                newString = newString.Replace("-", string.Empty);
                if (!string.IsNullOrEmpty(newString) && newString.Length>2 && !newString.Contains("'"))
                {
                    
                    withoutSing.Add(newString.ToLower());
                }
            }

            WordContainer wordContainer = new WordContainer();
            foreach (string item in withoutSing)
            {
                if (!wordContainer.Contain(item))
                {
                    int totalNumber = 0;
                    foreach (string item1 in withoutSing)
                    {
                        if (item1 == item)
                        {
                            totalNumber++;
                        }
                    }
                    wordContainer.Add(item, totalNumber);
                }
            }


            Clear("new1.txt", wordContainer);
            //Clear("new2.txt", wordContainer);

            List<Word> words = wordContainer.Returnsorted();
            int j = 0;
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i].Count > 1)
                {
                    Console.WriteLine(words[i].Name + "   " + words[i].Count + "  " + j);
                    j++;
                }

            }

            Console.ReadKey();
        }


        public static void Clear(string path, WordContainer wordContainer)
        {
            List<string> thousand = new List<string>();
            using (StreamReader reader = new StreamReader(path)) // new1.txt
            {
                string nStr = reader.ReadLine();
                while (!string.IsNullOrEmpty(nStr))
                {
                    Match str = Regex.Match(nStr, @"[0-9]+\t+[a-z]+");
                    Match str1 = Regex.Match(str.Value, @"[a-z]+");
                    thousand.Add(str1.Value);

                    nStr = reader.ReadLine();
                }
            }

            foreach (var item in thousand)
            {
                wordContainer.DeleteEll(item);
                wordContainer.DeleteEll(item + "s");
                wordContainer.DeleteEll(item + "ed");
                wordContainer.DeleteEll(item + "ing");
            }
        }




    }












}
