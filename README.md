# C# Interview Pairing Exercise: Spell Checker/Dictionary

## Instructions to explain to the candidate:

Build a dictionary service using C#.

There are three methods to be implemented in the `LanguageDictionary` class:
  - ##### `public bool Check(string language, string word)`
    Determines if `word` is in the dictionary for `language`. If it is, it returns `true`, if it's not, it will return `false`.

  - ##### `public boolean Add(string language, string word)`
    Adds `word` to the dictionary for `language`. It returns `true` if it was successfully added. If it was not, it returns `false`.
  
  - ##### `public IEnumerable<string> Search(string word)`
    Finds words in all languages that start with the `word`.

### Clarifications:
  - The time limit is 1 hour. The first 10 minutes will be spent by the candidate alone, and then the interviewers will join in order to have a pair-programming session.
  - The goal is not to complete the three methods, but to complete one or two (if possible), covering all the scenarios, and demonstrating rock-solid coding skills. They should start with the `add` and `check` methods.
  - TDD is expected and required.
  - Everything should be kept simple and in memory. There's no need to store information in external files or a DB.
  - New classes can be added to the project, but remember the goal is to keep everything simple.
  - Internet can be used to search for information.
  - Any questions can be asked to the interviewers.