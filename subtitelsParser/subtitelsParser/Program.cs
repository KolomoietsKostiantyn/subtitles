
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            using (StreamReader reader = new StreamReader("shrek-yify-english.srt")) //shrek-yify-english.srt // abyss.txt // Alexander.srt
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
                newString = newString.Replace("--", string.Empty);
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
            Clear("new2.txt", wordContainer);
            Clear1("1000.txt", wordContainer);
            List<string> existingWords = GetExistingWorlds();
            List<Word1> words = wordContainer.Returnsorted();

            List<Word1> selectedWords = ValidateCorectWords(words, existingWords);
            selectedWords.Sort();
            int j = 0;
            for (int i = 0; i < selectedWords.Count; i++)
            {
                if (selectedWords[i].Count > 2)
                {
                    Console.WriteLine(selectedWords[i].Name + "   " + selectedWords[i].Count + "  " + j);
                    j++;
                }

            }

            Console.ReadKey();
        }


        public static List<string> GetExistingWorlds()
        {
            List<string> result = new List<string>();
            //word.mysql

            string dash = "[0-9]+";
            Regex dashRegex = new Regex(dash);
            string tab = @"\t";
            Regex tabRegex = new Regex(tab);

            using (StreamReader reader = new StreamReader("word.mysql")) 
            {
                string res = reader.ReadLine();
                while (res != null)
                {
                    res = dashRegex.Replace(res, string.Empty);
                    res = tabRegex.Replace(res, string.Empty);
                    result.Add(res);
                    res = reader.ReadLine();
                } 
            }
            return result;
        }



        public static List<Word1> ValidateCorectWords(List<Word1> word1s, List<string> existings)
        {
            List<Word1> result = new List<Word1>();
            foreach (Word1 item in word1s)
            {
                bool flag = false;
                foreach (var exist in existings)
                {
                    if (exist.ToLower() == item.Name)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    result.Add(new Word1() { Count = item.Count, Name = item.Name });
                }
            }

            return result;
        }








        public static void Clear1(string path, WordContainer wordContainer)
        {
            List<string> thousand = new List<string>();
            using (StreamReader reader = new StreamReader(path)) // new1.txt
            {
                string nStr = reader.ReadLine();
                while (!string.IsNullOrEmpty(nStr))
                {
                    thousand.Add(nStr);

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
