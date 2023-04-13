using AgarioModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    /// This class allows for the creation of a Client, which keeps track of who "this player" is and stores the World.
    /// </summary>
    internal class Client
    {
        /// <summary>
        /// Represents "this player"
        /// </summary>
        public Player thisPlayer;

        /// <summary>
        /// Keeps track of the current view portal width.
        /// </summary>
        public int currViewPortalWidth = 800;

        public long thisPlayersID;

        /// <summary>
        /// The game world.
        /// </summary>
        public World world = new World();

        /// <summary>
        /// This method converts from world coordinates to screen coordinates, since the server sends positions in 
        /// world coordinates and we must scale them to fit onto the screen display. This implementation provides a 
        /// view zoomed around "this player" and grows accordingly as "this player" grows.
        /// </summary>
        /// <param name="world_x"> the world x coordinate </param>
        /// <param name="world_y"> the world y coordinate </param>
        /// <param name="world_radius"> the world radius of the circle </param>
        /// <param name="screen_x"> the converted screen x coordinate </param>
        /// <param name="screen_y"> the converted screen y coordinate </param>
        /// <param name="screen_radius"> the converted screen radius of the circle </param>
        /// <returns></returns>
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
                    // calculate the percentage across the screen the object sits in world coordinates, and 
                    // make the converted screen coordinate follow that same ratio

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
