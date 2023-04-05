using AgarioModels;
using AndroidX.ConstraintLayout.Helper.Widget;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Graphics.ColorSpace;

namespace ClientGUI
{
    internal class Drawable : IDrawable
    {
        public World world = new World();

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            HashSet<AgarioModels.Players> players = world.players;
            HashSet<Food> foods = world.food;

            foreach (Player player in players)
            {
                //draw each circle
                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.FillColor = Colors.Red;
                canvas.FillCircle(player.xPos, player.yPos, player.radius);
            }

            foreach (Food food in foods)
            {
                //draw each circle
                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.FillColor = Colors.Red;
                canvas.FillCircle(food.xPos, food.yPos, food.radius);
            }
        }
    }
}
