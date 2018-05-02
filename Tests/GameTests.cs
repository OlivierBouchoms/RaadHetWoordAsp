using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaadHetWoordGit.Controllers;
using RaadHetWoordGit.ViewModels;

namespace Tests
{
    [TestClass]
    public class GameTests
    {
        private ChecksLogic _checksLogic;
        private GameApiController _gameApiController;
        private GameController _gameController;

        private void Initialize()
        {
            _checksLogic = new ChecksLogic();
            _gameApiController = new GameApiController();
            _gameController = new GameController();
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
            var before = _gameController.PlayGame(_gameController.Index(CreateViewModel(), true), true);

            var scoreBefore = before.Game.CurrentRound.Team.Score;

            var scoreAfter = _gameApiController.ChangeScore(true, before).Game.CurrentRound.Team.Score;

            Assert.IsTrue(scoreBefore < scoreAfter);
        }

        /// <summary>
        /// Testcase: TC11
        /// </summary>
        [TestMethod]
        public void TestDecreaseScore()
        {
            Initialize();
            var before = _gameController.PlayGame(_gameController.Index(CreateViewModel(), true), true);
            before = _gameApiController.ChangeScore(true, before);

            var scoreBefore = before.Game.CurrentRound.Team.Score;

            var scoreAfter = _gameApiController.ChangeScore(false, before).Game.CurrentRound.Team.Score;

            Assert.IsTrue(scoreBefore > scoreAfter);
        }

        /// <summary>
        /// Testcase: TC13
        /// </summary>
        [TestMethod]
        public void TestNextRoundOver()
        {
            Initialize();

            var viewModel = _gameController.PlayGame(_gameController.Index(CreateViewModel(), true), true);
            viewModel.Game.TeamList[0].Score = viewModel.Game.Maxscore;
            Assert.IsTrue(_gameController.PlayGame(viewModel, true).Winner == "Gewonnen");
        }

        /// <summary>
        /// Testcase: TC14
        /// </summary>
        [TestMethod]
        public void TestNextRound()
        {
            Initialize();

            var viewModel = _gameController.PlayGame(_gameController.Index(CreateViewModel(), true), true);
            viewModel.Game.TeamList[0].Score = viewModel.Game.Maxscore - 1;
            Assert.IsFalse(_gameController.PlayGame(viewModel, true).Winner == "Gewonnen");
        }

        private GameViewModel CreateViewModel()
        {
            var viewModel = new GameViewModel();
            viewModel.TeamOne = "teamOne";
            viewModel.TeamTwo = "teamTwo";
            viewModel.MaxScore = 5;
            viewModel.Wordlist = null;

            return viewModel;
        }
    }
}
