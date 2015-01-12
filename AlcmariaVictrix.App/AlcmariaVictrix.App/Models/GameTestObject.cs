using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcmariaVictrix.Shared.Models
{
    public class Games
    {
        public Game Game { get; set; }
        public Competition Competition { get; set; }
        public Gamefield Gamefield { get; set; }
    }

    public class GameRootObject
    {
        public List<Games> games { get; set; }
    }
}
