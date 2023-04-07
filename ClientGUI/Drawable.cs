using AgarioModels;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGUI
{
    internal class Drawable : IDrawable
    {
        public World world = new World();

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            //HashSet<AgarioModels.Players> players = world.players;
            //HashSet<Food> foods = world.food;

            //foreach (Player player in world.players.Values)
            //{
            //    // ConvertFromWorldToScreen

            //    // draw each circle
            //    canvas.StrokeColor = Colors.Black;
            //    canvas.StrokeSize = 2;
            //    canvas.FillColor = Colors.Red;
            //    canvas.FillCircle(player.xPos, player.yPos, player.radius);
            //}

            foreach (Food food in world.foods.Values)
            {
                // ConvertFromWorldToScreen

                // draw each circle
                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.FillColor = Colors.Red;
                canvas.FillCircle(food.X, food.Y, food.getRadius());
            }
        }
    }
}
