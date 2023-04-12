using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgarioModels
{
    public class Player : GameObject
    {
        public Player(int ID, int ARGBColor, float Mass, float X, float Y) : base(ID, ARGBColor, Mass, X, Y)
        {
        }

        public string Name { get; set; }
    }
}
