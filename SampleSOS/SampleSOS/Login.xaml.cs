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
using System.Runtime.Serialization.Json;

namespace SampleSOS
{
    public partial class Login : PhoneApplicationPage
    {
        WebClient productClient;
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = "";
            string password = "";
            if (temail.Text == string.Empty)
            {
                MessageBoxResult m = MessageBox.Show("", "please enter email id", MessageBoxButton.OK);
                temail.Focus();
                return;

            }
            else

                if (tpassword.Text == string.Empty)
                {
                    MessageBoxResult m = MessageBox.Show("", "please enter password", MessageBoxButton.OK);
                    tpassword.Focus();
                    return;

                }
                else
                {
                    email = temail.Text;
                    password = tpassword.Text;
                    productClient = new WebClient();
                    productClient.OpenReadCompleted +=
                    new OpenReadCompletedEventHandler(productClient_OpenReadCompleted);
                    GetLogin(email, password);
               
                }
           

        }

        private void GetLogin(string email,string password)
        {
            string requestUri = "http://localhost:16160/jsonLoginRequest.cshtml?email='"+ email+"'";
            Uri uri = new Uri(requestUri);
            productClient.OpenReadAsync(uri);
        }
        void productClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBoxResult m = MessageBox.Show("", "Login Error", MessageBoxButton.OK);
                temail.Focus();
                return;
            }

            var serializer = new DataContractJsonSerializer(typeof(Login_details));
            var s = (Login_details)serializer.ReadObject(e.Result);
            string password = s.password;
            string name = s.name;
            string date = s.lastdondate;
            string availability = s.availability;
           
            if (name == null || password == null)
            {
                MessageBoxResult m = MessageBox.Show("", "User does not exist.Try again", MessageBoxButton.OK);
                temail.Focus();
                return;

            }
            else
            {
                if (password == tpassword.Text)
                {
                    NavigationService.Navigate(new Uri("/Logged_In.xaml?name="+name+"&email="+temail.Text+"&date="+date+"&availability="+availability, UriKind.Relative));
                }
                else 
                {
                    MessageBoxResult m = MessageBox.Show("", "Password incorrect.try again", MessageBoxButton.OK);
                    temail.Focus();
                    return;                
                }
            }


        }
        void newButton_Click(object sender, RoutedEventArgs e)
        {
          

        }
        
       

    }
    public class Login_details
    {
        public string password { get; set; }
        public string name { get; set; }
        public string lastdondate { get; set; }
        public string availability { get; set; }
        
    }
}

       