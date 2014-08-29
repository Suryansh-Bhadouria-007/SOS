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
using System.IO;

namespace SampleSOS
{
    public partial class Page1 : PhoneApplicationPage
    {
        WebClient productClient;
        public Page1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //string error = "";
            string name="";
            string mobile="";
            string city="";
            string  email="";
            string password="";
            string bloodgroup="";
            string availability="";
            string pin = "";
           
            if (tname.Text == string.Empty)
            {
                MessageBoxResult m = MessageBox.Show("", "please enter name", MessageBoxButton.OK);
                tname.Focus();
                return;

            }
            else
            
            if (tmobile.Text == string.Empty)
            {
                MessageBoxResult m = MessageBox.Show("", "please enter mobile number", MessageBoxButton.OK);
                tmobile.Focus();
                return;

            }
            else
           
            if (tcity.Text == string.Empty)
            {
                MessageBoxResult m = MessageBox.Show("", "please enter city", MessageBoxButton.OK);
                tcity.Focus();
                return;

            }
            else
            
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
           
            if (tbloodgroup.Text == string.Empty)
            {
                MessageBoxResult m = MessageBox.Show("", "please enter bloodgroup", MessageBoxButton.OK);
                tbloodgroup.Focus();
                return; 

            }
            else
            
            if (tavailability.Text == string.Empty)
            {
                MessageBoxResult m = MessageBox.Show("", "please select if you are available", MessageBoxButton.OK);
                tpassword.Focus();
                return; 

            }
           
            if (tpin.Text == string.Empty)
            {

                MessageBoxResult m = MessageBox.Show("", "please enter pin", MessageBoxButton.OK);
                tpassword.Focus();
                return; 

            }
            else
            {
                name = tname.Text;
                mobile = tmobile.Text;
                city = tcity.Text;
                email = temail.Text;
                password = tpassword.Text;
                bloodgroup = tbloodgroup.Text;
                availability = tavailability.Text;
                pin = tpin.Text;
                productClient = new WebClient();
                productClient.OpenReadCompleted +=
                new OpenReadCompletedEventHandler(productClient_OpenReadCompleted);
                GetDonor(name,mobile,city,email,password,bloodgroup,availability,pin);
               
            }
        }


        private void GetDonor(string name,string mobile,string city,string email,string password,string bloodgroup,string availability,string pin)
        {
            string requestUri = "http://localhost:16160/jsonInsert.cshtml?name="+name+"&mobile="+mobile+"&city="+city+"&email="+email+"&password="+password+"&bloodgroup="+bloodgroup+"&availability="+availability+"&pin="+pin ;
            Uri uri = new Uri(requestUri);
            productClient.OpenReadAsync(uri);
        }
        void productClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBoxResult m = MessageBox.Show("", "Registration Error", MessageBoxButton.OK);
                tname.Focus();
                return;
            }
            else
            {
                MessageBoxResult m = MessageBox.Show("", "Registration Successful!!", MessageBoxButton.OK);
                NavigationService.Navigate(new Uri("/MainPage.xaml",UriKind.Relative));
            }
        }
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }
        //public class Product
        //{
        //    public string Name { get; set; }
        //    public string Price { get; set; }
        //}

    }
}