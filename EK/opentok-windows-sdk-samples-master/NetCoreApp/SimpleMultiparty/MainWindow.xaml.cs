using Newtonsoft.Json.Linq;
using OpenTok;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Windows;
using WpfApplication1;

namespace SimpleMultiparty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string API_KEY = "46774454";
        public string SESSION_ID = "2_MX40Njc3NDQ1NH5-MTU5MTI0ODg5OTE1NH5XNkhUVWM5bEhGQVFqeEMwZWM5djhjeit-fg";
        public string TOKEN = "T1==cGFydG5lcl9pZD00Njc3NDQ1NCZzaWc9NWI0NzUwZDM5MjRjZTFkYzkzZTgwOGVjZDc4NGM0YjY5Zjg5NDg5NzpzZXNzaW9uX2lkPTJfTVg0ME5qYzNORFExTkg1LU1UVTVNVEkwT0RnNU9URTFOSDVYTmtoVVZXTTViRWhHUVZGcWVFTXdaV001ZGpoamVpdC1mZyZjcmVhdGVfdGltZT0xNTkxMjQ5NjA0JnJvbGU9cHVibGlzaGVyJm5vbmNlPTE1OTEyNDk2MDQuMDc1NjEwMTcwNDAxOTU=";


        VideoCapturer Capturer;
        Session Session;
        Publisher Publisher;
        bool Disconnect = false;
        Dictionary<Stream, Subscriber> SubscriberByStream = new Dictionary<Stream, Subscriber>();


        public string ROOM_NAME = "test";

        public MainWindow()
        {
            InitializeComponent();


            var dialog = new MyDialog();
            if (dialog.ShowDialog() == true)
            {
                ROOM_NAME = dialog.ResponseText;
            }

            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync("https://learnie.herokuapp.com/room/" + ROOM_NAME).GetAwaiter().GetResult();
                dynamic result = JObject.Parse(response);
                API_KEY = Convert.ToString(result.apiKey);
                SESSION_ID = Convert.ToString(result.sessionId);
                TOKEN = Convert.ToString(result.token);
            }


            // This shows how to enumarate the available capturer devices on the system to allow the user of the app
            // to select the desired camera. If a capturer is not provided in the publisher constructor the first available 
            // camera will be used.
            var devices = VideoCapturer.EnumerateDevices();
            if (devices.Count > 0)
            {
                var selectedDevice = devices[0];
                Trace.WriteLine("Using camera: " + devices[0].Name);
                Capturer = selectedDevice.CreateVideoCapturer(VideoCapturer.Resolution.High);
            }
            else
            {
                Trace.WriteLine("Warning: no cameras available, the publisher will be audio only.");
            }

            // We create the publisher here to show the preview when application starts
            // Please note that the PublisherVideo component is added in the xaml file
            Publisher = new Publisher.Builder(Context.Instance)
            {
                Renderer = PublisherVideo,
                Capturer = Capturer
            }.Build();

            if (API_KEY == "" || SESSION_ID == "" || TOKEN == "")
            {
                MessageBox.Show("Please fill out the API_KEY, SESSION_ID and TOKEN variables in the source code " +
                    "in order to connect to the session", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ConnectDisconnectButton.IsEnabled = false;
            }
            else
            {
                Session = new Session.Builder(Context.Instance, API_KEY, SESSION_ID).Build();

                Session.Connected += Session_Connected;
                Session.Disconnected += Session_Disconnected;
                Session.Error += Session_Error;
                Session.StreamReceived += Session_StreamReceived;
                Session.StreamDropped += Session_StreamDropped;
            }

            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var subscriber in SubscriberByStream.Values)
            {
                subscriber.Dispose();
            }
            Publisher?.Dispose();
            Capturer?.Dispose();
            Session?.Dispose();
        }

        private void Session_Connected(object sender, EventArgs e)
        {
            try
            {
                Session.Publish(Publisher);
            }
            catch (OpenTokException ex)
            {
                Trace.WriteLine("OpenTokException " + ex.ToString());
            }
        }

        private void Session_Disconnected(object sender, EventArgs e)
        {
            Trace.WriteLine("Session disconnected");
            SubscriberByStream.Clear();
            SubscriberGrid.Children.Clear();
        }

        private void Session_Error(object sender, Session.ErrorEventArgs e)
        {
            Trace.WriteLine("Session error:" + e.ErrorCode);
            MessageBox.Show("Session error:" + e.ErrorCode, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UpdateGridSize(int numberOfSubscribers)
        {
            int rows = Convert.ToInt32(Math.Round(Math.Sqrt(numberOfSubscribers)));
            int cols = rows == 0 ? 0 : Convert.ToInt32(Math.Ceiling(((double)numberOfSubscribers) / rows));
            SubscriberGrid.Columns = cols;
            SubscriberGrid.Rows = rows;
        }

        private void Session_StreamReceived(object sender, Session.StreamEventArgs e)
        {
            Trace.WriteLine("Session stream received");

            VideoRenderer renderer = new VideoRenderer();

            SubscriberGrid.Children.Add(renderer);
            UpdateGridSize(SubscriberGrid.Children.Count);
            Subscriber subscriber = new Subscriber.Builder(Context.Instance, e.Stream)
            {
                Renderer = renderer
            }.Build();
            SubscriberByStream.Add(e.Stream, subscriber);

            try
            {
                Session.Subscribe(subscriber);
            }
            catch (OpenTokException ex)
            {
                Trace.WriteLine("OpenTokException " + ex.ToString());
            }
        }

        private void Session_StreamDropped(object sender, Session.StreamEventArgs e)
        {
            Trace.WriteLine("Session stream dropped");
            var subscriber = SubscriberByStream[e.Stream];
            if (subscriber != null)
            {
                SubscriberByStream.Remove(e.Stream);
                try
                {
                    Session.Unsubscribe(subscriber);
                }
                catch (OpenTokException ex)
                {
                    Trace.WriteLine("OpenTokException " + ex.ToString());
                }

                SubscriberGrid.Children.Remove((UIElement)subscriber.VideoRenderer);
                UpdateGridSize(SubscriberGrid.Children.Count);
            }
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (Disconnect)
            {
                Trace.WriteLine("Disconnecting session");
                try
                {
                    Session.Unpublish(Publisher);
                    Session.Disconnect();
                }
                catch (OpenTokException ex)
                {
                    Trace.WriteLine("OpenTokException " + ex.ToString());
                }
            }
            else
            {
                Trace.WriteLine("Connecting session");
                try
                {
                    Session.Connect(TOKEN);
                }
                catch (OpenTokException ex)
                {
                    Trace.WriteLine("OpenTokException " + ex.ToString());
                }
            }
            Disconnect = !Disconnect;
            ConnectDisconnectButton.Content = Disconnect ? "Disconnect" : "Connect";
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Session.Connected -= Session_Connected;
            Session.Disconnected -= Session_Disconnected;
            Session.Error -= Session_Error;
            Session.StreamReceived -= Session_StreamReceived;

            Capturer.Dispose();
            Publisher.Dispose();
            Session.Dispose();
        }
    }
}
