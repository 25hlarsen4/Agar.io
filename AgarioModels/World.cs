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

        readonly int width = 5000;
        readonly int height = 5000;

        public HashSet<Player> players;
        public HashSet<Food> food;

        public World()
        {
            //this.logger = logger;
        }
    }
}
