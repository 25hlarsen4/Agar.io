using AgarioModels;
using Communications;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json;

namespace ClientGUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

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
        }

        private void OnNameEntryCompleted(object sender, EventArgs e)
        {
            client.Connect(ServerEntry.Text, 11000);
            client.ClientAwaitMessagesAsync();

            IDispatcherTimer timer = Dispatcher.CreateTimer();
            TimeSpan span = TimeSpan.FromSeconds(30);
            timer.Interval = span;
            timer.Tick += (s, e) => TickEvent();
            timer.Start();
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
        }

        private void OnDisconnect(Networking networking)
        {

        }

        // string message = JsonSerializer.Serialize<Person>(person);
        private void OnMessageReceived(Networking networking, string message)
        {
            string foodCommand = message.Substring(0, 14);
            if (foodCommand == "{Command Food}")
            {
                string foodList = message.Substring(14);

                HashSet<Food> foods = JsonSerializer.Deserialize<HashSet<Food>>(foodList);

                drawable.world.food = foods;
            }
        }
    }
}