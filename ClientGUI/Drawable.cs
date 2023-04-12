using AgarioModels;
using System.Drawing;
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
        // this client stores the world
        public Client client = new Client();

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            //HashSet<AgarioModels.Players> players = world.players;
            //HashSet<Food> foods = world.food;

            foreach (Player player in client.world.players.Values)
            {
                // ConvertFromWorldToScreen
                client.ConvertFromWorldToScreen(player.X, player.Y, player.getRadius(),
                    out int screen_x, out int screen_y, out int screen_radius);

                // draw each circle
                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.FillColor = Colors.Blue;
                //canvas.FillCircle(player.xPos, player.yPos, player.radius);
                canvas.FillCircle(screen_x, screen_y, screen_radius);
            }

            foreach (Food food in client.world.foods.Values)
            {
                // ConvertFromWorldToScreen
                client.ConvertFromWorldToScreen(food.X, food.Y, food.getRadius(), 
                    out int screen_x, out int screen_y, out int screen_radius);

                // draw each circle
                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.FillColor = Colors.Red;
                //canvas.FillCircle(food.X, food.Y, food.getRadius());
                canvas.FillCircle(screen_x, screen_y, screen_radius);
            }
        }
    }
}
