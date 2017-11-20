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
using System.Windows.Shapes;

namespace euston_leisure_messages
{
    /// <summary>
    /// Interaction logic for MessageViewer.xaml
    /// </summary>
    public partial class MessageViewer : Window
    {


        public MessageViewer()
        {
            InitializeComponent();
        }

        /// used to refresh listboxes
        /// <param name="typeIn">type of messages to show</param>
        private void refreshData(string typeIn)
        {
            if (typeIn == "all") //shows all messages in the list
            {
                messageListBox.Items.Clear(); //have to clear listbox to avoid duplicates, could check for delta in future
                foreach (Message m in MessageHolder.messages.Values)
                {
                    string messageType = m.GetType().ToString();
                    string[] splitval = messageType.Split('.');
                    MessageReader proc = m.returnData();
                    string body = proc.body;
                    messageListBox.Items.Add(createGrid(proc.header, body, splitval[splitval.Length - 1]));
                }
            }
            else //a message type has been selected so filter the messages to show only the select type
            {
                messageListBox.Items.Clear();
                foreach (Message m in MessageHolder.messages.Values)
                {
                    string messageType = m.GetType().ToString();
                    string[] splitval = messageType.Split('.');
                    MessageReader proc = m.returnData();
                    string body = proc.body;

                    //checks if the message type is the same as the selected type in combo box
                    if (splitval[splitval.Length - 1] == typeIn)
                    {
                        messageListBox.Items.Add(createGrid(proc.header, body, splitval[splitval.Length - 1]));
                    }
                }
            }
        }

        //creates a grid for adding to listbox
        /// <param name="header">message header</param>
        /// <param name="body">message body</param>
        /// <param name="messageType">message type</param>
        private Grid createGrid(string header, string body, string messageType)
        {
            Grid myGrid = new Grid();
            myGrid.Width = messageListBox.Width;
            myGrid.Height = 50;
            myGrid.HorizontalAlignment = HorizontalAlignment.Left;
            myGrid.VerticalAlignment = VerticalAlignment.Top;
            myGrid.ShowGridLines = true;

            // Define the Columns
            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            ColumnDefinition colDef3 = new ColumnDefinition();

            myGrid.ColumnDefinitions.Add(colDef1);
            myGrid.ColumnDefinitions.Add(colDef2);
            myGrid.ColumnDefinitions.Add(colDef3);

            // Define the Rows
            RowDefinition rowDef1 = new RowDefinition();
            myGrid.RowDefinitions.Add(rowDef1);

            // Add the second text cell to the Grid
            Label txt1 = new Label();
            txt1.Content = header;
            txt1.FontSize = 12;
            Grid.SetRow(txt1, 0);
            Grid.SetColumn(txt1, 0);

            // Add the third text cell to the Grid
            Label txt2 = new Label();
            txt2.Content = body;
            txt2.FontSize = 12;
            Grid.SetRow(txt2, 0);
            Grid.SetColumn(txt2, 1);

            // Add the third text cell to the Grid
            Label txt3 = new Label();
            txt3.Content = messageType;
            txt3.FontSize = 12;
            Grid.SetRow(txt3, 0);
            Grid.SetColumn(txt3, 2);

            myGrid.Children.Add(txt1);
            myGrid.Children.Add(txt2);
            myGrid.Children.Add(txt3);

            return myGrid;
        }

        //used to open a window to display the selected message 
        private void messageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                String msg = "";
                Grid message = (Grid)messageListBox.SelectedValue;
                msg += ((Label)message.Children[0]).Content;
                /*
                if (MessageHolder.messages[msg].seen == false)
                {
                    MessageHolder.messages[msg].seen = true;
                    MessageHolder.refresher.addNewSeen();
                }*/

                Window messV = new ViewMessage(msg);
                messV.Show();
            }
            catch (NullReferenceException ex)
            {
                //clears the selected item in the listbox, will be hit if the combobox value is changed and an item is selected
            }

        }


        private void messageListBox_Loaded(object sender, RoutedEventArgs e)
        {
            refreshData("all"); //shows all messages by default
        }

        //used to filter the listbox messages 
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            messageListBox.SelectedItem = null;
            ListBoxItem itm = (ListBoxItem)comboBox.SelectedValue;
            if (itm.Content.ToString() == "All")
            {
                refreshData("all");
            }
            else if (itm.Content.ToString() == "Tweets")
            {
                refreshData("Tweet");
            }
            else if (itm.Content.ToString() == "Standard Emails")
            {
                refreshData("Email");
            }
            else if (itm.Content.ToString() == "SIR Emails")
            {
                refreshData("SIR");
            }
            else if (itm.Content.ToString() == "SMS")
            {
                refreshData("SMS");
            }
        }
    }
}
