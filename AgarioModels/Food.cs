using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgarioModels
{
    public class Food : GameObject
    {
        public Food(int ID, int ARGBColor, float Mass, float X, float Y) : base(ID, ARGBColor, Mass, X, Y)
        {
        }
    }
}
