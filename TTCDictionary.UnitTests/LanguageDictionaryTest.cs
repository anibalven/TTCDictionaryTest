using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TTCDictionary.UnitTests
{
    [TestFixture]
    public class LanguageDictionaryTest
    {
        private LanguageDictionary SUT;

        private List<TTCDictionaryEntry> list;

        const string nullLanguage = null;
        const string emptyLanguage = "";
        const string nullWord = null;
        const string emptyWord = null;

        [SetUp]
        public void Setup()
        {
            list = new List<TTCDictionaryEntry>();
            SUT = new LanguageDictionary(list);
        }

        [Test]
        [Category("Adding")]
        public void When_adding_a_null_or_empty_word_should_return_an_exception()
        {
            // Arrange.
            var language = "English";

            // Act. and Assert.
            Assert.Throws<TTCDictionaryException>(() => this.SUT.Add(language, nullWord), "Null word not validated");
            Assert.Throws<TTCDictionaryException>(() => this.SUT.Add(language, emptyWord), "Empty word not validated");
        }

        [Test]
        [Category("Adding")]
        public void When_adding_a_null_or_empty_language_should_return_an_exception()
        {
            // Arrange.
            var word = "test";

            // Act. and Assert.
            Assert.Throws<TTCDictionaryException>(() => this.SUT.Add(nullLanguage, word), "Null language not validated");
            Assert.Throws<TTCDictionaryException>(() => this.SUT.Add(emptyLanguage, word), "Empty language not validated");
        }

        [Test]
        [Category("Adding")]
        public void When_adding_a_word_which_does_not_exist_should_return_true()
        {
            // Arrange.
            var word = "test";
            var lang = "English";

            // Act.
            var result = this.SUT.Add(lang, word);

            // Assert.
            Assert.IsTrue(result);

            var listCheck = this.list.FirstOrDefault(i => i.Language  == lang && i.Word == word);

            Assert.IsNotNull(listCheck, "Word not found");
            Assert.IsTrue(listCheck.Language == lang, "Incorrect language");
            Assert.IsTrue(listCheck.Word == word, "Incorrect word");
        }

        [Test]
        [Category("Adding")]
        public void When_adding_a_word_which_does_exist_should_return_false()
        {
            // Arrange.
            var word = "test";
            this.SUT.Add("English", word);

            // Act.
            var result = this.SUT.Add("English", word);

            // Assert.
            Assert.IsFalse(result);
        }

        [Test]
        [Category("Adding")]
        public void When_adding_a_word_which_does_exist_but_in_a_different_language_should_return_true()
        {
            // Arrange.
            var word = "test";
            this.SUT.Add("English", word);

            // Act.
            var result = this.SUT.Add("German", word);

            // Assert.
            Assert.IsTrue(result);
        }

        [Test]
        [Category("Checking")]
        public void When_checking_an_empty_or_null_language_should_return_an_exception()
        {
            // Arrange.
            var word = "test";

            // Act. and Assert.
            Assert.Throws<TTCDictionaryException>(() => this.SUT.Check(nullLanguage, word), "Null language not validated");
            Assert.Throws<TTCDictionaryException>(() => this.SUT.Check(emptyLanguage, word), "Empty language not validated");
        }

        [Test]
        [Category("Checking")]
        public void When_checking_an_empty_or_null_word_should_return_an_exception()
        {
            // Arrange.
            string nullWord = null;
            string emptyWord = string.Empty;

            // Act. and Assert.
            Assert.Throws<TTCDictionaryException>(() => this.SUT.Check("English", nullWord), "Null word not validated");
            Assert.Throws<TTCDictionaryException>(() => this.SUT.Check("English", emptyWord), "Empty word not validated");
        }

        [Test]
        [Category("Checking")]
        public void When_checking_a_word_whith_unknown_language_should_return_an_exception()
        {
            // Arrange.
            var word = "test";
            this.SUT.Add("English", word);

            // Act. and Assert.
            Assert.Throws<TTCDictionaryException>(()=> this.SUT.Check("Polish", word),"Word found with an unknwon language");
        }

        [Test]
        [Category("Checking")]
        public void When_checking_a_word_which_does_exist_should_return_true()
        {
            // Arrange.
            var word = "test";
            this.SUT.Add("English", word);

            // Act.
            var result = this.SUT.Check("English", word);

            // Assert.
            Assert.IsTrue(result);
        }

        [Test]
        [Category("Checking")]
        public void When_checking_a_word_which_does_not_exist_should_return_false()
        {
            // Arrange.
            this.SUT.Add("English", "Dummy"); //In order to have english language
            var word = "test";

            // Act.
            var result = this.SUT.Check("English", word);

            // Assert.
            Assert.IsFalse(result);
        }

        [Test]
        [Category("Searching")]
        public void When_searching_an_empty_or_null_word_should_return_an_exception()
        {
            // Arrange. 
            var nullWord = "";
            var emptyWord = "";

            // Act. and Assert.
            Assert.Throws<TTCDictionaryException>(() => this.SUT.Search(nullWord), "Null word not validated");
            Assert.Throws<TTCDictionaryException>(() => this.SUT.Search(emptyWord), "Empty Word not validated");
        }

        [Test]
        [Category("Searching")]
        public void When_searching_an_non_existent_word_should_return_an_empty_list()
        {
            // Arrange.
            this.SUT.Add("English", "dummy"); 
            var word = "test";

            // Act.
            var result = this.SUT.Search(word);

            // Assert.
            CollectionAssert.IsEmpty(result, "List is not empty"); 
        }

        [Test]
        [Category("Searching")]
        public void When_searching_an_existent_word_should_return_a_list_from_all_languages()
        {
            // Arrange.
            this.SUT.Add("Italian", "prova");
            this.SUT.Add("Spanish", "prueba");
            this.SUT.Add("English", "test");
            var word = "pr";

            // Act.
            var result = this.SUT.Search(word);

            // Assert.
            Assert.That(result.Count() == 2, "Incorrect number of words");
        }

        [Test]
        [Category("Searching")]
        public void When_searching_with_an_empty_dictionary_should_return_an_empty_list()
        {
            // Arrange.
            var word = "test";

            // Act.
            var result = this.SUT.Search(word);

            // Assert.
            CollectionAssert.IsEmpty(result, "List is not empty");
        }
    }
}
