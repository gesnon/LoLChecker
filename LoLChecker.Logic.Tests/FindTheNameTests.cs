using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoLChecker.Logic;
namespace LoLChecker.Logic.Tests
{
    [TestClass]
    public class FindTheNameTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            FindTheName TestFinds = new FindTheName();

            string lobbystarts = @"gesnons joined the lobby
Dârcy joined the lobby
JeFumeLaKush joined the lobby
SiteDeFouille64 joined the lobby
TheFrenchAnkulé joined the lobby";

            string[] teammate =  TestFinds.Teammates(lobbystarts);

            string[] correctArray = new string[] { "gesnons", "Dârcy", "JeFumeLaKush", "SiteDeFouille64", "TheFrenchAnkulé" };

            Assert.AreEqual(correctArray.Length, teammate.Length);

            CollectionAssert.AreEqual(correctArray, teammate);
        }

    }
}
