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
    internal class World
    {
        // also store a logger

        readonly int width = 5000;
        readonly int height = 5000;

        HashSet<Player> players;
        HashSet<Food> food;
    }
}
