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
        int ID;

        int ARGBcolor;

        public float mass;

        // determine by considering the mass to be the area/vol
        public float radius;

        Vector2 position = new Vector2(100, 100);

        public float xPos
        {
            get { return position.X; }
        }

        public float yPos
        {
            get { return position.Y; }
        }
    }
}
