using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AlcmariaVictrix.Shared.Models
{
    public class Team
    {
        [JsonProperty("team_id")]
        public int Id { get; set; }

        [JsonProperty("sport_id")]
        public int SportId { get; set; }

        [JsonProperty("team_naam")]
        public string Name { get; set; }

        [JsonProperty("team_naamkort")]
        public string ShortName { get; set; }
    }
}
