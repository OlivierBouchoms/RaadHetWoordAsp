using System.Collections.Generic;
using Models;

namespace Data
{
    public interface IWordListContext
    {
        List<string> GetWordsFromWordlist();
    }
}
