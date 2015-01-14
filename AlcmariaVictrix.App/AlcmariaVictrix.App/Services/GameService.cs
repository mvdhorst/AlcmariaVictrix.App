﻿using Akavache;
using AlcmariaVictrix.Shared.Models;
using Connectivity.Plugin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace AlcmariaVictrix.Shared.Services
{
    public class GameService : IGameService
    {
        private const string BaseUrL = "http://alcmariavictrix.nl/cakephp/";

        // Key provided by Met Office Datapoint service. 
        // Please obtain a free key from http://www.metoffice.gov.uk/datapoint
        private const string Key = "place met office datapoint api key here";



        public async Task<List<Game>> GetGames()
        {
            var cache = BlobCache.LocalMachine;
            var cachedGames = cache.GetAndFetchLatest("games", () => GetGamesAsync(),
                offset =>
                {
                    TimeSpan elapsed = DateTimeOffset.Now - offset;
                    return elapsed > new TimeSpan(hours: 24, minutes: 0, seconds: 0);
                });

            var games = await cachedGames.FirstOrDefaultAsync();
            return games;
        }

        public async Task<System.Collections.Generic.IEnumerable<Competition>> GetCompetitions()
        {
            throw new NotImplementedException();
        }

        public async Task<Game> GetGame(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Competition> GetCompetitionInfo(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<List<Game>> GetGamesAsync()
        {

            List<Game> games = null;
            Task<List<Game>> getGamesTask;
            getGamesTask = GetGamesAsync2();
            if (CrossConnectivity.Current.IsConnected)
            {
                games = await Policy
                      .Handle<WebException>()
                      .WaitAndRetry
                      (
                        retryCount: 5,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                      )
                      .ExecuteAsync(async () => await getGamesTask);
            }
            return games;
        }

        private async Task<List<Game>> GetGamesAsync2()
        {
            string result = null;

            try
            {
                Debug.WriteLine("Getting games from Alcmaria Service...");
                result = await Get("games/honksoftbalxml");
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get competition from service provider. The service maybe down. Retry or try again later.", ex);
            }

            List<Game> games;

            try
            {
                //var comp = JObject.Parse(result)["competitions"];
                GameRootObject root = JsonConvert.DeserializeObject<GameRootObject>(result);
                games = root.games.Select(g => new Game
                {
                    HomeTeam = g.Game.HomeTeam,
                    AwayTeam = g.Game.AwayTeam,
                    GameDate = g.Game.GameDate,
                    GameNumber = g.Game.GameNumber,
                    Competition = g.Competition,
                    Field = g.Gamefield
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new FormatException("Failed to format competition.", ex);
            }

            return games;
        }

        private async Task<IEnumerable<Competition>> GetCompetitionsAsync()
        {
            string result = null;

            try
            {
                Debug.WriteLine("Getting competitions from Alcmaria Service...");
                result = await Get("competitions/json");
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get competitions from service provider. The service maybe down. Retry or try again later.", ex);
            }

            Competition[] competitions;

            try
            {
                var competitionToken = JObject.Parse(result)["competitions"];
                try
                {
                    RootObject root = JsonConvert.DeserializeObject<RootObject>(result);
                }
                catch
                {
                    Debug.WriteLine(" test");
                }

                competitions = competitionToken.Select(comp =>
                {

                    var competition = new Competition
                    {
                        Competition_id = (string)(comp["Competition"]["competition_id"] ?? ""),
                        Name = (string)(comp["Competition"]["name"] ?? ""),
                        Team = new Team
                        {                            
                        Name = (string)(comp["Team"]["team_naam"] ?? ""), 
                        ShortName = (string)(comp["Team"]["team_naamkort"] ?? ""),
                        }
                    };
                    int id = 0;
                    //if(Int32.TryParse((string)(comp["Competition"]["competition_id"] ?? ""), out id))
                    //    competition.Id = id;
                    if (Int32.TryParse((string)(comp["Team"]["team_id"] ?? ""), out id))
                        competition.Team.Id = id;
                    if(Int32.TryParse((string)(comp["Team"]["sport_id"] ?? ""), out id))
                        competition.Team.SportId = id;

                    return competition;

                }).ToArray();
            }
            catch (Exception ex)
            {
                throw new FormatException("Failed to format competitions.", ex);
            }

            return competitions;
        }

        private async Task<Models.Game> GetGameAsync(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<Competition> GetCompetitionInfoAsync(int competitionId)
        {
            string result = null;

            try
            {
                Debug.WriteLine("Getting competition " + competitionId + " from Alcmaria Service...");
                result = await Get("competitions/view/" + competitionId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get competition from service provider. The service maybe down. Retry or try again later.", ex);
            }

            Competition competition;

            try
            {
                //var comp = JObject.Parse(result)["competitions"];
                RootObject root = JsonConvert.DeserializeObject<RootObject>(result);

                competition = root.competitions.Competition;
                competition.Games = root.competitions.Game;
                competition.Results = root.competitions.Result;
                competition.Standings = root.competitions.Standing;
                competition.Team = root.competitions.Team;

                //competition = new Competition
                //{
                //    Competition_id = (string)(comp["Competition"]["competition_id"] ?? ""),
                //       Name = (string)(comp["Competition"]["name"] ?? ""),
                //       Team = new Team
                //       {
                //           Name = (string)(comp["Team"]["team_naam"] ?? ""),
                //           ShortName = (string)(comp["Team"]["team_naamkort"] ?? ""),
                //       }
                //   };
                //   int id = 0;
                //   //if (Int32.TryParse((string)(comp["Competition"]["competition_id"] ?? ""), out id))
                //   //    competition.Id = id;
                //   if (Int32.TryParse((string)(comp["Team"]["team_id"] ?? ""), out id))
                //       competition.Team.Id = id;
                //   if (Int32.TryParse((string)(comp["Team"]["sport_id"] ?? ""), out id))
                //       competition.Team.SportId = id;

                //   //return competition;               
            }
            catch (Exception ex)
            {
                throw new FormatException("Failed to format competition.", ex);
            }

            return competition;
        }

        private async Task<string> Get(string uri)
        {
            var client = new System.Net.Http.HttpClient();

            client.BaseAddress = new Uri(BaseUrL);

            var response = await client.GetAsync(string.Format("{0}?key={1}", uri, Key));

            response.EnsureSuccessStatusCode();

            var result = response.Content.ReadAsStringAsync().Result;
            return result;
        }

    }
}
