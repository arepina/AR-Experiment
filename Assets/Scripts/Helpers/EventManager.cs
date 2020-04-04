using System;
using System.Collections.Generic;

namespace Logic
{
    public enum EVENT { NotificationCreated };
    public class EventManager
    {
        // Stores the delegates that get called when an event is fired
        private static Dictionary<EVENT, Action> eventTable
                     = new Dictionary<EVENT, Action>();
 
        // Adds a delegate to get called for a specific event
        public static void AddHandler(EVENT evnt, Action action)
        {
            if (!eventTable.ContainsKey(evnt)) eventTable[evnt] = action;
            else eventTable[evnt] += action;
        }
 
        // Fires the event
        public static void Broadcast(EVENT evnt)
        {
            if (eventTable.Count != 0 && eventTable[evnt] != null) eventTable[evnt]();
        }
    }
}