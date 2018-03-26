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

        public List<string> GetWords()
        {
            return context.GetWordsFromWordlist();
        }
    }
}