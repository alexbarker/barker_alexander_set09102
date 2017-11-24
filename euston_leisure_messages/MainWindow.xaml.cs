using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace euston_leisure_messages
{
    /// <summary>
    /// SET09102 2017-8 TR1 001 - Software Engineering
    /// Euston Leisure Message System
    /// Version 1.0.0
    /// Alexander Barker 
    /// 40333139
    /// Created on 30th October 2017
    /// Last Updated on 22nd November 2017
    /// </summary>
    /// <summary>
    /// MainWindow.xaml.cs - This class is the main window of the application. Processes all user inputs via button or drop-down lists.
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MessageHolder.ReadMessages();
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Message_Type_SelectionChanged(object sender, EventArgs e)
        {
            if (comboBox.SelectedIndex == 1)
            {
                Views.AddSMS form = new Views.AddSMS();
                form.ShowDialog();
            }
            if (comboBox.SelectedIndex == 2)
            {
                Views.AddEmail form = new Views.AddEmail();
                form.ShowDialog();
            }
            if (comboBox.SelectedIndex == 3)
            {
                Views.AddSIR form = new Views.AddSIR();
                form.ShowDialog();
            }
            if (comboBox.SelectedIndex == 4)
            {
                Views.AddTweet form = new Views.AddTweet();
                form.ShowDialog();
            }
        }

        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
             Window window = new MessageViewer();
             window.Show();          
        }

        private void Trending_List_Button_Click(object sender, RoutedEventArgs e)
        {
            viewList.Items.Clear();
            viewList.Items.Add("Trending List\n");
            int x = 1;
            var values = from pair in MessageHolder.mentions orderby pair.Value.Count descending select pair.Key.ToString();
            foreach (string v in values)
            {
                if (v.StartsWith("#"))
                {
                    viewList.Items.Add("Rank: " + x + " - " + v);
                    x++;
                }
            }
        }

        private void Mentions_List_Button_Click(object sender, RoutedEventArgs e)
        {
            viewList.Items.Clear();
            viewList.Items.Add("Mentions List\n");

            foreach (string h in MessageHolder.mentions.Keys)
            {
                if (h.StartsWith("@"))
                {
                    viewList.Items.Add(h);
                }
            }
        }

        private void SIR_List_Button_Click(object sender, RoutedEventArgs e)
        {
            viewList.Items.Clear();
            viewList.Items.Add("SIR List\n");
            int x = 0;
            int y = 0;
            int z = MessageHolder.SIRcodes.Count;
            string[] pp = new string[z];
            string[] qq = new string[z];

            foreach (string p in MessageHolder.SIRcodes.Values)
            {
                pp[x] = p;
                x++;
            }

            viewList.Items.Add("------------------------------------");

            foreach (string a in MessageHolder.SIRincidents.Values)
            {
                qq[y] = a;
                y++;
            }

            for (int i = 0; i < z; i++)
            {
                viewList.Items.Add("Centre Code: " + pp[i]);
                viewList.Items.Add("Incident: " + qq[i]);
                viewList.Items.Add("------------------------------------");
            }
        }

        private void Quarantine_List_Button_Click(object sender, RoutedEventArgs e)
        {
            viewList.Items.Clear();
            viewList.Items.Add("Quarantined List\n");

            foreach (string s in MessageHolder.quarantined.Keys)
            {
                viewList.Items.Add(s);
            }
        }
    }
}


