using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgarioModels
{
    public class GameObject
    {
        [JsonIgnore]
        public Vector2 position { get; set; }

        public int ID { get; set; }

        public int ARGBColor { get; set; } 

        public float Mass { get; set; }

        public float X
        {
            //get { return position.X; }
            //set { }
            get; set;
        }

        public float Y
        {
            //get { return position.Y; }
            //set { }
            get; set;
        }
        public float getRadius()
        {
            return (float)Math.Sqrt(Mass / float.Pi);
        }



    }
}
