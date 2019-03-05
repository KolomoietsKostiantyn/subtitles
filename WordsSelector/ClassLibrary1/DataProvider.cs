using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleAnalyzer
{
    public interface DataProvider: IDisposable
    {
        List<string> GetAllSimpleWords();
        bool AreExist(string word);
        List<string> GetUserDictionary(int userId);
    }
}
