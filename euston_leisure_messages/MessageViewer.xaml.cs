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
    /// SET09102 2017-8 TR1 001 - Software Engineering
    /// Euston Leisure Message System
    /// Version 0.5.0
    /// Alexander Barker 
    /// 40333139
    /// Created on 30th October 2017
    /// Last Updated on 22th November 2017
    /// </summary>
    /// <summary>
    /// MessageViewer.cs - This class creates a list of clickable messages of viewing.
    /// </summary>
    
    public partial class MessageViewer : Window
    {

        public MessageViewer()
        {
            InitializeComponent();
        }

        /// Get all the boxes.
        /// <param name="getType">Get the type of message.</param>
        private void RefreshData(string getType)
        {
            if (getType == "all") // Display all the messages.
            {
                messageListBox.Items.Clear(); // Clear the list.
                foreach (Message m in MessageHolder.messages.Values)
                {
                    string messageType = m.GetType().ToString();
                    string[] splitval = messageType.Split('.');
                    MessageReader proc = m.returnData();
                    string body = proc.body;
                    messageListBox.Items.Add(MakeList(proc.header, body, splitval[splitval.Length - 1]));
                }
            }
            else // Display by type.
            {
                messageListBox.Items.Clear();
                foreach (Message m in MessageHolder.messages.Values)
                {
                    string messageType = m.GetType().ToString();
                    string[] splitval = messageType.Split('.');
                    MessageReader proc = m.returnData();
                    string body = proc.body;

                    if (splitval[splitval.Length - 1] == getType)
                    {
                        messageListBox.Items.Add(MakeList(proc.header, body, splitval[splitval.Length - 1]));
                    }
                }
            }
        }

        // Creates the box for the list.
        /// <param name="header">Message header.</param>
        /// <param name="body">Message body.</param>
        /// <param name="messageType">Message type.</param>
        private Grid MakeList(string header, string body, string messageType)
        {
            Grid box = new Grid();
            box.Width = messageListBox.Width;
            box.Height = 50;
            box.HorizontalAlignment = HorizontalAlignment.Left;
            box.VerticalAlignment = VerticalAlignment.Top;
            box.ShowGridLines = true;

            // Define the Columns
            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            ColumnDefinition colDef3 = new ColumnDefinition();

            box.ColumnDefinitions.Add(colDef1);
            box.ColumnDefinitions.Add(colDef2);
            box.ColumnDefinitions.Add(colDef3);

            // Define the Rows
            RowDefinition rowDef1 = new RowDefinition();
            box.RowDefinitions.Add(rowDef1);

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

            box.Children.Add(txt1);
            box.Children.Add(txt2);
            box.Children.Add(txt3);

            return box;
        }

        // Detect message selection.
        private void MessageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                String msg = "";
                Grid message = (Grid)messageListBox.SelectedValue;
                msg += ((Label)message.Children[0]).Content;

                Window messageViewer = new ViewMessage(msg);
                messageViewer.Show();
            }
            catch (NullReferenceException ex)
            {
                // Causes crashed. Removed.
            }
        }

        private void messageListBox_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData("all"); // Show all messages at the start.
        }

        // Filter messages by type.
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            messageListBox.SelectedItem = null;
            ListBoxItem itm = (ListBoxItem)comboBox.SelectedValue;
            if (itm.Content.ToString() == "All")
            {
                RefreshData("all");
            }
            else if (itm.Content.ToString() == "Tweets")
            {
                RefreshData("Tweet");
            }
            else if (itm.Content.ToString() == "Standard Emails")
            {
                RefreshData("Email");
            }
            else if (itm.Content.ToString() == "SIR Emails")
            {
                RefreshData("SIR");
            }
            else if (itm.Content.ToString() == "SMS")
            {
                RefreshData("SMS");
            }
        }
    }
}
