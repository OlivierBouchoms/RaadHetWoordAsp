using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace RaadHetWoordGit.ViewModels
{
    public class LeaderboardViewModel
    {
        public LeaderboardViewModel(List<Team> teams)
        {
            Teams = teams;
        }

        public List<Team> Teams { get; set; }
        public OrderBy Order { get; set; }
        public string OrderBy { get; set; }
    }
}
