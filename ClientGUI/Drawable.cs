using AgarioModels;
using System.Drawing;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ClientGUI
{
    internal class Drawable : IDrawable
    {
        // this client stores the world
        public Client client = new Client();

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            foreach (Player player in client.world.players.Values)
            {
                // if the player is in bounds of the view portal, draw it, otherwise don't
                if (client.ConvertFromWorldToScreen(player.X, player.Y, player.getRadius(),
                    out int screen_x, out int screen_y, out int screen_radius))
                {
                    //Debug.WriteLine("screen X: " + screen_x + ", Y: " + screen_y + ", radius: " + screen_radius);
                    //Debug.WriteLine("drawing player " + player.ID);
                    canvas.StrokeColor = Colors.Black;
                    canvas.StrokeSize = 2;
                    canvas.FillColor = Colors.Blue;
                    canvas.FillCircle(screen_x, screen_y, screen_radius);
                }
            }

            foreach (Food food in client.world.foods.Values)
            {
                if (client.ConvertFromWorldToScreen(food.X, food.Y, food.getRadius(), 
                    out int screen_x, out int screen_y, out int screen_radius))
                {
                    //Debug.WriteLine("drawing food");
                    canvas.StrokeColor = Colors.Black;
                    canvas.StrokeSize = 2;
                    canvas.FillColor = Colors.Red;
                    canvas.FillCircle(screen_x, screen_y, screen_radius);
                }
            }
        }
    }
}
