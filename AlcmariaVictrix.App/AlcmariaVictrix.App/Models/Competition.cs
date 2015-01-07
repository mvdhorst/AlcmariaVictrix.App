using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AlcmariaVictrix.Shared.Models
{
    public class Competition
    {
        public int Id
        {
            get
            {
                int id = 0;
                Int32.TryParse(Competition_id, out id);
                return id;
            }
        }
        [JsonProperty("competition_id")]
        public string Competition_id { get; set; }
        public string Season_id { get; set; }
        public string Team_id { get; set; }
        public string Lookup_name { get; set; }
        public string Name { get; set; }
        public string Team_picture { get; set; }

        public Team Team { get; set; }
        public List<Result> Result { get; set; }
        public List<Game> Game { get; set; }
        public List<Standing> Standing { get; set; }
    }
}
