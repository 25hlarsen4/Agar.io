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
    /// This class allows for the world to be constantly redrawn.
    /// </summary>
    internal class Drawable : IDrawable
    {
        /// <summary>
        /// This Client stores the World
        /// </summary>
        public Client client = new Client();

        /// <summary>
        /// This method does the drawing.
        /// </summary>
        /// <param name="canvas"> the canvas to draw on </param>
        /// <param name="dirtyRect"> the dirtyRect </param>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            foreach (Player player in client.world.players.Values)
            {
                // if the player is in bounds of the view portal, draw it, otherwise don't
                if (client.ConvertFromWorldToScreen(player.X, player.Y, player.getRadius(),
                    out int screen_x, out int screen_y, out int screen_radius))
                {
                    Microsoft.Maui.Graphics.Color color = Microsoft.Maui.Graphics.Color.FromInt(player.ARGBColor);
                    canvas.StrokeColor = color;
                    canvas.StrokeSize = 2;
                    canvas.FillColor = color;
                    canvas.FillCircle(screen_x, screen_y, screen_radius);

                    // draw the player name
                    canvas.FontColor = Colors.Black;
                    canvas.FontSize = (float)(4 * Math.Sqrt(player.getRadius()));
                    canvas.Font = Microsoft.Maui.Graphics.Font.Default;
                    canvas.DrawString(player.Name, screen_x - 100, (screen_y - 50 ), 200, 100, HorizontalAlignment.Center, VerticalAlignment.Center);
                }
            }

            foreach (Food food in client.world.foods.Values)
            {
                if (client.ConvertFromWorldToScreen(food.X, food.Y, food.getRadius(), 
                    out int screen_x, out int screen_y, out int screen_radius))
                {
                    Microsoft.Maui.Graphics.Color color = Microsoft.Maui.Graphics.Color.FromInt(food.ARGBColor);
                    canvas.StrokeColor = color;
                    canvas.StrokeSize = 2;
                    canvas.FillColor = color;
                    canvas.FillCircle(screen_x, screen_y, screen_radius);
                }
            }
        }
    }
}
