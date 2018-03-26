using System;
using Models;

namespace RaadHetWoordGit.ViewModels
{
    public class GameViewModel
    {
        //Naar UI
        public Game Game { get; set; }

        public bool TeamOneSuccess { get; set; }
        public bool TeamTwoSuccess { get; set; }

        //Naar Controller
        public string TeamOne { get; set; }
        public string TeamTwo { get; set; }

        public int MaxScore { get; set; }

        //Beide kanten
        public string TeamColumnClass { get; set; }
        public string TeamFormClass { get; set; }

    }
}
