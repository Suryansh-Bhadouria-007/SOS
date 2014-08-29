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
    public partial class pin_search : PhoneApplicationPage
    {
        WebClient productClient;
        public pin_search()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            string pin = "";
            if (tpin.Text != string.Empty)
            {
                pin = tpin.Text;
                productClient = new WebClient();
                productClient.OpenReadCompleted +=
                new OpenReadCompletedEventHandler(productClient_OpenReadCompleted);
                GetHosp(pin);
            }
            else
            {
                MessageBoxResult m = MessageBox.Show("", "please enter pin", MessageBoxButton.OK);
                tpin.Focus();
                return;

            }

        }
        private void GetHosp(string pin)
        {
            string requestUri = "http://localhost:16160/jsonHospitalRequest.cshtml?pin='" + pin + "'";
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
            var serializer = new DataContractJsonSerializer(typeof(Hospital_details));
            var s = (Hospital_details)serializer.ReadObject(e.Result);
            string name = s.name;
            string addline1 = s.addline1;
            string addline2 = s.addline2;
            string phone = s.phone;
            int length = name.Length;
            int len1 = addline1.Length;
            int len2 = addline2.Length;
            //int plen = phone.Length;



            if (len1 <= 2 || len2 <= 2 || length <= 2)
            {
                MessageBoxResult m = MessageBox.Show("", "No hospital match your search criteria", MessageBoxButton.OK);
                tpin.Focus();
                return;

            }
            else
            {
                NavigationService.Navigate(new Uri("/hospital_diplay.xaml?name=" + name + "&addline1=" + addline1 + "&addline2=" + addline2 + "&phone=" + phone, UriKind.Relative));
            }


        }
    }
        public class Hospital_details
        {
            public string name { get; set; }
            public string addline1 { get; set; }
            public string addline2 { get; set; }
            public string phone { get; set; }
        }
    }


