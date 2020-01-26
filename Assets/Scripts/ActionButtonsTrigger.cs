﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic
{
    public class ActionButtonsTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private long startTime;
        private long durationConstant = 3;
        private StorageEditor storageEditor = new StorageEditor();
        private Logger myLogger = new Logger(new LogHandler());

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            //todo fix the real joystick usage
            startTime = DateTime.Now.Ticks;
            if (eventData.pointerEnter.tag.Equals("GroupIcon"))
            {
                try
                {
                    eventData.pointerEnter.transform.parent.transform.Find("Hide").gameObject.SetActive(false);
                    eventData.pointerEnter.transform.parent.transform.Find("MarkAsRead").gameObject.SetActive(false);
                    eventData.pointerEnter.transform.Find("HideGroup").gameObject.SetActive(true);
                    eventData.pointerEnter.transform.Find("MarkAsReadGroup").gameObject.SetActive(true);
                }
                catch (NullReferenceException e) { }
            }
            else
            {
                try
                {
                    eventData.pointerEnter.transform.Find("GroupIcon").transform.Find("HideGroup").gameObject.SetActive(false);
                    eventData.pointerEnter.transform.Find("GroupIcon").transform.Find("MarkAsReadGroup").gameObject.SetActive(false);
                    eventData.pointerEnter.transform.Find("Hide").gameObject.SetActive(true);
                    eventData.pointerEnter.transform.Find("MarkAsRead").gameObject.SetActive(true);
                }
                catch (NullReferenceException e) { }
            }
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerEnter.tag.Equals("GroupIcon"))
            {
                try
                {
                    eventData.pointerEnter.transform.Find("HideGroup").gameObject.SetActive(false);
                    eventData.pointerEnter.transform.Find("MarkAsReadGroup").gameObject.SetActive(false);
                }
                catch (NullReferenceException e) { }
            }
            else
            {
                try
                {                
                    eventData.pointerEnter.transform.Find("Hide").gameObject.SetActive(false);
                    eventData.pointerEnter.transform.Find("MarkAsRead").gameObject.SetActive(false);
                }
                catch (NullReferenceException e){}
        }
            long duration = (long)TimeSpan.FromTicks(DateTime.Now.Ticks - startTime).TotalSeconds;
            processReticleEvent(eventData, duration);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            processReticleEvent(eventData, durationConstant);
        }

        private void processReticleEvent(PointerEventData eventData, long duration)
        {
            string tag = eventData.pointerEnter.tag;
            if (duration >= durationConstant)
            {
                if (tag.Equals("Notification"))
                {
                    try
                    {
                        string id = eventData.pointerEnter.transform.Find("Id").GetComponent<TextMeshPro>().text;
                        Color groupColor = eventData.pointerEnter.transform.Find("GroupIcon").GetComponent<MeshRenderer>().material.color;
                        string sourceName = groupColor.Equals(Color.gray) ? Global.silentGroupKey :
                            eventData.pointerEnter.transform.Find("Source").GetComponent<TextMeshPro>().text;
                        processHideTray(id, sourceName);
                    }
                    catch (NullReferenceException e) { }
                }
                else
                {
                    if (tag.Equals("Hide") || tag.Equals("MarkAsRead"))
                    {
                        try
                        {
                            string id = eventData.pointerEnter.transform.parent.transform.Find("Id").GetComponent<TextMeshPro>().text;
                            Color groupColor = eventData.pointerEnter.transform.parent.transform.Find("GroupIcon").GetComponent<MeshRenderer>().material.color;
                            string sourceName = groupColor.Equals(Color.gray) ? Global.silentGroupKey :
                                eventData.pointerEnter.transform.parent.Find("Source").GetComponent<TextMeshPro>().text;
                            processHideAndMarkAsRead(id, sourceName, tag);
                        }
                        catch (NullReferenceException e) { }
                    }
                    else // for all at once
                    {
                        try
                        {
                            Color groupColor = eventData.pointerEnter.transform.parent.GetComponent<MeshRenderer>().material.color;
                            string sourceName = groupColor.Equals(Color.gray) ? Global.silentGroupKey :
                                eventData.pointerEnter.transform.parent.transform.parent.Find("Source").GetComponent<TextMeshPro>().text;
                            processHideAndMarkAsReadAll(sourceName, tag);
                        }
                        catch (NullReferenceException e) { }
                    }
                }
            }
        }

        private void processHideTray(string id, string sourceName)
        {
            myLogger.Log(string.Format("Notification with id {0} from source {1} was clicked", id, sourceName));
            storageEditor.closeTray();
        }

        private void processHideAndMarkAsRead(string id, string sourceName, string tag)
        {
            myLogger.Log(string.Format("Notification with id {0} from source {1} was chosen to {2}", id, sourceName, tag));
            storageEditor.removeFromStorage(id, sourceName);
        }

        private void processHideAndMarkAsReadAll(string sourceName, string tag)
        {
            myLogger.Log(string.Format("Notifications from source {0} were chosen to {1}", sourceName, tag));
            storageEditor.removeAllFromStorage(sourceName);
        }
    }
}
