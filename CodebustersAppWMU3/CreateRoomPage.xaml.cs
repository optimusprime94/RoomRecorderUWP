﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CodebustersAppWMU3.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CodebustersAppWMU3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateRoomPage : Page
    {
        public CreateRoomPage()
        {
            this.InitializeComponent();
            GetRoomLocation();

        }

        public async void GetRoomLocation()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();


            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 50 };

                    // Subscribe to the PositionChanged event to get location updates.
                    geolocator.PositionChanged += OnPositionChanged;

                    geolocator.PositionChanged += OnPositionChanged;
                    geolocator.DesiredAccuracyInMeters = 1;
                    var position = await geolocator.GetGeopositionAsync();
                    var myposition = position.Coordinate.Point;

                    LatiValue.Text = myposition.Position.Latitude.ToString();
                    LongtValue.Text = myposition.Position.Longitude.ToString();

                    break;

                case GeolocationAccessStatus.Denied:
                    Frame.Navigate(typeof(MainPage));
                    break;

                case GeolocationAccessStatus.Unspecified:
                    DisplayErrorDialog("Some kind of error occured, please try again!");
                    Frame.Navigate(typeof(MainPage));
                    break;
            }

        }

        private async void OnPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var myposition = args.Position.Coordinate.Point;

                LatiValue.Text = myposition.Position.Latitude.ToString();
                LongtValue.Text = myposition.Position.Longitude.ToString();
            });

        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {

            double lat = double.Parse(LatiValue.Text);
            double longt = double.Parse(LongtValue.Text);

            //// Check if title already exists!
            //if (Title.Text == "" || Description.Text == "")
            //{
            //    DisplayErrorDialog("Please, give me something bro!");
            //    return;
            //}

            if (!IsTitleAllowed(Title.Text))
            {
                DisplayErrorDialog("Please, check yo title again!");
                return;
            }

            if (Description.Text == "")
            {
                DisplayErrorDialog("Please, check yo description again!");
                return;
            }

            var newRoom = new Room()
            {
                Title = Title.Text,
                Description = Description.Text,
                Longt = double.Parse(LongtValue.Text),
                Lat = double.Parse(LatiValue.Text)
            };

            Frame.Navigate(typeof(CreateSurfacesPage), newRoom);
        }

        //private static bool IsTextAllowed(string text)
        //{
        //    Regex regex = new Regex(@"^[a-zA-Z0-9\s]{1,}$"); //letters, whitespace and more than 0 chars
        //    return regex.IsMatch(text);
        //}
        private static bool IsTitleAllowed(string text)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9]{1,}$"); //letters, whitespace and more than 0 chars
            return regex.IsMatch(text);
        }

        private static async void DisplayErrorDialog(string message)
        {
            var dialog = new Windows.UI.Popups.MessageDialog(message);
            await dialog.ShowAsync();

        }
    }
}
