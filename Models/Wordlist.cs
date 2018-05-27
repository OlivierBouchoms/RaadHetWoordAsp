using System.Collections.Generic;

namespace Models
{
    public class Wordlist
    {
        public readonly string Title;
        public List<string> Words { get; set; }

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