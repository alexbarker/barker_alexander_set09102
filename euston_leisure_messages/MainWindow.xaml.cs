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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MessageHolder.readMessages();
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

        private void View_SMS_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void View_Email_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void View_SIR_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public void View_Tweet_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Trending_List_Button_Click(object sender, RoutedEventArgs e)
        {
            viewList.Items.Clear();
            viewList.Items.Add("Trending List\n");
            int x = 1;
            var values = from pair in MessageHolder.mentions orderby pair.Value.Count descending select pair.Key.ToString();
            foreach (string v in values)
            {
                if (v.StartsWith("#")) //dont want to also add "mentions" (@ symbol) 
                {
                    viewList.Items.Add("Rank: " + x + " - " + v);
                    //viewList.Text = v;
                    //viewList.Text = "\n";
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
                //if (MessageHolder.mentions[h].Contains("@"))
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
            int x = 1;
            int y = 1;

            foreach (string p in MessageHolder.SIRcodes.Values)
            {
                //if (MessageHolder.mentions[h].Contains("@"))
                //if (p.Contains(" "))
                // {
                
                viewList.Items.Add(x + " - Centre Code: " + p);
                x++;
                // }
            }

            viewList.Items.Add("------------------------------------");

            foreach (string a in MessageHolder.SIRincidents.Values)
            {
                //if (MessageHolder.mentions[h].Contains("@"))
                //if (p.Contains(" "))
                // {
                viewList.Items.Add(y + " - Incident: " + a);
                y++;
                // }
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

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sanitize_Button_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
