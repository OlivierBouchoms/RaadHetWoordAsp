using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Newtonsoft.Json;
using RaadHetWoordGit.Controllers;
using RaadHetWoordGit.ViewModels;

namespace Tests
{
    [TestClass]
    public class JsonTests
    {
        private List<Tuple<string, string>> session;

        private void Initialize()
        {
            session = new List<Tuple<string, string>>();
        }

        [TestMethod]
        public void TestSerializing()
        {
            Initialize();
            var viewModel = new GameViewModel();
            viewModel.Game = new Game(10, new List<Team>{new Team("team1"), new Team("team2")});
            viewModel.Game.Maxscore = 7;
            viewModel.Game.Wordlist = new Wordlist(new List<string>{ "hoi", "doei", "bier"}, "wordlist");
            viewModel.Game.CurrentRound = new Round(viewModel.Game);

            PlaceViewModelInSession(true, viewModel);

            var vm = GetViewModelFromSession();

        }

        private void PlaceViewModelInSession(bool _round, GameViewModel viewModel)
        {
            var teamList = viewModel.Game.TeamList;
            var wordList = viewModel.Game.Wordlist.Words;

            var round = new Round();
            if (_round)
            {
                round = viewModel.Game.CurrentRound;
                session.Add(new Tuple<string, string>(nameof(Round), JsonConvert.SerializeObject(round)));
                viewModel.Game.CurrentRound = null;
            }
            session.Add(new Tuple<string, string>("teamlist", JsonConvert.SerializeObject(teamList)));
            session.Add(new Tuple<string, string>(nameof(Wordlist), JsonConvert.SerializeObject(wordList)));
            viewModel.Game.TeamList = null;
            viewModel.Game.Wordlist = null;

            session.Add(new Tuple<string, string>(nameof(GameViewModel), JsonConvert.SerializeObject(viewModel)));

            viewModel.Game.TeamList = teamList;
            viewModel.Game.Wordlist = new Wordlist(wordList);
            if (_round)
            {
                viewModel.Game.CurrentRound = round;
            }
        }

        private GameViewModel GetViewModelFromSession()
        {
            var teamList = JsonConvert.DeserializeObject<List<Team>>(session.FirstOrDefault(_session => _session.Item1 == "teamlist").Item2);
            var wordList = JsonConvert.DeserializeObject<List<string>>(session.FirstOrDefault(_session => _session.Item1 == nameof(Wordlist)).Item2);
            var viewModel = JsonConvert.DeserializeObject<GameViewModel>(session.FirstOrDefault(_session => _session.Item1 == nameof(GameViewModel)).Item2);

            viewModel.Game.TeamList = teamList;
            viewModel.Game.Wordlist = new Wordlist(wordList);

            var round = JsonConvert.DeserializeObject<Round>(session.FirstOrDefault(_session => _session.Item1 == nameof(Round)).Item2);
            viewModel.Game.CurrentRound = round;

            return viewModel;
        }
    }
}
