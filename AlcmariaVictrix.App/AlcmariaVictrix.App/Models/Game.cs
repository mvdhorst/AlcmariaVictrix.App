using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AlcmariaVictrix.Shared.Models
{
    public class Game
    {

        public DateTime DateSort
        {
            get
            {
                return GameDate.Date;
            }
        }

        [JsonProperty("game_id")]
        public int Id { get; set; }

        [JsonProperty("game_number")]
        public string GameNumber { get; set; }

        public Competition Competition { get; set; }

        public Gamefield Field { get; set; }

        public int GameType { get; set; }

        [JsonProperty("game_date")]
        public DateTime GameDate { get; set; }

        [JsonProperty("home")]
        public string HomeTeam { get; set; }

        [JsonProperty("away")]
        public string AwayTeam { get; set; }

        [JsonProperty("score_home")]
        public string HomeScore { get; set; }

        [JsonProperty("score_away")]
        public string AwayScore { get; set; }

        public string Umpire { get; set; }
        public string Competition_id { get; set; }
        public string Gamefield_id { get; set; }
        public object Changingroom_id { get; set; }
        public object Changingroom_away_id { get; set; }
        public string Gametype_id { get; set; }
        public bool? Canceled { get; set; }
        public DateTime? Time_present { get; set; }
        public bool Home_game { get; set; }
        public string Venue { get; set; }
        public object Competition2_id { get; set; }

    }
}
