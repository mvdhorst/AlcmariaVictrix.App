using Akavache;
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
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            Debug.WriteLine("Getting games");
            var cache = BlobCache.LocalMachine;
            //await cache.InvalidateAll();
            var cachedGames = cache.GetAndFetchLatest("games", async () => await GetGamesAsync(),
                offset =>
                {
                    TimeSpan elapsed = DateTimeOffset.Now - offset;
                    return elapsed > new TimeSpan(hours: 0, minutes: 0, seconds: 20);
                }
                , new DateTimeOffset(DateTime.UtcNow.AddSeconds(30)));
                //offset =>
                //{
                //    TimeSpan elapsed = DateTimeOffset.Now - offset;
                //    return elapsed > new TimeSpan(hours: 0, minutes: 1, seconds: 0);
                //});

            var games = await cachedGames.FirstOrDefaultAsync();
            return games;
        }

        public async Task<System.Collections.Generic.IEnumerable<Competition>> GetCompetitions()
        {
            return await GetCompetitionsAsync();
        }

        public async Task<Game> GetGame(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Competition> GetCompetitionInfo(int id)
        {
            return await GetCompetitionInfoAsync(id);
        }

        private async Task<List<Game>> GetGamesAsync()
        {
            Debug.WriteLine("Getting games from webservice async");
            List<Game> games = null;
            Task<List<Game>> getGamesTask;
            getGamesTask = GetGamesAsync2();
            //if (CrossConnectivity.Current.IsConnected)
            {
                
                games = await Policy
                      .Handle<WebException>()
                      .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (_, timeSpan) => getGamesTask.RunSynchronously())
                      //(
                      //  retryCount: 5,
                      //  sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                      //)
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



        public Task<IEnumerable<FeedItem>> GetNewsItems()
        {
            return ExecuteLoadItemsCommand();
        }

        private const string _url = "http://www.alcmariavictrix.nl/feeds/nieuws-hsb.rss";
        bool IsBusy = false;

        private async Task<IEnumerable<FeedItem>> ExecuteLoadItemsCommand()
        {
            List<FeedItem> FeedItems = new List<FeedItem>();
            if (IsBusy)
                return null;

            IsBusy = true;

            try
            {
                var httpClient = new HttpClient();
                var responseString = await httpClient.GetStringAsync(_url);

                FeedItems.Clear();
                var items = await ParseFeed(responseString);
                foreach (var item in items)
                {
                    FeedItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw new FormatException("Failed to load news items.", ex);
            }

            IsBusy = false;
            return FeedItems;
        }

        private async Task<List<FeedItem>> ParseFeed(string rss)
        {
            return await Task.Run(() =>
            {
                var xdoc = XDocument.Parse(rss);
                var id = 0;
                return (from item in xdoc.Descendants("item")
                        select new FeedItem
                        {
                            Title = (string)item.Element("title"),
                            Description = (string)item.Element("description"),
                            Link = (string)item.Element("link"),
                            PublishDate = (string)item.Element("pubDate"),
                            Category = (string)item.Element("category"),
                            Id = id++
                        }).ToList();
            });
        }
    }
}
