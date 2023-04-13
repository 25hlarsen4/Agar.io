using System;
using System.Collections.Generic;
using System.Linq;
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
    /// This class represents Player objects, which are GameObjects with names.
    /// </summary>
    public class Player : GameObject
    {
        /// <summary>
        /// Creates a Player object with the base properties.
        /// </summary>
        /// <param name="ID"> the player id </param>
        /// <param name="ARGBColor"> the player color </param>
        /// <param name="Mass"> the player mass </param>
        /// <param name="X"> the player x-coord </param>
        /// <param name="Y"> the player y-coord </param>
        public Player(int ID, int ARGBColor, float Mass, float X, float Y) : base(ID, ARGBColor, Mass, X, Y)
        {
        }

        /// <summary>
        /// Represents the name of the player
        /// </summary>
        public string Name { get; set; }
    }
}
