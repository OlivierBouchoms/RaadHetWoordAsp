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
            return repo.GetWords();
        }
    }
}
