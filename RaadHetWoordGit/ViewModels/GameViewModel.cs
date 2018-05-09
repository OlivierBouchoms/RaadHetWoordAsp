using System.Collections.Generic;
using Models;

namespace RaadHetWoordGit.ViewModels
{
    public class GameViewModel
    {
        //Naar UI
        public Game Game { get; set; }

        public string WordlistClass { get; set; }
        public string WarningClass { get; set; }
        public string Winner { get; set; }

        public List<string> Wordlists { get; set; }

        //Naar Controller
        public string TeamOne { get; set; }
        public string TeamTwo { get; set; }
        public string Wordlist { get; set; }

        public bool Increase { get; set; }

        public int MaxScore { get; set; }

        public int ScoreChange { get; set; }
    }
}
