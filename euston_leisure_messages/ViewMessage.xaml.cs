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
    /// Interaction logic for ViewMessage.xaml
    /// </summary>
    public partial class ViewMessage : Window
    {
        /// <summary>
        /// shows the message details inside this window
        /// </summary>
        /// <param name="M"></param>
        public ViewMessage(string M)
        {
            InitializeComponent();
            getFrameData(M);
        }
        /// <summary>
        /// can add pages 
        /// </summary>
        /// <param name="pageIN">the page to be added to this window</param>
        public ViewMessage(Page pageIN)
        {
            InitializeComponent();
            this.Content = pageIN;
        }

        /// <summary>
        /// sets message status to seen if not already
        /// </summary>
        /// <param name="m"></param>
        public void getFrameData(string m)
        {
            MessageHolder.refresher.addNewSeen();
            
                Page gen = new MessageReaderViewer(MessageHolder.messages[m]);
                frame.Content = gen;
            }
        }

    }

