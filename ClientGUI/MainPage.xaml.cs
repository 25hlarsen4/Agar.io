using AgarioModels;
using Communications;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json;
using System.Diagnostics;
using System.Timers;
using Microsoft.Extensions.Logging;

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
    /// This class provides the backing code for the agario GUI xaml code. It allows for
    /// a client to connect to the game, play it, and either play again or quit upon their death.
    /// </summary>
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// The Drawable object that allows for the game world to be continuously redrawn.
        /// This object stores a Client object, which keeps track of "this player" playing the game
        /// and stores a World object, which keeps track of the state of the game world.
        /// </summary>
        Drawable drawable;

        /// <summary>
        /// The Networking object that allows this player to connect to the server and play the game.
        /// </summary>
        Networking channel;

        /// <summary>
        /// Stores the current mouse position.
        /// </summary>
        Point mousePosition;

        /// <summary>
        /// A stopwatch used to track the amount of time a player has stayed alive to include
        /// in their final stats upon their death.
        /// </summary>
        Stopwatch stopWatch;

        /// <summary>
        /// This starts the application.
        /// </summary>

        public MainPage(ILogger<MainPage> logger)
        {
            InitializeComponent();

            drawable = new Drawable();

            PlaySurface.Drawable = drawable;

            drawable.client.world._logger = logger;

            channel = new Networking(logger, OnConnect, OnDisconnect, OnMessageReceived, '\n');

            stopWatch = new Stopwatch();
        }

        /// <summary>
        /// This method ensures that when a client enters their name, the game screen is made
        /// visible and an attempt to connect to the server is made.
        /// </summary>
        /// <param name="sender"> the sender </param>
        /// <param name="e">the event args </param>
        private async void OnNameEntryCompleted(object sender, EventArgs e)
        {
            WelcomeScreen.IsVisible = false;
            GameScreen.IsVisible = true;

            // focus the entry that keeps track of keyboard input to handle when a player
            // wants to split
            SpaceBarEntry.Focus();

            try
            {
                channel.Connect(ServerEntry.Text, 11000);
                drawable.client.world._logger.LogDebug(" successfully connected ");
                channel.ClientAwaitMessagesAsync();

                System.Timers.Timer timer = new System.Timers.Timer(33);
                timer.Elapsed += TickEvent;
                timer.Start();
            } catch
            {
                await DisplayAlert("Alert", "Unable to connect :(", "OK");
            }
        }

        /// <summary>
        /// This method continuously updates the global mousePosition variable so we know at all 
        /// times where to send move requests to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointerChanged(object sender, PointerEventArgs e)
        {
            Point windowPosition = (Point)e.GetPosition(null);
            mousePosition = windowPosition;
        }

        /// <summary>
        /// This method converts screen coordinates to world coordinates, since the mousePosition 
        /// is in screen coordinates and must be converted to world coordinates before every request 
        /// to the server to move toward that point.
        /// </summary>
        /// <param name="screen_x">  the screen x coordinate </param>
        /// <param name="screen_y"> the screen y coordinate </param>
        /// <param name="world_x"> the converted world x coordinate </param>
        /// <param name="world_y"> the converted world y coordinate </param>
        private void ConvertFromScreenToWorld(in float screen_x, in float screen_y,
                                            out int world_x, out int world_y)
        {
            int leftBoundary = (int)(drawable.client.thisPlayer.X - (drawable.client.currViewPortalWidth / 2));
            int topBoundary = (int)(drawable.client.thisPlayer.Y - (drawable.client.currViewPortalWidth / 2));

            world_x = (int)((screen_x / 800 * drawable.client.currViewPortalWidth) + leftBoundary);
            world_y = (int)((screen_y / 800 * drawable.client.currViewPortalWidth) + topBoundary);
        }

        /// <summary>
        /// This method allows a player to split when they hit the space bar.
        /// </summary>
        /// <param name="sender"> the sender </param>
        /// <param name="e"> the event args </param>
        private void OnEntryTextChanged(object sender, EventArgs e)
        {
            Entry space = (Entry) sender;
            if (space.Text == " ")
            {
                float xPos = (float)mousePosition.X;
                float yPos = (float)mousePosition.Y;
                ConvertFromScreenToWorld(xPos, yPos, out int worldX, out int worldY);

                drawable.client.world._logger.LogDebug(" this player splitting ");
                String splitMessage = String.Format(Protocols.CMD_Split, worldX, worldY);
                channel.Send(splitMessage);
            }

            Dispatcher.Dispatch(() => { space.Text = ""; });
        }

        /// <summary>
        /// This method ensures that the world is redrawn 30 frames per second and that 
        /// the player is constantly asking to move toward their mouse position.
        /// </summary>
        /// <param name="source"> the source </param>
        /// <param name="e"> the event args </param>
        private void TickEvent(Object source, ElapsedEventArgs e)
        {
            // redraw the world
            PlaySurface.Invalidate();

            if (drawable.client.thisPlayer != null)
            {
                try
                {
                    // update the stats display
                    Dispatcher.Dispatch(() => { FoodLabel.Text = "Food: " + drawable.client.world.foods.Count; });
                    Dispatcher.Dispatch(() => { PositionLabel.Text = "Position: " + (int) drawable.client.thisPlayer.X + ", " + (int) drawable.client.thisPlayer.Y; });
                    Dispatcher.Dispatch(() => { MassLabel.Text = "Mass: " + drawable.client.thisPlayer.Mass; });
                } catch
                {

                }
            }

            if (drawable.client.thisPlayer != null && (drawable.client.thisPlayer.X != mousePosition.X || drawable.client.thisPlayer.Y != mousePosition.Y))
            {
                float xPos = (float)mousePosition.X;
                float yPos = (float)mousePosition.Y;
                ConvertFromScreenToWorld(xPos, yPos, out int worldX, out int worldY);

                String moveMessage = String.Format(Protocols.CMD_Move, worldX, worldY);
                channel.Send(moveMessage);
            }
        }

        /// <summary>
        /// This is the connection delegate to be passed into the Networking object, which 
        /// ensures that when a client successfully connects to the server, they ask to join 
        /// the game.
        /// </summary>
        /// <param name="networking"> the networking object that connected </param>
        private void OnConnect(Networking networking)
        {
            String message = String.Format(Protocols.CMD_Start_Game, NameEntry.Text);
            channel.Send(message);
        }

        /// <summary>
        /// This is the disconnection delegate to be passed into the Networking object, which 
        /// simply provides helpful logging information.
        /// </summary>
        /// <param name="networking"> the networking object that disconnected </param>
        private void OnDisconnect(Networking networking)
        {
            drawable.client.world._logger.LogDebug(" player successfully disconnected ");
        }

        /// <summary>
        /// This is the message received delegate to be passed into the Networking object, which 
        /// checks for each of the specific commands from the server and handles them accordingly.
        /// 
        /// If receives the Command Food command: adds the new food to the food list and display
        /// 
        /// If receives the Command Eaten Food command: removes the eaten food from the foods list and display
        /// 
        /// If receives the Command Player Object command: link the client id to the given player id 
        /// and set thisPlayer accordingly
        /// 
        /// If receives the Command Players command: adds the new food to the food list and display
        /// 
        /// If receives the Command Dead Players command: removes the dead players from the players list and display
        /// 
        /// </summary>
        /// <param name="networking"> the networking object that sent the message </param>
        /// <param name="message"> the message </param>
        private async void OnMessageReceived(Networking networking, string message)
        {
            // example of command: {Command Food}
            if (message.Contains(Protocols.CMD_Food))
            {
                string foodList = message.Substring(14);

                HashSet<Food> foods = JsonSerializer.Deserialize<HashSet<Food>>(foodList);

                foreach (Food food in foods)
                {
                    lock (drawable.client.world.foods)
                    {
                        if (!drawable.client.world.foods.ContainsValue(food))
                        {
                            drawable.client.world.foods.Add(food.ID, food);
                            drawable.client.world._logger.LogTrace(" added food ");
                        }
                    }  
                }
            }

            // example of command: {Command Player Object}5
            // lets the client know the game has started
            else if (message.Contains(Protocols.CMD_Player_Object))
            {
                stopWatch.Start();

                string id = message.Substring(23);
                int.TryParse(id, out int result);
                long longID = (long)result;

                // link the client id to the player id so the server knows which player to update when it gets
                // move requests from this client
                channel.ID = id;

                drawable.client.world.players.TryGetValue(longID, out Player player);
                drawable.client.thisPlayer = player;
                drawable.client.world._logger.LogDebug(" this player has been set ");
            }


            // example of command: {Command Players}
            else if (message.Contains(Protocols.CMD_Update_Players))
            {
                string playersList = message.Substring(17);
                HashSet<Player> players = JsonSerializer.Deserialize<HashSet<Player>>(playersList);

                foreach (Player player in players)
                {
                    lock (drawable.client.world.players)
                    {
                        if (!drawable.client.world.players.ContainsKey(player.ID))
                        {
                            drawable.client.world.players.Add(player.ID, player);
                            drawable.client.world._logger.LogTrace(" added new player ");
                        }

                        else
                        {
                            // if the new player, is "this player" set thisPlayer accordingly
                            if (drawable.client.thisPlayer != null && player.ID == drawable.client.thisPlayer.ID)
                            {
                                drawable.client.thisPlayer = player;
                            }

                            drawable.client.world.players.Remove(player.ID);
                            drawable.client.world.players.Add(player.ID, player);
                            drawable.client.world._logger.LogTrace(" updated player ");
                        }
                    }
                }
            }


            // example of command: {Command Dead Players}[5,10,20,30,16,121,...]
            else if (message.Contains(Protocols.CMD_Dead_Players))
            {
                // parse the string of ids into a list of separate ids
                string deadPlayerIDs = message.Substring(22);
                deadPlayerIDs = deadPlayerIDs.Replace("[", String.Empty);
                deadPlayerIDs = deadPlayerIDs.Replace("]", String.Empty);
                string[] idStrings = deadPlayerIDs.Split(',');

                List<int> IDs = new List<int>();
                foreach (string idString in idStrings)
                {
                    if (int.TryParse(idString, out int ID))
                    {
                        IDs.Add(ID);
                    }
                }

                foreach (int id in IDs)
                {
                    bool wantsToPlayAgain = false;
                    bool thisPlayerDead = false;

                    lock (drawable.client.world.players)
                    {
                        if (drawable.client.world.players.Keys.Contains(id))
                        {
                            drawable.client.world.players.Remove(id);
                            drawable.client.world._logger.LogTrace(" player died ");
                        }
                    }

                    // if "this player" has died, present their stats and ask if they want to play again
                    if (id == drawable.client.thisPlayer.ID)
                    {
                        drawable.client.world._logger.LogDebug(" this player has died ");
                        stopWatch.Stop();
                        TimeSpan ts = stopWatch.Elapsed;
                        thisPlayerDead = true;
                        wantsToPlayAgain = await DisplayAlert("You died!", "Your final mass was " + drawable.client.thisPlayer.Mass + ",\nand you managed to stay alive for " + ts.Minutes + " minutes!\nDo you want to play again?", "Yes", "No");
                    }

                    if (wantsToPlayAgain)
                    {
                        drawable.client.world._logger.LogDebug(" this player is restarting ");
                        String command = String.Format(Protocols.CMD_Start_Game, NameEntry.Text);
                        channel.Send(command);
                    }

                    if (thisPlayerDead && !wantsToPlayAgain)
                    {
                        channel.Disconnect();
                        drawable.client.world._logger.LogDebug(" disconnecting this player because they don't want to play again ");
                        await DisplayAlert("Thanks for playing!", "Hit the X to leave the game.", "OK");
                    }
                }
            }


            // example of command: {Command Eaten Food}[2701,2546,515,1484,2221,240,1378,1124,1906,1949]
            else if (message.Contains(Protocols.CMD_Eaten_Food))
            {
                // parse the string of ids into a list of separate ids
                string eatenFood = message.Substring(20);
                eatenFood = eatenFood.Replace("[", String.Empty);
                eatenFood = eatenFood.Replace("]", String.Empty);
                string[] eatenFoods = eatenFood.Split(',');

                List<int> IDs = new List<int>();
                foreach (string food in eatenFoods)
                {
                    if (int.TryParse(food, out int ID))
                    {
                        IDs.Add(ID);
                    }
                }

                foreach (int id in IDs)
                {
                    lock (drawable.client.world.foods.Keys)
                    {
                        if (drawable.client.world.foods.Keys.Contains(id))
                        {
                            drawable.client.world.foods.Remove(id);
                            drawable.client.world._logger.LogTrace(" food eaten ");
                        }
                    }
                }
            }
        }
    }
}