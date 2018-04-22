using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class Wordlist
    {
        public string Title { get; }
        public string Description { get; private set; }
        public List<string> Words { get; }

        public Wordlist()
        {
            //Empty
        }

        public Wordlist(string title)
        {
            Title = title;
        }

        public Wordlist(List<string> words, string title)
        {
            Words = words;
            Title = title;
        }

        public Wordlist(List<string> words)
        {
            Words = words;
        }
    }
}