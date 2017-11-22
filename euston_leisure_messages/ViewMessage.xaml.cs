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
    /// ViewMessage.cs - This class displays an individual message inside the current window.
    /// </summary>
    
    public partial class ViewMessage : Window
    {
        /// <summary>
        /// Displays message inside a textbox.
        /// </summary>
        /// <param name="m1">Message body.</param>
        public ViewMessage(string m1)
        {
            InitializeComponent();
            CreateBox(m1);
        }
        /// <summary>
        /// Add the new view as a page.
        /// </summary>
        /// <param name="addBox">Add the box that displays message.</param>
        public ViewMessage(Page addBox)
        {
            InitializeComponent();
            this.Content = addBox;
        }

        /// <summary>
        /// Talks to the refresher.
        /// </summary>
        /// <param name="m">Message body</param>
        public void CreateBox(string m)
        {
            MessageHolder.refresher.AddNewSeen();
            
                Page display = new MessageReaderViewer(MessageHolder.messages[m]);
                frame.Content = display;
         }
     }
}

