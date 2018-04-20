using System.Collections.Generic;

namespace Data
{
    public interface IWordListContext
    {
        List<string> GetWords();
        List<string> GetWordsFromWordlist(int id);
    }
}
