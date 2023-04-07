using AgarioModels;
using Communications;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json;

namespace ClientGUI
{
    public partial class MainPage : ContentPage
    {
        //World world;
        Drawable drawable;
        Networking client;

        public MainPage()
        {
            InitializeComponent();

            //world = new World();
            drawable = new Drawable();
            PlaySurface.Drawable = drawable;
            client = new Networking(NullLogger.Instance, OnConnect, OnDisconnect, OnMessageReceived, '\n');




            client.Connect("localhost", 11000);
            client.ClientAwaitMessagesAsync();

            IDispatcherTimer timer = Dispatcher.CreateTimer();
            TimeSpan span = TimeSpan.FromSeconds(30);
            timer.Interval = span;
            timer.Tick += (s, e) => TickEvent();
            timer.Start();
        }

        //private void OnNameEntryCompleted(object sender, EventArgs e)
        //{
        //    client.Connect(ServerEntry.Text, 11000);
        //    client.ClientAwaitMessagesAsync();

        //    IDispatcherTimer timer = Dispatcher.CreateTimer();
        //    TimeSpan span = TimeSpan.FromSeconds(30);
        //    timer.Interval = span;
        //    timer.Tick += (s, e) => TickEvent();
        //    timer.Start();
        //}

        private void PointerChanged(object sender, EventArgs e)
        {

        }

        private void OnTap(object sender, EventArgs e)
        {

        }

        private void PanUpdated(object sender, EventArgs e)
        {

        }

        private void TickEvent()
        {
            // redraw the world
            PlaySurface.Invalidate();
        }

        private void OnConnect(Networking networking)
        {
            //networking.Send("{Command Player Object}");
            // or ???????????
            //client.Send("{Command Player Object}");

            // ask to start the game
            String message = String.Format(Protocols.CMD_Start_Game, "Jim");
            networking.Send(message);

            // or ?????
            // client.Send(message);
        }

        private void OnDisconnect(Networking networking)
        {

        }

        private void OnMessageReceived(Networking networking, string message)
        {
            // {Command Food}
            if (message.Contains(Protocols.CMD_Food))
            {
                string foodList = message.Substring(14);

                HashSet<Food> foods = JsonSerializer.Deserialize<HashSet<Food>>(foodList);

                foreach (Food food in foods)
                {
                    if (!drawable.world.foods.ContainsValue(food))
                    {
                        drawable.world.foods.Add(food.ID, food);
                        food.position.X = food.X;
                        food.position.Y = food.Y;
                    }
                }
            }

            // {Command Players}
            else if (message.Contains(Protocols.CMD_Update_Players))
            {
                string playersList = message.Substring(17);

                HashSet<Player> players = JsonSerializer.Deserialize<HashSet<Player>>(playersList);

                foreach (Player player in players)
                {
                    if (!drawable.world.players.ContainsValue(player))
                    {
                        drawable.world.players.Add(player.ID, player);
                        player.position.X = player.X;
                        player.position.Y = player.Y;
                    }

                    else
                    {
                        // does it update on its own??
                        player.position.X = player.X;
                        player.position.Y = player.Y;
                    }
                }
            }



            //This is new code to handle other messages but I have it commented out so we can just
            // testing displaying the food first:


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
                    if (drawable.world.players.Keys.Contains(id))
                    {
                        drawable.world.players.Remove(id);
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
                    if (drawable.world.foods.Keys.Contains(id))
                    {
                        drawable.world.foods.Remove(id);
                    }
                }
            }
        }
    }
}