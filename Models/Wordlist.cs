﻿using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class Wordlist
    {
        public readonly string Title;
        public readonly List<string> Words;

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