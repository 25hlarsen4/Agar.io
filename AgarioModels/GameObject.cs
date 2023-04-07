using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AgarioModels
{
    public class GameObject
    {

        public Vector2 position;

        public int ID { get; set; }

        public int ARGBColor { get; set; } 

        public float Mass { get; set; }

        // determine by considering the mass to be the area/vol
        private float radius;
        

        public float getRadius()
        {
           return (float)Math.Sqrt(Mass / float.Pi); 
        }

        public float X { get; set; }

        public float Y { get; set; }


            
    }
}
