﻿using System.Collections;
using System;
using UnityEngine;

namespace Logic
{
    public class NotificationsGenerator : MonoBehaviour
    {

        private System.Random random = new System.Random();
        private ArrayList notifications = new ArrayList();
        public bool isRunning;
        public int secondsRange;

        public void Update()
        {
            if (isRunning)
            {
                StartCoroutine(Wait());
            }
        }

        public IEnumerator Wait()
        {
            isRunning = false;
            int pause = random.Next(1, secondsRange + 1);
            Debug.Log("pause: " + pause);
            createNotification();
            yield return new WaitForSeconds(pause);
            isRunning = true;
        }

        public void Start()
        {
            isRunning = true;
        }

        public void Stop()
        {
            isRunning = false;
        }

        private void createNotification()
        {
            Notification n = new Notification("sourceImage: " + Guid.NewGuid().ToString(),
                                              "sourceName: " + Guid.NewGuid().ToString(),
                                              "author: " + Guid.NewGuid().ToString(),
                                              "text: " + Guid.NewGuid().ToString(),
                                              "header: " + Guid.NewGuid().ToString(),
                                              DateTime.Now.ToString());
            notifications.Add(n);
            Debug.Log(n);
        }

        public ArrayList Notifications
        {
            get
            {
                return notifications;
            }
        }
    }

}