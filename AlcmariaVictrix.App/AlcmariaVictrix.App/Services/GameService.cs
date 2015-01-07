using AlcmariaVictrix.Shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcmariaVictrix.Shared.Services
{
    public class GameService : IGameService
    {
        private const string BaseUrL = "http://alcmariavictrix.nl/cakephp/";

        // Key provided by Met Office Datapoint service. 
        // Please obtain a free key from http://www.metoffice.gov.uk/datapoint
        private const string Key = "place met office datapoint api key here";

        public async Task<Models.Game[]> GetGames()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Competition>> GetCompetitions()
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

        public async Task<Models.Game> GetGame(int id)
        {
            throw new NotImplementedException();
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


        public async Task<Competition> GetCompetitionInfo(int competitionId)
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
                competition.Game = root.competitions.Game;
                competition.Result = root.competitions.Result;
                competition.Standing = root.competitions.Standing;
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
    }
}
