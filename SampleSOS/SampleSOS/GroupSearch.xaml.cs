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

namespace SampleSOS{
    public partial class GroupSearch : PhoneApplicationPage
    {
        WebClient productClient;
        public GroupSearch()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            String city="";
            if (tcity.Text == string.Empty)
            {

                MessageBoxResult m = MessageBox.Show("", "please enter City", MessageBoxButton.OK);
                tcity.Focus();
                return;

            }
              else
            
           
            {
                city=tcity.Text;
          
                productClient = new WebClient();
                productClient.OpenReadCompleted +=
                new OpenReadCompletedEventHandler(productClient_OpenReadCompleted);
                GetGroup(city);


            }
            



        }
        private void GetGroup(string city)
        {
            string requestUri = "http://localhost:16160/jsonGroupSearchRequest.cshtml?city='" + city +"'";
            Uri uri = new Uri(requestUri);
            productClient.OpenReadAsync(uri);
        }
        void productClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    MessageBoxResult m = MessageBox.Show("", "an Error Occurred", MessageBoxButton.OK);
                    tcity.Focus();
                    return;
                }
                var serializer = new DataContractJsonSerializer(typeof(Group_detail));
                var s = (Group_detail)serializer.ReadObject(e.Result);
                string name = s.groupname;
                int length = name.Length;
                if (length <= 2)
                {
                    MessageBoxResult m = MessageBox.Show("", "No Group match your search", MessageBoxButton.OK);
                    tcity.Text = "";


                    tcity.Focus();
                    return;

                }
                else
                {
                    NavigationService.Navigate(new Uri("/ShowGroupSearch.xaml?name=" + name, UriKind.Relative));
                }

            }
            catch (Exception e1)
            {
                MessageBoxResult m = MessageBox.Show("", ""+e1, MessageBoxButton.OK);
                
            }
            
       }
}
    public class Group_detail
    {
        public string groupname { get; set; }
    
    }
}
        
