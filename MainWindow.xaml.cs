using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Microsoft.Toolkit.Uwp.Notifications; // Add this for notifications


namespace RadioPlayer_Basic
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private List<RadioStation> _stations = new List<RadioStation>();

        public MainWindow()
        {
            InitializeComponent();
            FetchRadioStations();
            searchBox.TextChanged += SearchBox_TextChanged;

            // Show the notification when the app starts
            ShowNotificationWithDelay();

            // Register the event handler for toast notifications
            ToastNotificationManagerCompat.OnActivated += ToastNotificationManagerCompat_OnActivated;

        }
        private async void ShowNotificationWithDelay()
        {
            // Wait for 10 seconds
            await Task.Delay(10000);
            // Then show the notification
            ShowNotification();
        }
        private void ToastNotificationManagerCompat_OnActivated(ToastNotificationActivatedEventArgsCompat args)
        {
            // Check the action and handle it
            if (args.Argument == "action=openWebsite")
            {
                // Open your website here
                Process.Start(new ProcessStartInfo("https://voiddtest1d.wpenginepowered.com/?product=radio-player-premium") { UseShellExecute = true });
            }
        }
        private async void FetchRadioStations()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://de1.api.radio-browser.info/json/stations/search?limit=10000");
                _stations = JsonConvert.DeserializeObject<List<RadioStation>>(response);
                DisplayChannels(_stations);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching radio stations: {ex.Message}");
            }
        }

        private void DisplayChannels(List<RadioStation> stations)
        {
            channelList.ItemsSource = stations;
        }

        private void ChannelButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string url)
            {
                audioPlayer.Source = new Uri(url);
                audioPlayer.Play();
                playButton.IsEnabled = false;
                pauseButton.IsEnabled = true;
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            audioPlayer.Play();
            playButton.IsEnabled = false;
            pauseButton.IsEnabled = true;
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            audioPlayer.Pause();
            playButton.IsEnabled = true;
            pauseButton.IsEnabled = false;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = searchBox.Text.ToLower();
            var filteredStations = _stations.FindAll(s => s.Name.ToLower().Contains(filter));
            DisplayChannels(filteredStations);
        }

        private void ShowNotification()
        {
            // Create the button to open your website
            var openWebsiteButton = new ToastButton("Open Website", "action=openWebsite")
                .SetBackgroundActivation(); // Optionally run in background

            // Create the toast notification content
            new ToastContentBuilder()
                .AddText("Radio App")
                .AddText("Your favorite radio is just a click away!")
                .AddButton(openWebsiteButton)
                .Show();
        }


    }

    public class RadioStation
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
