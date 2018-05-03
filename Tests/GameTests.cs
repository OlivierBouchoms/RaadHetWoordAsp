using System;
using System.Collections.Generic;
using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using RaadHetWoordGit.ViewModels;

namespace Tests
{
    [TestClass]
    public class GameTests
    {
        private ChecksLogic _checksLogic;
        private GameLogic _gameLogic;
        private TeamLogic _teamLogic;
        private TeamInGameLogic _teamInGameLogic;
        private WordListLogic _wordListLogic;

        private void Initialize()
        {
            _checksLogic = new ChecksLogic();
            _gameLogic = new GameLogic(new GameRepository(new GameMemoryContext()));
            _teamInGameLogic = new TeamInGameLogic(new TeamInGameRepository(new TeamInGameMemoryContext()));
            _teamLogic = new TeamLogic(new TeamRepository(new TeamMSSQLContext()));
            _wordListLogic = new WordListLogic(new WordListRepository(new WordListMSSQLContext()));
        }

        /// <summary>
        /// Testcase: TC01
        /// </summary>
        [TestMethod]
        public void TestAddTeams()
        {
            Initialize();
            Assert.IsTrue(_checksLogic.ValuesAreValid("teamOne", "teamTwo"));
        }

        /// <summary>
        /// Testcase: TC02
        /// </summary>
        [TestMethod]
        public void TestAddTeamsNull()
        {
            Initialize();
            Assert.IsFalse(_checksLogic.ValuesAreValid(null, "teamTwo"));

        }

        /// <summary>
        /// Testcase: TC03
        /// </summary>
        [TestMethod]
        public void TestAddTeamsDuplicate()
        {
            Initialize();
            Assert.IsFalse(_checksLogic.ValuesAreValid("hoi", "hoi"));
        }

        /// <summary>
        /// Testcase: TC04
        /// </summary>
        [TestMethod]
        public void TestMaxScoreValid()
        {
            Initialize();
            for (int i = 5; i < 25; i++)
            {
                Assert.IsTrue(i == _checksLogic.MaxScore(i));
            }
        }

        /// <summary>
        /// Testcase: TC05
        /// </summary>
        [TestMethod]
        public void TestMaxScoreInvalid()
        {
            Initialize();
            for (int i = 1; i > -2; i--)
            {
                Assert.IsTrue(_checksLogic.MaxScore(i) == 2);
            }
        }

        /// <summary>
        /// Testcase: TC08
        /// </summary>
        [TestMethod]
        public void TestStartGameValid()
        {
            Initialize();
            
            Assert.IsTrue(_checksLogic.ValuesAreValid("teamOne", "teamTwo") && _checksLogic.MaxScore(4) == 4);
        }

        /// <summary>
        /// Testcase: TC09
        /// </summary>
        [TestMethod]
        public void TestStartGameInvalidTeams()
        {
            Initialize();

            Assert.IsTrue(!_checksLogic.ValuesAreValid("teamOne", null) && _checksLogic.MaxScore(4) == 4);
        }

        /// <summary>
        /// Testcase: TC10
        /// </summary>
        [TestMethod]
        public void TestStartGameInvalidScore()
        {
            Initialize();

            Assert.IsTrue(_checksLogic.ValuesAreValid("teamOne", "teamTwo") && _checksLogic.MaxScore(-1) == 2);
        }

        /// <summary>
        /// Testcase: TC11
        /// </summary>
        [TestMethod]
        public void TestIncreaseScore()
        {
            Initialize();
            var before = PlayGame(Index(CreateViewModel(), true), true);

            var scoreBefore = before.Game.CurrentRound.Team.Score;

            var scoreAfter = ChangeScore(true, before).Game.CurrentRound.Team.Score;

            Assert.IsTrue(scoreBefore < scoreAfter);
        }

        /// <summary>
        /// Testcase: TC11
        /// </summary>
        [TestMethod]
        public void TestDecreaseScore()
        {
            Initialize();
            var before = PlayGame(Index(CreateViewModel(), true), true);
            before = ChangeScore(true, before);

            var scoreBefore = before.Game.CurrentRound.Team.Score;

            var scoreAfter = ChangeScore(false, before).Game.CurrentRound.Team.Score;

            Assert.IsTrue(scoreBefore > scoreAfter);
        }

        /// <summary>
        /// Testcase: TC13
        /// </summary>
        [TestMethod]
        public void TestNextRoundOver()
        {
            Initialize();

            var viewModel = PlayGame(Index(CreateViewModel(), true), true);
            viewModel.Game.TeamList[0].Score = viewModel.Game.Maxscore;
            Assert.IsTrue(PlayGame(viewModel, true).Winner == "Gewonnen");
        }

