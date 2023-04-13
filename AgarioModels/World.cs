using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AgarioModels
{
    /// <summary>
    /// Authors:     Hannah Larsen & Todd Oldham
    /// Date:        05-April-2023
    /// Course:      CS3500, University of Utah, School of Computing
    /// Copyright:   CS3500, Hannah Larsen, and Todd Oldham - This work may not be copied for use in academic coursework.
    /// 
    /// We, Hannah Larsen and Todd Oldham, certify that we wrote this code from scratch and did not copy it in part or whole 
    /// from another source.
    /// All references used in the completion of the assignment are cited in our README file.
    /// 
    /// File Contents:
    /// This class represents the "state" of the game simulation.
    /// </summary>
    public class World
    {
        /// <summary>
        /// A logger to log debugging information
        /// </summary>
        public ILogger _logger;

        /// <summary>
        /// The width of the world.
        /// </summary>
        public readonly int width = 5000;

        /// <summary>
        /// The height of the world.
        /// </summary>
        public readonly int height = 5000;

        /// <summary>
        /// The Dictionary that keeps track of all current players and their IDs
        /// </summary>
        public Dictionary<long, Player> players = new Dictionary<long, Player>();

        /// <summary>
        /// The Dictionary that keeps track of all current food and their IDs
        /// </summary>
        public Dictionary<long, Food> foods = new Dictionary<long, Food>();

        //public World(ILogger<World> logger) 
        //{ 
        //    _logger = logger;
        //}

    }
}
