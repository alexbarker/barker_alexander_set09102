﻿using System;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace euston_leisure_messages.Views
{
    /// <summary>
    /// Interaction logic for AddTweet.xaml
    /// </summary>
    public partial class AddTweet : Window
    {
        public AddTweet()
        {
            InitializeComponent();
        }

        private void Close_Tweet_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Tweet_Button_Click(object sender, RoutedEventArgs e)
        {



            this.Close();
        }
    }
}