        /// <summary>
        /// Testcase: TC14
        /// </summary>
        [TestMethod]
        public void TestNextRound()
        {
            Initialize();

            var viewModel = PlayGame(Index(CreateViewModel(), true), true);
            viewModel.Game.TeamList[0].Score = viewModel.Game.Maxscore - 1;
            Assert.IsFalse(PlayGame(viewModel, true).Winner == "Gewonnen");
        }

        private GameViewModel CreateViewModel()
        {
            var viewModel = new GameViewModel
            {
                TeamOne = "teamOne",
                TeamTwo = "teamTwo",
                MaxScore = 5,
                Wordlist = null
            };

            return viewModel;
        }

        /// <summary>
        /// Testmethod for ChangeScore, replace session with parameters and return values
        /// </summary>
        public GameViewModel ChangeScore(bool increase, GameViewModel viewModel)
        {
            if (increase)
            {
                _teamInGameLogic.IncreaseScore(viewModel.Game.CurrentRound.Team);
                _teamLogic.IncreaseScore(viewModel.Game.CurrentRound.Team);

                for (int i = 0; i < 2; i++)
                {
                    if (viewModel.Game.CurrentRound.Team.Name == viewModel.Game.TeamList[i].Name)
                    {
                        viewModel.Game.TeamList[i] = viewModel.Game.CurrentRound.Team;
                    }
                }

                return viewModel;
            }
            _teamInGameLogic.DecreaseScore(viewModel.Game.CurrentRound.Team);
            _teamLogic.DecreaseScore(viewModel.Game.CurrentRound.Team);

            for (int i = 0; i < 2; i++)
            {
                if (viewModel.Game.CurrentRound.Team.Name == viewModel.Game.TeamList[i].Name)
                {
                    viewModel.Game.TeamList[i] = viewModel.Game.CurrentRound.Team;
                }
            }

            return viewModel;
        }

        /// <summary>
        /// Testmethod for index, replaces session with return value
        /// </summary>
        public GameViewModel Index(GameViewModel viewModel, bool test)
        {
            if (!_checksLogic.ValuesAreValid(viewModel.TeamOne, viewModel.TeamTwo))
            {
                viewModel.WarningClass = "visible";
                viewModel.Wordlists = _wordListLogic.GetWordlists();
                return new GameViewModel();
            }

            var teams = new List<Team>
            {
                new Team(viewModel.TeamOne),
                new Team(viewModel.TeamTwo)
            };

            viewModel.Game = new Game(_checksLogic.MaxScore(viewModel.MaxScore), teams);
            viewModel.Game = _gameLogic.AddTeams(teams, viewModel.Game);
            viewModel.Game = _gameLogic.AddWordlist(viewModel.Game, new Wordlist(_wordListLogic.GetWords(viewModel.Wordlist)));

            _teamLogic.AddTeam(teams[0]);
            _teamLogic.AddTeam(teams[1]);

            return viewModel;
        }

        /// <summary>
        /// Testmethod for PlayGame, replace session with parameters and return values
        /// </summary>
        public GameViewModel PlayGame(GameViewModel viewModel, bool test)
        {
            Initialize();

            if (_gameLogic.GameIsOver(viewModel.Game))
            {
                var winner = _gameLogic.GetWinner(viewModel.Game);
                viewModel.Winner = winner.Name;
                _teamLogic.IncreaseWins(_gameLogic.GetWinner(viewModel.Game));
                _teamLogic.IncreaseLosses(_gameLogic.GetLoser(viewModel.Game));
                return new GameViewModel { Winner = "Gewonnen" };
            }

            viewModel.Game.CurrentRound = new Round(viewModel.Game);

            try
            {
                viewModel.Game.TeamList[Round.Playerindex - 1] = _teamInGameLogic.IncreaseTurns(viewModel.Game.TeamList[Round.Playerindex - 1]);
                _teamLogic.IncreaseTurns(viewModel.Game.TeamList[Round.Playerindex - 1]);
            }
            catch (Exception e)
            {
                new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionSqLiteContext())).LogException(e);
                viewModel.Game.TeamList[Round.Playerindex] = _teamInGameLogic.IncreaseTurns(viewModel.Game.TeamList[Round.Playerindex]);
                _teamLogic.IncreaseTurns(viewModel.Game.TeamList[Round.Playerindex]);
            }

            if (viewModel.Game.Wordlist.Words.Count < 10)
            {
                viewModel.Game = _gameLogic.AddWordlist(viewModel.Game, new Wordlist(_wordListLogic.GetWords(viewModel.Wordlist)));
                viewModel.WordlistClass = "visible";
            }

            _wordListLogic.RemoveWords(viewModel.Game.Wordlist.Words);

            return viewModel;
        }
    }
}
