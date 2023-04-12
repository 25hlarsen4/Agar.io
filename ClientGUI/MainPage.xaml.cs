using AgarioModels;
using Communications;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json;
using System.Diagnostics;
using System.Timers;

namespace ClientGUI
{
    public partial class MainPage : ContentPage
    {
        Drawable drawable;
        Networking channel;

        Point mousePosition;
        //DateTime startTime;

        //Player thisPlayer;
        //int currViewPortalWidth = 800;
        //long thisPlayersID;

        public MainPage()
        {
            InitializeComponent();

            InitializationLogic();

            // the Drawable stores the Client which stores the world
            //drawable = new Drawable();

            //PlaySurface.Drawable = drawable;

            //channel = new Networking(NullLogger.Instance, OnConnect, OnDisconnect, OnMessageReceived, '\n');




            //client.Connect("localhost", 11000);
            //client.ClientAwaitMessagesAsync();

            //System.Timers.Timer timer = new System.Timers.Timer(33);
            //timer.Elapsed += TickEvent;
            //timer.Start();
        }

        private void InitializationLogic()
        {
            drawable = new Drawable();

            PlaySurface.Drawable = drawable;

            channel = new Networking(NullLogger.Instance, OnConnect, OnDisconnect, OnMessageReceived, '\n');
        }

        private async void OnNameEntryCompleted(object sender, EventArgs e)
        {
            WelcomeScreen.IsVisible = false;
            GameScreen.IsVisible = true;
            SpaceBarEntry.Focus();

            try
            {
                channel.Connect(ServerEntry.Text, 11000);
                Debug.WriteLine("connected");
                channel.ClientAwaitMessagesAsync();
                Debug.WriteLine("awaiting messages");

                System.Timers.Timer timer = new System.Timers.Timer(33);
                timer.Elapsed += TickEvent;
                timer.Start();
                //startTime = DateTime.Now;
            } catch
            {
                await DisplayAlert("Alert", "Unable to connect :(", "OK");
            }
        }

        private void OnServerEntryCompleted(object sender, EventArgs e)
        {

        }

        private void PointerChanged(object sender, PointerEventArgs e)
        {
            //if ((DateTime.Now - startTime).TotalMilliseconds > 300 ) 
            //{
            //    // Position inside window
            //    Point windowPosition = (Point)e.GetPosition(null);
            //    float xPos = (float)windowPosition.X;
            //    float yPos = (float)windowPosition.Y;
            //    ConvertFromScreenToWorld(xPos, yPos, out int worldX, out int worldY);

            //    //// Position relative to the container view
            //    //Point? relativeToContainerPosition = e.GetPosition((View)sender);

            //    // send move command with this point?
            //    String moveMessage = String.Format(Protocols.CMD_Move, worldX, worldY);
            //    channel.Send(moveMessage);

            //    startTime = DateTime.Now;

            //    mousePosition = windowPosition;
            //}  

            // Position inside window
            Point windowPosition = (Point)e.GetPosition(null);
            //float xPos = (float)windowPosition.X;
            //float yPos = (float)windowPosition.Y;
            //ConvertFromScreenToWorld(xPos, yPos, out int worldX, out int worldY);

            //// Position relative to the container view
            //Point? relativeToContainerPosition = e.GetPosition((View)sender);

            // send move command with this point?
            //String moveMessage = String.Format(Protocols.CMD_Move, worldX, worldY);
            //channel.Send(moveMessage);

            //startTime = DateTime.Now;

            mousePosition = windowPosition;
        }

        private void ConvertFromScreenToWorld(in float screen_x, in float screen_y,
                                            out int world_x, out int world_y)
        {
            int leftBoundary = (int)(drawable.client.thisPlayer.X - (drawable.client.currViewPortalWidth / 2));

            int rightBoundary = (int)(drawable.client.thisPlayer.X + (drawable.client.currViewPortalWidth / 2));

            int bottomBoundary = (int)(drawable.client.thisPlayer.Y + (drawable.client.currViewPortalWidth / 2));

            int topBoundary = (int)(drawable.client.thisPlayer.Y - (drawable.client.currViewPortalWidth / 2));

            // if the object is out of bounds???

            world_x = (int)((screen_x / 800 * drawable.client.currViewPortalWidth) + leftBoundary);
            world_y = (int)((screen_y / 800 * drawable.client.currViewPortalWidth) + topBoundary);


            // screenx = ((worldx-leftbound) / viewPort) * 800

            // worldx = (screenx / 800 * viewPort) + leftbound



            //world_x = (int)(screen_x / 800 * drawable.client.world.width);
            //world_y = (int)(screen_y / 800 * drawable.client.world.height);
        }

        private void OnTap(object sender, EventArgs e)
        {

        }

        private void PanUpdated(object sender, EventArgs e)
        {

        }

