using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AlcmariaVictrix.Shared.Models
{
    public class Competitions
    {
        public Competition Competition { get; set; }
        public Season Season { get; set; }
        public Team Team { get; set; }
        public List<Result> Result { get; set; }
        public List<Game> Game { get; set; }
        public List<Standing> Standing { get; set; }
    }

    public class RootObject
    {
        public Competitions competitions { get; set; }
    }
}
