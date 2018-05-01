using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Newtonsoft.Json;

namespace Tests
{
    [TestClass]
    public class JsonTests
    {
        private Wordlist _wordlist;

        private void Initialize()
        {
            _wordlist = new Wordlist(new List<string> { "woord1", "woord2", "woord3" }, "wordlist");
        }

        /// <summary>
        /// Testcase: nvt
        /// </summary>
        [TestMethod]
        public void TestSerializing()
        {
            Initialize();

            var jsonStringWordlist = SerializeWordlist(_wordlist.Words);

            for (int i = 0; i < _wordlist.Words.Count; i++)
            {
                Assert.IsTrue(_wordlist.Words[i] == DeSerializeWordlist(jsonStringWordlist)[i]);
            }
        }

        private string SerializeWordlist(List<string> input)
        {
            return JsonConvert.SerializeObject(input);
        }

        private List<string> DeSerializeWordlist(string input)
        {
            return JsonConvert.DeserializeObject<List<string>>(input);
        }
    }
}