        private void OnEntryTextChanged(object sender, EventArgs e)
        {
            Entry space = (Entry) sender;
            if (space.Text == " ")
            {
                float xPos = (float)mousePosition.X;
                float yPos = (float)mousePosition.Y;
                ConvertFromScreenToWorld(xPos, yPos, out int worldX, out int worldY);

                String splitMessage = String.Format(Protocols.CMD_Split, worldX, worldY);
                channel.Send(splitMessage);
            }

            Dispatcher.Dispatch(() => { space.Text = ""; });
        }

        private void TickEvent(Object source, ElapsedEventArgs e)
        {
            // redraw the world
            //Debug.WriteLine("redrawing");
            PlaySurface.Invalidate();

            if (drawable.client.world.foods != null && drawable.client.thisPlayer != null)
            {
                try
                {
                    Dispatcher.Dispatch(() => { FoodLabel.Text = "Food: " + drawable.client.world.foods.Count; });
                    Dispatcher.Dispatch(() => { PositionLabel.Text = "Position: " + drawable.client.thisPlayer.X + ", " + drawable.client.thisPlayer.Y; });
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

        private void OnConnect(Networking networking)
        {
            ////networking.Send("{Command Player Object}");
            //// or ???????????
            ////client.Send("{Command Player Object}");

            //// ask to start the game
            String message = String.Format(Protocols.CMD_Start_Game, NameEntry.Text);
            //networking.Send(message);

            //// or ?????
            channel.Send(message);
        }

        private void OnDisconnect(Networking networking)
        {

        }

        private async void OnMessageReceived(Networking networking, string message)
        {
            // {Command Food}
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
                            //food.position.X = food.X;
                            //food.position.Y = food.Y;
                        }
                    }  
                }
            }

            // {Command Player Object}5, lets the client know the game has started
            else if (message.Contains(Protocols.CMD_Player_Object))
            {
                string id = message.Substring(23);
                int.TryParse(id, out int result);
                long longID = (long)result;

                // link the client id to the player id so the server knows which player to update when it gets
                // move requests from this client
                channel.ID = id;

                drawable.client.thisPlayersID = longID;
                Debug.WriteLine("this player's id: " + drawable.client.thisPlayersID);

                drawable.client.world.players.TryGetValue(longID, out Player player);
                drawable.client.thisPlayer = player;
                Debug.WriteLine("thisPlayer has been set, id is: " + drawable.client.thisPlayer.ID);
            }


            // {Command Players}
            else if (message.Contains(Protocols.CMD_Update_Players))
            {
                //Debug.WriteLine("players updated");
                string playersList = message.Substring(17);
                //Debug.WriteLine(playersList);
                HashSet<Player> players = JsonSerializer.Deserialize<HashSet<Player>>(playersList);

                foreach (Player player in players)
                {
                    lock (drawable.client.world.players)
                    {
                        if (!drawable.client.world.players.ContainsKey(player.ID))
                        {
                            Debug.WriteLine(player.ID);
                            drawable.client.world.players.Add(player.ID, player);
                            Debug.WriteLine("player x: " + player.X + ", player y: " + player.Y);
                            //Debug.WriteLine("successfully added");
                            //player.position.X = player.X;
                            //player.position.Y = player.Y;
                        }

                        else
                        {
                            if (drawable.client.thisPlayer != null && player.ID == drawable.client.thisPlayer.ID)
                            {
                                drawable.client.thisPlayer = player;
                            }
                            // does it update on its own??
                            drawable.client.world.players.Remove(player.ID);
                            drawable.client.world.players.Add(player.ID, player);
                            //player.position.X = player.X;
                            //player.position.Y = player.Y;
                        }
                    }
                }
            }


            // {Command Dead Players}[5,10,20,30,16,121,...]
            else if (message.Contains(Protocols.CMD_Dead_Players))
            {
                // this will be [5,10,20,30,16,121,...]
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

                    if (id == drawable.client.thisPlayer.ID)
                    {
                        thisPlayerDead = true;
                        wantsToPlayAgain = await DisplayAlert("You died!", "Do you want to play again?", "Yes", "No");
                    }

                    lock (drawable.client.world.players)
                    {
                        if (drawable.client.world.players.Keys.Contains(id))
                        {
                            drawable.client.world.players.Remove(id);
                        }
                    }

                    if (wantsToPlayAgain)
                    {
                        //GameScreen.IsVisible = false;
                        //WelcomeScreen.IsVisible = true;
                        channel.Disconnect();
                        Debug.WriteLine("Disconnected");
                        InitializationLogic();
                        try
                        {
                            channel.Connect(ServerEntry.Text, 11000);
                            channel.ClientAwaitMessagesAsync();
                        } catch
                        {
                            await DisplayAlert("Alert", "Unable to connect :(", "OK");
                        }
;                    }

                    if (thisPlayerDead && !wantsToPlayAgain)
                    {
                        channel.Disconnect();
                        // what happens now?
                    }
                }
            }


            // {Command Eaten Food}[2701,2546,515,1484,2221,240,1378,1124,1906,1949]
            else if (message.Contains(Protocols.CMD_Eaten_Food))
            {
                // this will be [2701,2546,515,1484,2221,240,1378,1124,1906,1949]
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
                        }
                    }
                }
            }
        }
    }
}