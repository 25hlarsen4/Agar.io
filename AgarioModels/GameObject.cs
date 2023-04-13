using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
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
    /// This class represents GameObjects, which can be both players and foods.
    /// </summary>
    public class GameObject
    {
        /// <summary>
        /// This constructor allows for JSON to correctly deserialize.
        /// </summary>
        /// <param name="ID"> the id </param>
        /// <param name="ARGBColor"> the color </param>
        /// <param name="Mass"> the mass </param>
        /// <param name="X"> the x position </param>
        /// <param name="Y"> the y position </param>
        [JsonConstructor]
        public GameObject(int ID, int ARGBColor, float Mass, float X, float Y) 
        { 
            this.ID = ID;
            this.ARGBColor = ARGBColor;
            this.Mass = Mass;
            this.X = X;
            this.Y = Y;
        }


        [JsonIgnore]
        public Vector2 position { get; set; }

        /// <summary>
        /// Represents the ID of a GameObject
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Represents the color of a GameObject
        /// </summary>
        public int ARGBColor { get; set; }

        /// <summary>
        /// Represents the mass of a GameObject
        /// </summary>
        public float Mass { get; set; }

        /// <summary>
        /// Represents the x position of a GameObject
        /// </summary>
        public float X
        {
            get; private set;
        }

        /// <summary>
        /// Represents the y position of a GameObject
        /// </summary>
        public float Y
        {
            get; private set;
        }

        /// <summary>
        /// A getter for a GameObject's radius which is needed for 
        /// drawing purposes.
        /// </summary>
        /// <returns></returns>
        public float getRadius()
        {
            return (float)Math.Sqrt(Mass / float.Pi);
        }
    }
}
