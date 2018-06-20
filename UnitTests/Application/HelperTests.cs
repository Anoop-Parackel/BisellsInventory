using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Application.Tests
{
    [TestClass()]
    public class HelperTests
    {
        [TestMethod()]
        public void IsValidDateTest()
        {
            string expected = "01/01/2017";
            string expected2 =null;
            bool result = expected.IsValidDate();
            bool result2 = expected2.IsValidDate();
            Assert.AreEqual(result, true);
            Assert.AreEqual(result2, false);
        }
        [TestMethod]
        public void IsValidEmailTest()
        {
            Assert.AreEqual("nithin@gmail.com".IsValidEmail(), true);
            Assert.AreEqual("nithin@gmail".IsValidEmail(), true);
            Assert.AreEqual("nithingmail.com".IsValidEmail(), false);
            Assert.AreEqual("nithin.com".IsValidEmail(), false);
        }
    }
}