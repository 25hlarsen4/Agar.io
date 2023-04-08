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

            foreach (Player player in world.players.Values)
            {
                // ConvertFromWorldToScreen
                ConvertFromWorldToScreen(player.X, player.Y, player.getRadius(), player.getRadius(),
                    out int screen_x, out int screen_y, out int screen_w, out int screen_h);

                // draw each circle
                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.FillColor = Colors.Blue;
                //canvas.FillCircle(player.xPos, player.yPos, player.radius);
                canvas.FillCircle(screen_x, screen_y, screen_w);
            }

            foreach (Food food in world.foods.Values)
            {
                // ConvertFromWorldToScreen
                ConvertFromWorldToScreen(food.X, food.Y, food.getRadius(), food.getRadius(), 
                    out int screen_x, out int screen_y, out int screen_w, out int screen_h);

                // draw each circle
                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.FillColor = Colors.Red;
                //canvas.FillCircle(food.X, food.Y, food.getRadius());
                canvas.FillCircle(screen_x, screen_y, screen_w);
            }
        }


        private void ConvertFromWorldToScreen(in float world_x, in float world_y, in float world_w, in float world_h,
                                                    out int screen_x, out int screen_y, out int screen_w, out int screen_h)
        {
            screen_x = (int)(world_x / 1500.0F * 800);
            screen_y = (int)(world_y / 1500.0F * 800);
            screen_w = (int)(world_w / 1500.0F * 800);
            screen_h = (int)(world_h / 1500.0F * 800);
        }
    }
}
