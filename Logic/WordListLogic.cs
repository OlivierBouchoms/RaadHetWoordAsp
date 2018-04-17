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

        public List<string> GetWords()
        {
            return ShuffleWords(repo.GetWords());
        }

        public List<string> ShuffleWords(List<string> words)
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
    }
}
