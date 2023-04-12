using AgarioModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGUI
{
    internal class Client
    {
        public Player thisPlayer;
        public int currViewPortalWidth = 800;
        public long thisPlayersID;

        public World world = new World();

        internal bool ConvertFromWorldToScreen(in float world_x, in float world_y, in float world_radius,
                                                    out int screen_x, out int screen_y, out int screen_radius)
        {
            // if not currently playing, don't need to worry about zoomed portal view
            if (thisPlayer == null)
            {
                screen_x = (int)(world_x / world.width * 800);
                screen_y = (int)(world_y / world.height * 800);
                screen_radius = (int)(world_radius / world.width * 800);

                return true;
            }

            else
            {
                currViewPortalWidth = (int)(150 * Math.Cbrt(thisPlayer.getRadius()));

                int leftBoundary = (int)(thisPlayer.X - (currViewPortalWidth / 2));
                //Debug.WriteLine(thisPlayer.X);

                int rightBoundary = (int)(thisPlayer.X + (currViewPortalWidth / 2));

                int bottomBoundary = (int)(thisPlayer.Y + (currViewPortalWidth / 2));

                int topBoundary = (int)(thisPlayer.Y - (currViewPortalWidth / 2));

                // if the object is out of bounds, don't worry about converting or drawing it
                if (world_x < leftBoundary || world_x > rightBoundary || world_y > bottomBoundary || world_y < topBoundary)
                {
                    screen_x = 0;
                    screen_y = 0;
                    screen_radius = 0;
                    return false;
                }

                else
                {
                    int horizOffset = (int)(world_x - leftBoundary);
                    double widthPercentage = ((double) horizOffset) / currViewPortalWidth;

                    int vertOffset = (int)(world_y - topBoundary);
                    double heightPercentage = ((double)vertOffset) / currViewPortalWidth;

                    screen_x = (int)(widthPercentage * 800);
                    screen_y = (int)(heightPercentage * 800);

                    double radiusPercentage = ((double)world_radius / currViewPortalWidth);
                    screen_radius = (int)(radiusPercentage * 800);

                    return true;
                }
            }
        }
    }
}
