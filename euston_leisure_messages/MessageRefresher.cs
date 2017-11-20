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
    /// Version 0.4.3
    /// Alexander Barker 
    /// 40333139
    /// Created on 30th October 2017
    /// Last Updated on 20th November 2017
    /// </summary>
    /// <summary>
    /// MessageRefresher.cs - 
    /// </summary>

    public class MessageRefresher
    {
        public List<Observer> observers { get; set; } //windows to notify of changes
        public int numberOfMessages { get; set; } //number of read messages, subtract from total messages to find notification number
        private static MessageRefresher instance; //singleton static variable to make sure only one instance can occur

        private MessageRefresher()
        {
            this.observers = new List<Observer>();
            refreshMessages();
        }

        /// increments the number of seen messages and notifies any windows registered
        public void addNewSeen()
        {
            instance.numberOfMessages++;
            instance.notifyObservers();
        }

        //singleton pattern 
        public static MessageRefresher getInstance()
        {
            if (instance == null)
            {
                instance = new MessageRefresher();
            }
            return instance;
        }

        /// timer used to check for new messages, checks every 5 seconds
        private void refreshMessages()
        {
            Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
        }

        //register a class requiring notification of updates to messages
        /// <param name="win">observer to add to list</param>
        public void addObserver(Observer win)
        {
            observers.Add(win);
        }

        /// notify class that a change has occurred 
        private void notifyObservers()
        {
            foreach (Observer o in observers)
            {
                o.receiveNotification();
            }
        }

        //check if seen messages is less than total number of messages, if so notify observers
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (MessageHolder.messages.Count > numberOfMessages)
            {
                notifyObservers();
            }
        }
    }
}
