using System;
using UnityEngine;

namespace Logic
{
    public class ActionButtonsTrigger : MonoBehaviour
    {
        private long startTime;
        public GameObject markAsRead;
        public GameObject hide;
        public GameObject markAsReadAll;
        public GameObject hideAll;
        public GameObject notification;

        public void groupEnter()
        {
            markAsReadAll.SetActive(true);
            hideAll.SetActive(true);
            hide.SetActive(false);
            markAsRead.SetActive(false);
        }

        public void groupExit()
        {
            markAsReadAll.SetActive(false);
            hideAll.SetActive(false);
            hide.SetActive(false);
            markAsRead.SetActive(false);
        }

        public void baseEnter()
        {
            if (markAsReadAll) // stickers do not have these btns
            {
                if (!hideAll.activeSelf && !markAsReadAll.activeSelf)
                {                    
                    markAsReadAll.SetActive(false);
                    hideAll.SetActive(false);
                }
            }
            hide.SetActive(true);
            markAsRead.SetActive(true);
        }

        public void baseExit()
        {
            if (markAsReadAll) // stickers do not have these btns
            {
                markAsReadAll.SetActive(false);
                hideAll.SetActive(false);
            }
            hide.SetActive(false);
            markAsRead.SetActive(false);
        }

        public void baseClick()
        {
            FindObjectOfType<ActionsProcessor>().actionOpenSourceApplication(notification);
        }

        public void anyButtonEnter()
        {
            startTime = DateTime.Now.Ticks;
        }

        public void allClick()
        {
            FindObjectOfType<ActionsProcessor>().actionProcessGroup(notification, tag);
        }

        public void allExit()
        {
            long duration = (long)TimeSpan.FromTicks(DateTime.Now.Ticks - startTime).TotalSeconds;
            if (duration >= FindObjectOfType<GlobalCommon>().waitForActionToBeAcceptedPeriod)
            {
                FindObjectOfType<ActionsProcessor>().actionProcessGroup(notification, tag);
            }
        }

        public void localClick()
        {
            FindObjectOfType<ActionsProcessor>().actionProcessLocalAction(notification, tag);
        }

        public void localExit()
        {
            long duration = (long)TimeSpan.FromTicks(DateTime.Now.Ticks - startTime).TotalSeconds;
            if (duration >= FindObjectOfType<GlobalCommon>().waitForActionToBeAcceptedPeriod)
            {
                FindObjectOfType<ActionsProcessor>().actionProcessLocalAction(notification, tag);
            }
        }
    }
}
