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
    public partial class display_donors : PhoneApplicationPage
    {
        string name = "";
        string mobile = "";
        String[] donor = new String[10];
        String[] phone = new String[10];
        int count = 0;
        public display_donors()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            

            if (NavigationContext.QueryString.TryGetValue("name", out name) && NavigationContext.QueryString.TryGetValue("mobile", out mobile))
            {
                        int length = name.Length;
                        int len = mobile.Length;
                        
                        for (int i = 1; i < length; i++)
                        {
                            if (name[i] == ',') count++;
                        }
                       // String []donor=new String[count];
                        //String []phone=new String[count];
                        int c2 = 0;
                        for (int i = 1; i < length; i++)
                        {

                            while (name[i] != ',')
                            {
                                donor[c2] = donor[c2] + name[i];
                                i++;
                            }
                            c2++;
                        }
                        int c1 = 0;
                        for (int i = 1; i < len; i++)
                        {

                            while (mobile[i] != ',')
                            {
                                phone[c1] = phone[c1] + mobile[i];
                                i++;
                            }
                            c1++;
                        }


                       
                            int loc = 150; 
                            for(int i=0;i<count;i++)
                            {


                                    Button newButton = new Button();
                                    newButton.Height = 75;
                                    newButton.Width = 400;
                                    newButton.Name = "" + i;
                                    newButton.Foreground = new SolidColorBrush(Colors.LightGray);
                                    newButton.Content = donor[i]+"\t"+phone[i];
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

                        try
                        {
                            string name = this.Name;
                            int index=0;
                            //int i = int.Parse(name);
                            for (int i = 0; i < count; i++)
                            { 
                            if(name==(""+i))
                            {
                                index = i;
                            }
                            }
                            PhoneCallTask phoneCallTask = new PhoneCallTask();

                            phoneCallTask.PhoneNumber = phone[index];
                            phoneCallTask.DisplayName = donor[index];

                            phoneCallTask.Show();
                        }
                        catch (Exception e1)
                        {
                            MessageBoxResult m = MessageBox.Show("", "" + e1, MessageBoxButton.OK);

                            return;
                        }
            }

                    
        }
    }
