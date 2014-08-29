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
    public partial class ShowGroupSearch : PhoneApplicationPage
    {
        string name = "";
        public ShowGroupSearch()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (NavigationContext.QueryString.TryGetValue("name", out name))
            {
                int length = name.Length;
                int count = 0;
                for (int i = 1; i < length; i++)
                {
                    if (name[i] == ',') count++;
                }
                String[] grp = new String[count];
    
                int c2 = 0;
                for (int i = 1; i < length; i++)
                {

                    while (name[i] != ',')
                    {
                        grp[c2] = grp[c2] + name[i];
                        i++;
                    }
                    c2++;
                }
                
                int loc = 150;
                for (int i = 0; i < count; i++)
                {


                    Button newButton = new Button();
                    newButton.Height = 75;
                    newButton.Width = 400;
                    newButton.Name = "" + i;
                    newButton.Foreground = new SolidColorBrush(Colors.LightGray);
                    newButton.Content = grp[i];
                    newButton.Click += new RoutedEventHandler(newButton_Click);
                    newButton.Margin = new Thickness(12, loc, 0, 0);
                    newButton.HorizontalAlignment = HorizontalAlignment.Center;
                    newButton.VerticalAlignment = VerticalAlignment.Top;
                    loc += 100;
                    ContentPanel.Children.Add(newButton);
                }



            }
            
        }
        void newButton_Click(object sender, RoutedEventArgs e)
        {

            //try
            //{
            //    string name = this.Name;
            //    int i = int.Parse(name);
            //    PhoneCallTask phoneCallTask = new PhoneCallTask();

            //    phoneCallTask.PhoneNumber = phone[i];
            //    phoneCallTask.DisplayName = donor[i];

            //    phoneCallTask.Show();
            //}
            //catch(Exception e1)
            //{
            //    MessageBoxResult m = MessageBox.Show("",""+e1, MessageBoxButton.OK);

            //    return;
            //}
        }

      
    }
}