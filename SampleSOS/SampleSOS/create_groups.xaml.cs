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
    public partial class create_groups : PhoneApplicationPage
    {
        WebClient productClient;
        string email = "";
        string name = "";
        string date="";
        string availability="";
        public create_groups()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {



            NavigationContext.QueryString.TryGetValue("email", out email);
            
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string groupname = "";
            string city = "";
            if (tgroupname.Text == string.Empty)
            {
                MessageBoxResult m = MessageBox.Show("", "please enter group name", MessageBoxButton.OK);
                tgroupname.Focus();
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
                {
                    groupname = tgroupname.Text;
                    city = tcity.Text;
                    productClient = new WebClient();
                    productClient.OpenReadCompleted +=
                    new OpenReadCompletedEventHandler(productClient_OpenReadCompleted);
                    CreateGroup(groupname, city);

                }
           

        }
        private void CreateGroup(string groupname,string city)
        {
            string requestUri = "http://localhost:16160/jsonCreateGroup.cshtml?groupname=" + groupname +"&city=" + city+"&email="+email;
            Uri uri = new Uri(requestUri);
            productClient.OpenReadAsync(uri);
        }
        void productClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBoxResult m = MessageBox.Show("", "Creation Failed", MessageBoxButton.OK);
                tgroupname.Focus();
                return;
            }
            else
            {
                MessageBoxResult m = MessageBox.Show("", "Group Created Successfully!!", MessageBoxButton.OK);
             
            }
        }
    }
}