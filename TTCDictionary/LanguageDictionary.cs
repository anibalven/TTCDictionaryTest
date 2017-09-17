using System.Collections.Generic;
using System.Linq;

namespace TTCDictionary
{
    public class LanguageDictionary : ILanguageDictionary
    {
        #region "local variables"
        //Changed from Dictionary to allow language repetition.
        //Can use Dictionary<string, TTCDictionaryEntry> for faster searches (or a Lookup)
        private List<TTCDictionaryEntry> list; 
        private List<string> languages = new List<string>(); //for faster search of languages
        #endregion "local variables"

        #region "Constructors"
        public LanguageDictionary(List<TTCDictionaryEntry> list)
        {
            this.list = list;
            languages = list.Select(l => l.Language).Distinct().ToList();
        }
        #endregion "Constructors"

        #region "ILanguageDictionary Implementation"
        public bool Check(string language, string word)
        {
            try
            {
                ValidateNullOrEmpty(word, language, Methods.Checking);
                ValidateLanguage(language, Methods.Checking);
            
                //search the word in the dictionary
                return WordExists(word, language);
            }
            catch
            {
                throw;
            }
        }

        public bool Add(string language, string word)
        {
            bool added = false;
            try
            {
                ValidateNullOrEmpty(word, language, Methods.Checking);

                if (!languages.Any(l => l == language))
                {
                    languages.Add(language);  //new language added
                }

                if (!WordExists(word, language))  //new word added
                {
                    list.Add(new TTCDictionaryEntry { Language = language, Word = word });
                    added = true;
                }

                return added;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<string> Search(string word)
        {
            try
            {
                ValidateNullOrEmpty(word, Methods.Searching);

                return list.Where(we => we.Word.StartsWith(word)).Select(we => we.Word);
            }
            catch
            {
                throw;
            }
        }

        #endregion "ILanguageDictionary Implementation"

        #region "private methods"
        private void ValidateNullOrEmpty(string word, string language, Methods method)
        {
            string wrongElement = string.IsNullOrEmpty(word)? "Word": "";
            wrongElement += string.IsNullOrEmpty(language)? (string.IsNullOrEmpty(wrongElement) ? " and Language":
                "Language"):"";

            if (!string.IsNullOrEmpty(wrongElement))
            {
                throw new TTCDictionaryException(string.Format("Missing {0} parameter", wrongElement), 
                    Methods.Checking, language, word);
            }
        }

        private void ValidateNullOrEmpty(string word, Methods method)
        {
            if (string.IsNullOrEmpty(word))
            {
                throw new TTCDictionaryException(string.Format("Missing word parameter"),
                    Methods.Searching, null, null);
            }
        }

        private void ValidateLanguage(string language, Methods method)
        {
            if (!languages.Contains(language))
            {
                throw new TTCDictionaryException("Language doesn't exists", Methods.Checking, language);
            }           
        }

        private bool WordExists(string word, string language)
        {
            return list.Any(we => we.Language == language && we.Word == word);
        }

        #endregion "private methods"
    }
}
