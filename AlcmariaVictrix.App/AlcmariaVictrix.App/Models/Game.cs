using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AlcmariaVictrix.Shared.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Game_id { get; set; }

        public string GameNumber { get; set; }

        public Competition Competition { get; set; }

        public Field Field { get; set; }

        public int GameType { get; set; }

        public DateTime GameTime { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }

        public string Umpire { get; set; }

        public string Competition_id { get; set; }
        public string Gamefield_id { get; set; }
        public object Changingroom_id { get; set; }
        public object Changingroom_away_id { get; set; }
        public string Gametype_id { get; set; }
        public string Game_number { get; set; }
        public bool? Canceled { get; set; }
        public DateTime? Game_date { get; set; }
        public DateTime? Time_present { get; set; }
        public string Home { get; set; }
        public string Away { get; set; }
        public string Score_home { get; set; }
        public string Score_away { get; set; }
        public bool Home_game { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
        public string Last_modified_by { get; set; }
        public bool? Active { get; set; }
        public bool? Game_ended { get; set; }
        public string Internal_comments { get; set; }
        public string External_comments { get; set; }
        public string Venue { get; set; }
        public object Competition2_id { get; set; }

    }
}
