using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCDictionary
{
    public interface ILanguageDictionary
    {
        bool Check(string language, string word);

        bool Add(string language, string word);

        IEnumerable<string> Search(string word);
    }
}
