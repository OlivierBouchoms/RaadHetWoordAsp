using System;
using System.Collections.Generic;
using Data;

namespace Logic
{
    public class WordListLogic
    {
        private readonly WordListRepository repo;

        public WordListLogic(WordListRepository wordListRepository)
        {
            repo = wordListRepository;
        }

        /// <summary>
        /// Get all words from the database
        /// </summary>
        /// <returns></returns>
        public List<string> GetWords()
        {
            return ShuffleWords(repo.GetWords());
        }

        /// <summary>
        /// Shuffle the words
        /// </summary>
        /// <param name="words">Input list with words</param>
        public List<string> ShuffleWords(List<string> words)
        {
            var random = new Random();
            var shuffledWords = new List<string>();

            //Algoritme
            while (words.Count != 0)
            {
                var index = random.Next(words.Count);
                shuffledWords.Add(words[index]);
                words.RemoveAt(index);
            }

            return shuffledWords;
        }

        /// <summary>
        /// Remove first five words from database
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public List<string> RemoveWords(List<string> words)
        {
            words.RemoveRange(0, 5);
            return words;
        }
    }
}
