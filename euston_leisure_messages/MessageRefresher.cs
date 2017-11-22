using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

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
    /// MessageRefresher.cs - This class is responsible for refreshing the page when new messages are added.
    /// </summary>

    public class MessageRefresher
    {
        public List<INotifier> Notifier { get; set; }
        public int NumberOfMessages { get; set; } 
        private static MessageRefresher instance; 

        private MessageRefresher()
        {
            this.Notifier = new List<INotifier>();
            RefreshMessages();
        }

        public void AddNewSeen()
        {
            instance.NumberOfMessages++;
            instance.Notify();
        }

        public static MessageRefresher GetInstance()
        {
            if (instance == null)
            {
                instance = new MessageRefresher();
            }
            return instance;
        }

        /// Timer used to refresh messages.
        private void RefreshMessages()
        {
            Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
        }

        public void AddNotifier(INotifier win)
        {
            Notifier.Add(win);
        }

        /// Notify that a change has occurred.
        private void Notify()
        {
            foreach (INotifier n in Notifier)
            {
                n.Notification();
            }
        }

        // Checks number of messages.
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (MessageHolder.messages.Count > NumberOfMessages)
            {
                Notify();
            }
        }
    }
}
