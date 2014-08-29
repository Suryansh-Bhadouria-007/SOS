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
    public partial class UpdateAvailability : PhoneApplicationPage
    {
        WebClient productClient;
        
        string email = "";
        string availability="";
        public UpdateAvailability()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //string availability = "";
            //string email = "";


            if (NavigationContext.QueryString.TryGetValue("email", out email) && NavigationContext.QueryString.TryGetValue("availability", out availability))
            {
                textBlock2.Text = availability;
                textBlock2.TextAlignment = TextAlignment.Center;
            }
        }

        private void ComboBoxItem_MouseEnter(object sender, MouseEventArgs e)
        {
            productClient = new WebClient();
            productClient.OpenReadCompleted +=
            new OpenReadCompletedEventHandler(productClient_OpenReadCompleted);
            GetAvail(email,"yes");
        }

        private void ComboBoxItem_MouseEnter_1(object sender, MouseEventArgs e)
        {
            productClient = new WebClient();
            productClient.OpenReadCompleted +=
            new OpenReadCompletedEventHandler(productClient_OpenReadCompleted);
            GetAvail(email,"no");
        }

             private void GetAvail(string email,string availability)
        {
            string requestUri = "http://localhost:16160/jsonAvailUpdate.cshtml?email=" +email+ "&availability=" +availability;
            Uri uri = new Uri(requestUri);
            productClient.OpenReadAsync(uri);
        }
        void productClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBoxResult m = MessageBox.Show("", "Error", MessageBoxButton.OK);
              
                return;
            }
            else
            {
            textBlock2.Text="Updation Successful";
            }
        }
           
   
       
    }
}