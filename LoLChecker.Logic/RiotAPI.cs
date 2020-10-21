using RiotSharp;
using RiotSharp.Endpoints.MatchEndpoint;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;
using RiotSharp.Endpoints.SummonerEndpoint;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace LoLChecker.Logic
{
    public class RiotAPI
    {
        private RiotApi _api;
        public RiotAPI ()
        {
           _api = RiotApi.GetDevelopmentInstance("RGAPI-1257aac3-ee80-48cf-8d4b-76b4e99e9f94");
        }
         
        public Summoner GetSummoner(String name)
        {
           return _api.Summoner.GetSummonerByNameAsync(RiotSharp.Misc.Region.Euw, name).Result;            
        }

        public async System.Threading.Tasks.Task<List<Match>> GetMatchHistoryAsync(string id)
        {
            var matchList = await _api.Match.GetMatchListAsync(RiotSharp.Misc.Region.Euw, id);

            List<Match> matchesList = new List<Match>(); 

            foreach(var matchReference in matchList.Matches)
            {

                var match = await _api.Match.GetMatchAsync(RiotSharp.Misc.Region.Euw, matchReference.GameId);

                matchesList.Add(match);
            }

            return matchesList;

        }
        public ChampionStatic GetChampion(string name)
        {
            ChampionStatic Champion = _api.StaticData.Champions.GetByKeyAsync(name, "10.20.1").Result;

            return Champion;
        }

        
        public async System.Threading.Tasks.Task<List<Match>> GetMatchHistoryForChampionAsync(string id, string name)
        {
            List<int> ChampionId = new List<int> { 51 };

            var matchList = await _api.Match.GetMatchListAsync(RiotSharp.Misc.Region.Euw, id, ChampionId);

            List<Match> matchesList = new List<Match>();

            foreach (var matchReference in matchList.Matches)
            {
                if (matchesList.Count > 10)
                {
                    return matchesList;
                }

                var match = await _api.Match.GetMatchAsync(RiotSharp.Misc.Region.Euw, matchReference.GameId);

                matchesList.Add(match);
            }
                      

            return matchesList;

        }

        public async System.Threading.Tasks.Task<float> GetWinratioAgainstChampionAsync(string id, string name)
        {
            var matches = await GetMatchHistoryForChampionAsync(id, name);

            foreach (var game in matches)
            {

            }
        }

    }
}
