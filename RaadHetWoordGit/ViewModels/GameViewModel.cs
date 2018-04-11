using Models;

namespace RaadHetWoordGit.ViewModels
{
    public class GameViewModel
    {
        //Naar UI
        public Game Game { get; set; }

        public bool TeamOneSuccess { get; set; }
        public bool TeamTwoSuccess { get; set; }

        public string TeamColumnClass { get; set; }
        public string TeamFormClass { get; set; }
        public string WordlistClass { get; set; }

        //Naar Controller
        public string TeamOne { get; set; }
        public string TeamTwo { get; set; }

        public bool Increase { get; set; }

        public int MaxScore { get; set; }
    }
}
