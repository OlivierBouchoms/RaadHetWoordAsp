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
            bool logged = false;
            try
            {
                throw new ArgumentNullException(message: "Exception thrown by unit test", paramName: @"n/a");
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
            bool logged = false;
            try
            {
                throw new ArgumentNullException(message: "Exception thrown by unit test", paramName: @"n/a");
            }
            catch (Exception e)
            {
                logged = new ExceptionLogLogic(new ExceptionLogRepository(new ExceptionXMLContext())).LogException(e);
            }
            Assert.IsTrue(logged);
        }
    }
}