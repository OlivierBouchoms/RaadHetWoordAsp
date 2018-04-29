using System.Collections.Generic;

namespace Data
{
    public class WordListRepository
    {
        private readonly IWordListContext _context;

        public WordListRepository(IWordListContext context)
        {
            _context = context;
        }

        public List<string> GetAllWords()
        {
            return _context.GetAllWords();
        }

        public List<string> GetWordlists()
        {
            return _context.GetWordlists();
        }

        public List<string> GetWordsFromWordlist(string title)
        {
            return _context.GetWordsFromWordlist(title);
        }
    }
}