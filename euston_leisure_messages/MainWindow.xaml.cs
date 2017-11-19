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

        private void Add_New_Button_Click(object sender, RoutedEventArgs e)
        {

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

        private void View_Tweet_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Trending_List_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Mentions_List_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SIR_List_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
