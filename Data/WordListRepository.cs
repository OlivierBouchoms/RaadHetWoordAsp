using System.Collections.Generic;

namespace Data
{
    public class WordListRepository
    {
        private IWordListContext context;

        public WordListRepository(IWordListContext context)
        {
            this.context = context;
        }

        public List<string> GetAllWords()
        {
            return context.GetAllWords();
        }

        public List<string> GetWordlists()
        {
            return context.GetWordlists();
        }

        public List<string> GetWordsFromWordlist(string title)
        {
            return context.GetWordsFromWordlist(title);
        }
    }
}