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
using System.Windows.Navigation;

namespace SampleSOS
{
    public partial class Logged_In : PhoneApplicationPage
    {
        string email = "";
        string date = "";
        string availability = "";
        string name = "";
        public Logged_In()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            


            if (NavigationContext.QueryString.TryGetValue("name", out name) && NavigationContext.QueryString.TryGetValue("email", out email) && NavigationContext.QueryString.TryGetValue("date", out date)  && NavigationContext.QueryString.TryGetValue("availability", out availability))
            {
                textBlock1.Text = "Welcome,"+name;
                textBlock1.TextAlignment = TextAlignment.Center;
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/last_don.xaml?email=" + email+"&date="+date,UriKind.Relative));
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/update_availability.xaml?email=" + email + "&availability=" + availability, UriKind.Relative));
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/create_groups.xaml?email=" + email, UriKind.Relative));
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/show_groups.xaml?email=" + email, UriKind.Relative));
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult m = MessageBox.Show("", "You have been successfully logged out", MessageBoxButton.OK);
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GroupSearch.xaml?email="+email, UriKind.Relative));
        }
    }
}