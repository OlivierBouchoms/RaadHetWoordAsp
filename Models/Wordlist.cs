using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class Wordlist
    {
        internal string Title { get; private set; }
        internal string Description { get; private set; }
        public List<string> Words { get; }

        public Wordlist()
        {
            //Empty
        }

        public Wordlist(List<string> words)
        {
            Words = words;
        }
    }
}