using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class WordlistTests
    {
        private WordListLogic _wordListLogic;

        public void Initialize()
        {
            _wordListLogic = new WordListLogic(new WordListRepository(new WordListMssqlContext()));
        }

        /// <summary>
        /// Testcase: TC06, TC17
        /// </summary>
        [TestMethod]
        public void TestChosenWordlist()
        {
            Initialize();
            var wordlists = _wordListLogic.GetWordlists();
            var allwords = _wordListLogic.GetWords(null);
            var wordlist = _wordListLogic.GetWords(wordlists[0]);

            Assert.IsTrue(allwords.Count > wordlist.Count);
        }

        /// <summary>
        /// Testcase: TC07, TC18
        /// </summary>
        [TestMethod]
        public void TestNoChosenWordlist()
        {
            Initialize();

            Assert.IsTrue(_wordListLogic.GetWords(null).Count > 0);
        }

    }
}
