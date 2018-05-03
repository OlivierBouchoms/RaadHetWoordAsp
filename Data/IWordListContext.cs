using System.Collections.Generic;

namespace Data
{
    public interface IWordListContext
    {
        List<string> GetWordlists();
        List<string> GetAllWords();
        List<string> GetWordsFromWordlist(string title);
    }
}
