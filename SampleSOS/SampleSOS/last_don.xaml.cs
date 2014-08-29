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
using System.Runtime.Serialization.Json;

namespace SampleSOS
{
    public partial class last_don : PhoneApplicationPage
    {       
        WebClient productClient;
        string email="";
        public last_don()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string date = "";


            if (NavigationContext.QueryString.TryGetValue("date", out date) && NavigationContext.QueryString.TryGetValue("email", out email))
            {
                textBox1.Text = date;
                textBox1.TextAlignment = TextAlignment.Center;
            }
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
            
                
                productClient = new WebClient();
                productClient.OpenReadCompleted +=
                new OpenReadCompletedEventHandler(productClient_OpenReadCompleted);
                GetDonor(email);

        }
        private void GetDonor(string email)
        {
            string requestUri = "http://localhost:16160/jsonDateUpdate.cshtml?email=" +email+ "&date=" +textBox1.Text;
            Uri uri = new Uri(requestUri);
            productClient.OpenReadAsync(uri);
        }
        void productClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBoxResult m = MessageBox.Show("", "Error", MessageBoxButton.OK);
                textBox1.Focus();
                return;
            }
            else
            {
            textBox1.Text="Updation Successful";
            }
        }
           

    

       

        private void textBox1_MouseEnter(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }

        

    }   
  

}