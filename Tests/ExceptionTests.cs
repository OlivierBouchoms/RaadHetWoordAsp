using System;
using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ExceptionTests
    { 
        /// <summary>
        /// Testcase: nvt
        /// </summary>
        [TestMethod]
        public void TestSqLiteLogging()
        {
            var logged = false;
            try
            {
                throw new ArgumentNullException();
            }
            catch (Exception e)
            {
                logged = new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionSqLiteContext())).LogException(e);
            }
            Assert.IsTrue(logged);
        }

        /// <summary>
        /// Testcase: nvt
        /// </summary>
        [TestMethod]
        public void TestXmlLogging()
        {
            var logged = false;
            try
            {
                throw new ArgumentNullException();
            }
            catch (Exception e)
            {
                logged = new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionXMLContext())).LogException(e);
            }
            Assert.IsTrue(logged);
        }
    }
}