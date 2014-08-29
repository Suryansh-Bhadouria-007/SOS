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
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

using System.Windows.Navigation;



namespace SampleSOS
{
    public partial class MainPage : PhoneApplicationPage
    {
        WebClient productClient;
        String[] donor = new String[10];
        String[] phone = new String[10];
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //int num;
           // string error = "";
            string bg = "";
            string city = "";
            string pin = "";
            if (blood_group.Text == string.Empty)
            {
               // error = error + "Please enter a valid blood group.";
                MessageBoxResult m = MessageBox.Show("", "please enter blood group", MessageBoxButton.OK);
                blood_group.Focus();
                return;
                

            }
            else
           

            if (tcity.Text == string.Empty)
            {

                //error = error + "Please enter a city.";
                MessageBoxResult m = MessageBox.Show("", "please enter city", MessageBoxButton.OK);
                tcity.Focus();
                return;

            }
            else
            
            if (tpin.Text == string.Empty)
            {

                //error = error + "Please enter a valid pin.";
                MessageBoxResult m = MessageBox.Show("", "please enter pin", MessageBoxButton.OK);
                tpin.Focus();
                return;

            }
            else
            
           
            {
                bg=blood_group.Text;
                city=tcity.Text;
                pin=tpin.Text;
                productClient = new WebClient();
                productClient.OpenReadCompleted +=
                new OpenReadCompletedEventHandler(productClient_OpenReadCompleted);
                GetDonor(bg, city, pin);


            }
            



        }
        private void GetDonor(string bg, string city, string pin)
        {
            string requestUri = "http://localhost:16160/jsonRequest.cshtml?bloodgroup='" + bg + "'&city='" + city + "'&pin='" + pin + "'";
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
            var serializer = new DataContractJsonSerializer(typeof(Donor_details));
            var s = (Donor_details)serializer.ReadObject(e.Result);
            string name = s.name;
            string mobile = s.mobile;
            
            int length = name.Length;
            int len = mobile.Length;
    //        int count = 0;
    //        for (int i = 1; i < length; i++)
    //        {
    //            if (name[i] == ',') count++;
    //        }
            
    //        int c2 = 0;
    //        for (int i = 1; i < length; i++)
    //        {

    //            while (name[i] != ',')
    //            {
    //                donor[c2] = donor[c2] + name[i];
    //                i++;
    //            }
    //            c2++;
    //        }
    //        int c1 = 0;
    //        for (int i = 1; i < len; i++)
    //        {

    //            while (mobile[i] != ',')
    //            {
    //                phone[c1] = phone[c1] + mobile[i];
    //                i++;
    //            }
    //            c1++;
    //        }


            if (length <= 2 || len <= 2)
            {
                MessageBoxResult m = MessageBox.Show("", "No donor match your search", MessageBoxButton.OK);
                blood_group.Text = "";
                tcity.Text = "";
                tpin.Text = "";

                blood_group.Focus();
                return;

            }
            else
            {
                NavigationService.Navigate(new Uri("/display_donors.xaml?name=" + name + "&mobile=" + mobile, UriKind.Relative));
            }
    //            int loc = 300; 
    //            for(int i=0;i<count;i++)
    //            {
               
                    
    //                    Button newButton = new Button();
    //                    newButton.Height = 75;
    //                    newButton.Width = 400;
    //                    newButton.Name = "" + i;
    //                    newButton.Foreground = new SolidColorBrush(Colors.LightGray);
    //                    newButton.Content = donor[i]+"\t"+phone[i];
    //                    newButton.Click += new RoutedEventHandler(newButton_Click);
    //                    newButton.Margin = new Thickness(12, loc, 0, 0);
    //                    newButton.HorizontalAlignment = HorizontalAlignment.Center;
    //                    newButton.VerticalAlignment = VerticalAlignment.Top;
    //                    //newButton.BorderThickness = new Thickness(0,0,0,0);
    //                    //newButton.Margin
    //                    loc += 75;
    //                    ContentPanel.Children.Add(newButton);
                   
                    

    //        }
    //    }


    //}
    //    void newButton_Click(object sender, RoutedEventArgs e)
    //    {
           
    //        try
    //        {
    //            string name = this.Name;
    //            int i = int.Parse(name);
    //            PhoneCallTask phoneCallTask = new PhoneCallTask();

    //            phoneCallTask.PhoneNumber = phone[i];
    //            phoneCallTask.DisplayName = donor[i];

    //            phoneCallTask.Show();
    //        }
    //        catch(Exception e1)
    //        {
    //            MessageBoxResult m = MessageBox.Show("",""+e1, MessageBoxButton.OK);
               
    //            return;
    //        }

            
       }
}
    public class Donor_details
    {
        public string name { get; set; }
        public string mobile { get; set; }
    }
}
    
