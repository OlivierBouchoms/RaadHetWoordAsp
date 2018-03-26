using System;
using System.Collections.Generic;
using System.Text;
using Data;

namespace Logic
{
    public class WordListLogic
    {
        private WordListRepository repo;

        public WordListLogic(WordListRepository wordListRepository)
        {
            this.repo = wordListRepository;
        }

        public List<string> GetWords()
        {
            return repo.GetWords();
        }
    }
}
