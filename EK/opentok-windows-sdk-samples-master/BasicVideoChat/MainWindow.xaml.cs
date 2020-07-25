using Newtonsoft.Json.Linq;
using OpenTok;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Windows;

namespace BasicVideoChat
{

    public partial class MainWindow : Window
    {
        public string API_KEY = ConfigurationManager.AppSettings["API_KEY"];
        public string SESSION_ID = ConfigurationManager.AppSettings["SESSION_ID"];
        public string TOKEN = ConfigurationManager.AppSettings["TOKEN"];


        private readonly Session Session;
        private readonly Publisher Publisher;

        public string ROOM_NAME = "test";

        public MainWindow()
        {
            InitializeComponent();

            // Uncomment following line to get debug logging

            var dialog = new MyDialog();
            if (dialog.ShowDialog() == true)
            {
                ROOM_NAME = dialog.ResponseText;
            }

            using (var client = new HttpClient())
            {
                var response = client.GetAsync("https://learnie.herokuapp.com/room/" + ROOM_NAME).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();

                var result = JObject.Parse(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());

                API_KEY = Convert.ToString(result["apiKey"]);
                SESSION_ID = Convert.ToString(result["sessionId"]);
                TOKEN = Convert.ToString(result["token"]);
            }


            try
            {
                LogUtil.Instance.EnableLogging();

                Publisher = new Publisher.Builder(Context.Instance)
                {
                    Renderer = PublisherVideo
                }.Build();

                Session = new Session.Builder(Context.Instance, API_KEY, SESSION_ID).Build();
                Session.Connected += Session_Connected;
                Session.Disconnected += Session_Disconnected;
                Session.Error += Session_Error;
                Session.StreamReceived += Session_StreamReceived;
                Session.Connect(TOKEN);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

  
        private void Window_Closed(object sender, EventArgs e)
        {
            Session.Connected -= Session_Connected;
            Session.Disconnected -= Session_Disconnected;
            Session.Error -= Session_Error;
            Session.StreamReceived -= Session_StreamReceived;

            Publisher.Dispose();
            Session.Dispose();
        }


        private void Session_Connected(object sender, System.EventArgs e)
        {
            Session.Publish(Publisher);
        }

        private void Session_Disconnected(object sender, System.EventArgs e)
        {
            Trace.WriteLine("Session disconnected.");
        }

        private void Session_Error(object sender, Session.ErrorEventArgs e)
        {
            Trace.WriteLine("Session error:" + e.ErrorCode);
        }

        private void Session_StreamReceived(object sender, Session.StreamEventArgs e)
        {
            var subscriber = new Subscriber.Builder(Context.Instance, e.Stream)
            {
                Renderer = SubscriberVideo
            }.Build();
            Session.Subscribe(subscriber);
        }


    }
}
