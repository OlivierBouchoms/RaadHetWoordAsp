﻿using System;
using System.Collections.Generic;
using Data;

namespace Logic
{
    public class WordListLogic
    {
        private readonly WordListRepository _repo;

        public WordListLogic(WordListRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get all words from the database
        /// </summary>
        public List<string> GetWords(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return ShuffleWords(_repo.GetAllWords());
            }

            return GetWordsFromWordlist(title);
        }

        /// <summary>
        /// Get words from a specific wordlist
        /// </summary>
        private List<string> GetWordsFromWordlist(string title)
        {
            try
            {
                return ShuffleWords(_repo.GetWordsFromWordlist(title));
            }
            catch (Exception e)
            {
                new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionSqLiteContext())).LogException(e);
            }

            return new List<string>();
        }

        /// <summary>
        /// Shuffle the words
        /// </summary>
        /// <param name="words">Input list with words</param>
        private List<string> ShuffleWords(List<string> words)
        {
            var random = new Random();
            var shuffledWords = new List<string>();

            while (words.Count != 0)
            {
                var index = random.Next(words.Count);
                shuffledWords.Add(words[index]);
                words.RemoveAt(index);
            }

            return shuffledWords;
        }

        /// <summary>
        /// Remove first five words from list
        /// </summary>
        public List<string> RemoveWords(List<string> words)
        {
            words.RemoveRange(0, 5);
            return words;
        }

        /// <summary>
        /// Get all wordlists
        /// </summary>
        /// <returns></returns>
        public List<string> GetWordlists()
        {
            return _repo.GetWordlists();
        }
    }
}
