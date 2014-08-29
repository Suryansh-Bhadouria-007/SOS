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
    public partial class show_groups : PhoneApplicationPage
    {
        WebClient productClient;
        string email="";
        public show_groups()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (NavigationContext.QueryString.TryGetValue("email", out email))
            {
                productClient = new WebClient();
                productClient.OpenReadCompleted +=
                new OpenReadCompletedEventHandler(productClient_OpenReadCompleted);
                GetGroups(email);
            }
        }
      private void GetGroups(string email)
        {
            string requestUri = "http://localhost:16160/jsonGroupRequest.cshtml?email='"+ email+"'";
            Uri uri = new Uri(requestUri);
            productClient.OpenReadAsync(uri);
        }
        void productClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBoxResult m = MessageBox.Show("", " Error", MessageBoxButton.OK);
                return;
            }

            var serializer = new DataContractJsonSerializer(typeof(Group_details));
            var s = (Group_details)serializer.ReadObject(e.Result);
            
            string groupname = s.groupname;
            int length = groupname.Length;
            int count = 0;
           
            if (groupname == null)
            {
                MessageBoxResult m = MessageBox.Show("", "No groups joined", MessageBoxButton.OK);
                return;
            }
            else
            {
                for (int i = 1; i < length; i++)
                {
                    if (groupname[i] == ',') count++;
                }

                String[] gp = new String[count];
                int c2 = 0;
                for (int i = 1; i < length; i++)
                {

                    while (groupname[i] != ',')
                    {
                        gp[c2] = gp[c2] + groupname[i];
                        i++;
                    }
                    c2++;
                }
           
                int loc = 200; 
                for(int i=0;i<count;i++)
                {
               
                    
                        Button newButton = new Button();
                        newButton.Height = 75;
                        newButton.Width = 400;
                        newButton.Name = "" + i;
                        newButton.Foreground = new SolidColorBrush(Colors.LightGray);
                        newButton.Content = gp[i];
                        newButton.Click += new RoutedEventHandler(newButton_Click);
                        newButton.Margin = new Thickness(12, loc, 0, 0);
                        newButton.HorizontalAlignment = HorizontalAlignment.Center;
                        newButton.VerticalAlignment = VerticalAlignment.Top;
                        loc += 75;
                        ContentPanel.Children.Add(newButton);
                   
                    

            }
        }


    }
        void newButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
          
}
    public class Group_details
    {
        public string groupname { get; set; }
    }
}