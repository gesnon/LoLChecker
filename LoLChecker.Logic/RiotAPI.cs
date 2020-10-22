using LoLChecker.Logic.Models;
using RiotSharp;
using RiotSharp.Endpoints.MatchEndpoint;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;
using RiotSharp.Endpoints.SummonerEndpoint;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace LoLChecker.Logic
{
    public class RiotAPI
    {
        private RiotApi _api;
        public RiotAPI ()
        {
           _api = RiotApi.GetDevelopmentInstance("RGAPI-84171906-8f89-41ab-9b06-5c2911300015");
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

        
        public async System.Threading.Tasks.Task<List<Match>> GetMatchHistoryForSummonerAsync(string id)
        {
            var matchList = await _api.Match.GetMatchListAsync(RiotSharp.Misc.Region.Euw, id);

            List<Match> matchesList = new List<Match>();

            foreach (var matchReference in matchList.Matches)
            {
                if (matchesList.Count > 80)
                {
                    return matchesList;
                }

                var match = await _api.Match.GetMatchAsync(RiotSharp.Misc.Region.Euw, matchReference.GameId);

                matchesList.Add(match);
            }
                      

            return matchesList;

        }

        public async System.Threading.Tasks.Task<WinRatioAgainstChampion> GetWinratioAgainstChampionAsync(string id, string name)
        {
            int championID = GetChampion(name).Id;
            WinRatioAgainstChampion statictic = new WinRatioAgainstChampion();     
            
            List<Match> matchesList = await GetMatchHistoryForSummonerAsync(id);                    
           
            foreach (var game in matchesList)
            {
                var player = game.Participants.FirstOrDefault(q => q.ChampionId == championID);

                if (player == null) { continue; }

                int myParticipantID = game.ParticipantIdentities.FirstOrDefault(q => q.Player.AccountId == id).ParticipantId;
                
                int myTeamID = game.Participants.FirstOrDefault(q => q.ParticipantId == myParticipantID).TeamId;
                
                if (player.TeamId != myTeamID)
                {
                    statictic.AmountGames++;

                    if (!player.Stats.Winner)
                    {
                        statictic.AmountWins++;
                    }

                }
            }

            statictic.Winratio = statictic.AmountWins / (float)statictic.AmountGames;            

            return statictic;
        }

    }
}
