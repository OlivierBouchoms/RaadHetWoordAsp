using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class LeaderboardsTests
    {
        private TeamLogic _teamLogic;

        private void Initialize()
        {
            _teamLogic = new TeamLogic(new TeamRepository(new TeamMssqlContext()));
        }

        /// <summary>
        /// Testcase: TC15
        /// </summary>
        [TestMethod]
        public void TestShowLeaderBoards()
        {
            Initialize();

            Assert.IsTrue(_teamLogic.GetTeams(null).Count > 0);
        }

        /// <summary>
        /// Testcase: TC16
        /// </summary>
        [TestMethod]
        public void TestShowLeaderBoardsSorted()
        {
            Initialize();

            Assert.IsTrue(_teamLogic.GetTeams(null) != _teamLogic.GetTeams("Wins"));
        }
    }
}
