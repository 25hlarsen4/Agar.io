using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AgarioModels
{
    /// <summary>
    /// This class represents the "state" of the simulation
    /// </summary>
    public class World
    {
        //readonly ILogger logger;

        public readonly int width = 5000;
        public readonly int height = 5000;

        public Dictionary<long, Player> players;
        public Dictionary<long, Food> foods;

        public World()
        {
            players = new Dictionary<long, Player>();
            foods = new Dictionary<long, Food>();
            //this.logger = logger;
        }
    }
}
