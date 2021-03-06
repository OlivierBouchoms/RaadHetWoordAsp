﻿using System.Collections.Generic;
using Models;

namespace RaadHetWoordGit.ViewModels
{
    public class GameViewModel
    {
        public GameViewModel()
        {
            WarningClass = "hidden";
        }
        //Naar UI
        public Game Game { get; set; }

        public string WarningClass { get; set; }
        public string Winner { get; set; }

        public List<string> Wordlists { get; set; }

        //Naar Controller
        public string TeamOne { get; set; }
        public string TeamTwo { get; set; }
        public string Wordlist { get; set; }

        public int MaxScore { get; set; }
        public int ScoreChange { get; set; }
    }
}
