using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Device.Location;
using System.Runtime.Serialization.Json;

namespace SampleSOS
{
    public partial class gps_search : PhoneApplicationPage
    {   WebClient productClient;
        GeoCoordinateWatcher watcher;
        public gps_search()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            BeginTracking(GeoPositionAccuracy.High);
        }
        private void BeginTracking(GeoPositionAccuracy accuracy)
        {
            watcher = new GeoCoordinateWatcher(accuracy);

            watcher.MovementThreshold = 20;

            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);

            watcher.Start();
        }
        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => PositionChanged(e));
        }

        /// <summary>
        /// Method to update UI
        /// </summary>
        /// <param name="e"></param>
        void PositionChanged(GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            latitude.Text = e.Position.Location.Latitude.ToString("0.000");
            longitude.Text = e.Position.Location.Longitude.ToString("0.000");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

            if (latitude.Text == string.Empty || longitude.Text == string.Empty)
            {
                MessageBoxResult m = MessageBox.Show("", "please press get location button ", MessageBoxButton.OK);
                button1.Focus();
                return;
                
            }
                productClient = new WebClient();
                productClient.OpenReadCompleted +=
                new OpenReadCompletedEventHandler(productClient_OpenReadCompleted);
                GetLocation();


        }
        private void GetLocation()
        {
            string requestUri = "http://localhost:16160/jsonGpsRequest.cshtml?latitude='" +latitude.Text + "'&longitude='" +longitude.Text+"'";
            Uri uri = new Uri(requestUri);
            productClient.OpenReadAsync(uri);
        }
        void productClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                textBlock2.Text = "An Error Occurred";
                return;
            }
            var serializer = new DataContractJsonSerializer(typeof(Loc_details));
            var s = (Loc_details)serializer.ReadObject(e.Result);
            string name = s.name;
            string addline1 = s.addline1;
            string addline2 = s.addline2;
            string phone = s.phone;
            int length = name.Length;
            int len1 = addline1.Length;
            int len2 = addline2.Length;
            //int count = 0;
            //for (int i = 1; i < length; i++)
            //{
            //    if (name[i] == ',') count++;
            //}
            //String[] hosp = new String[count];
            //String[] add1 = new String[count];
            //String[] add2 = new String[count];
            //int c2 = 0;
            //for (int i = 1; i < length; i++)
            //{

            //    while (name[i] != ',')
            //    {
            //        hosp[c2] = hosp[c2] + name[i];
            //        i++;
            //    }
            //    c2++;
            //}
            //int c1 = 0;
            //for (int i = 1; i < len1; i++)
            //{

            //    while (addline1[i] != '/')
            //    {
            //        add1[c1] = add1[c1] + addline1[i];
            //        i++;
            //    }
            //    c1++;
            //}
            //int c = 0;
            //for (int i = 1; i < len2; i++)
            //{

            //    while (addline2[i] != '/')
            //    {
            //        add2[c] = add2[c] + addline2[i];
            //        i++;
            //    }
            //    c++;
            //}


            if (len1<=2  || len2<=2 || length<=2)
            {
                MessageBoxResult m = MessageBox.Show("", "No hospital match search criteria", MessageBoxButton.OK);
                button1.Focus();
                return;
                
            }
            else
            {
                NavigationService.Navigate(new Uri("/hospital_diplay.xaml?name=" + name + "&addline1=" + addline1 + "&addline2=" + addline2+"&phone="+phone,UriKind.Relative));
            }
                //int loc = 200;
                //for (int i = 0; i < count; i++)
                //{

                //    Button newButton = new Button();
                //    newButton.Height = 150;
                //    newButton.Width = 400;
                //    //newButton.Name = "Button" + i;
                //    newButton.Foreground = new SolidColorBrush(Colors.LightGray);
                //    newButton.Content = hosp[i] + "\n" + add1[i] + "\n" + add2[i];
                //    newButton.Click += new RoutedEventHandler(newButton_Click);
                //    newButton.Margin = new Thickness(12, loc, 0, 0);
                //    newButton.HorizontalAlignment = HorizontalAlignment.Center;
                //    newButton.VerticalAlignment = VerticalAlignment.Top;
                //    //newButton.BorderThickness = new Thickness(0,0,0,0);
                //    //newButton.Margin
                //    loc += 150;
                //    ContentPanel.Children.Add(newButton);



                }
            


        
        //void newButton_Click(object sender, RoutedEventArgs e)
        //{
        //    //PhoneCallTask phoneCallTask = new PhoneCallTask();

        //    //phoneCallTask.PhoneNumber = phoneCallTask[;
        //    //phoneCallTask.DisplayName = "Gage";

        //    //phoneCallTask.Show();

        //}

       
    }
    public class Loc_details
    {
        public string name { get; set; }
        public string addline1 { get; set; }
        public string addline2 { get; set; }
        public string phone { get; set; }
    }
}

