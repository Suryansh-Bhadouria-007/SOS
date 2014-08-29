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
using Microsoft.Phone.Tasks;

namespace SampleSOS
{
    public partial class hospital_diplay : PhoneApplicationPage
    {
        int plen;
        string name = "";
        string addline1 = "";
        string addline2 = "";
        string phone = "";
        String[] hosp = new String[10];
        String[] add1 = new String[10];
        String[] add2 = new String[10];
        String[] mobile = new String[10];
        int count = 0;
        public hospital_diplay()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {


            if (NavigationContext.QueryString.TryGetValue("name", out name) && NavigationContext.QueryString.TryGetValue("addline1", out addline1) && NavigationContext.QueryString.TryGetValue("addline2", out addline2) && NavigationContext.QueryString.TryGetValue("phone", out phone))
            {
                int length = name.Length;
                int len1 =addline1.Length;
                int len2 = addline2.Length;
                plen = phone.Length;
                
                for (int i = 1; i < length; i++)
                {
                    if (name[i] == ',') count++;
                }
                
                int c2 = 0;
                for (int i = 1; i < length; i++)
                {

                    while (name[i] != ',')
                    {
                        hosp[c2] = hosp[c2] + name[i];
                        i++;
                    }
                    c2++;
                }
                int c1 = 0;
                for (int i = 1; i < len1; i++)
                {

                    while (addline1[i] != '/')
                    {
                        add1[c1] = add1[c1] + addline1[i];
                        i++;
                    }
                    c1++;
                }
                int c = 0;
                for (int i = 1; i < len2; i++)
                {

                    while (addline2[i] != '/')
                    {
                        add2[c] = add2[c] + addline2[i];
                        i++;
                    }
                    c++;
                }
                int a = 0;
                for (int i = 1; i < plen; i++)
                {

                    while (phone[i] != '/')
                    {
                        mobile[a] = mobile[a] + phone[i];
                        i++;
                    }
                    c++;
                }


                int loc = 100;
                for (int i = 0; i < count; i++)
                {


                    Button newButton = new Button();
                    newButton.Height = 200;
                    newButton.Width = 400;
                    newButton.Name = "" + i;
                    newButton.Foreground = new SolidColorBrush(Colors.LightGray);
                    newButton.Content = hosp[i] + "\n" + add1[i]+"\n"+add2[i];
                    newButton.Click += new RoutedEventHandler(newButton_Click);
                    newButton.Margin = new Thickness(12, loc, 0, 0);
                    newButton.HorizontalAlignment = HorizontalAlignment.Center;
                    newButton.VerticalAlignment = VerticalAlignment.Top;
                    loc += 200;
                    ContentPanel.Children.Add(newButton);
                }



            }
        }
        void newButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string name = this.Name;
                int index = 0;
                //int i = int.Parse(name);
                for (int i = 0; i < count; i++)
                {
                    if (name == ("" + i))
                    {
                        index = i;
                    }
                }
                if (plen <= 2)
                {
                    MessageBoxResult m = MessageBox.Show("", "Hospital has inaccessible private number", MessageBoxButton.OK);
                   return;
                }
                else
                {
                    PhoneCallTask phoneCallTask = new PhoneCallTask();

                    phoneCallTask.PhoneNumber = mobile[index];
                    phoneCallTask.DisplayName = hosp[index];

                    phoneCallTask.Show();
                }
            }
            catch (Exception e1)
            {
                MessageBoxResult m = MessageBox.Show("", "" + e1, MessageBoxButton.OK);

                return;
            }
        }


        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    NavigationService.Navigate(new Uri("/Donor_search.xaml", UriKind.Relative));
        //}
    }
}
