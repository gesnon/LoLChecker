using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoLChecker.Logic.Tests
{
    [TestClass]
    public class RiotApiTest
    {
        [TestMethod]
        public void CheckSummonerTest()
        {
            RiotAPI riotApiTest = new RiotAPI();

            string name = "gesnons";

            var summoner = riotApiTest.GetSummoner(name);

            Assert.AreEqual(name, summoner.Name);

            Assert.AreEqual(171, summoner.Level);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetMatchHistoryForChampionAsync()
        {
            RiotAPI riotApiTest = new RiotAPI();

            string name = "gesnons";

            string championName = "lucian";

            var summoner = riotApiTest.GetSummoner(name);

            var matchHistory = await riotApiTest.GetMatchHistoryForChampionAsync(summoner.AccountId,championName );

            Assert.AreEqual(5, matchHistory.Count);
                      
        }
    }
}
