using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCDictionary
{
    /// <summary>
    /// Class to throw unnexpected errors
    /// </summary>
    [Serializable]
    public class TTCDictionaryException:Exception
    {
        public TTCDictionaryException(string failReason, Methods method, string language="", string word="")
        {
            Language = language;
            Word = word;
            FailReason = failReason;

        }

        public string FailReason { get; }
        public Methods Method { get; }
        public string Language { get; }
        public string Word { get; }
        
        public override string ToString()
        {
            return string.Format("Error {0} {1}: Language: {2}, Word:{3} ", FailReason, Method, Language, Word);
        }
    }
}
