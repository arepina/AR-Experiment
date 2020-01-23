using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic
{
    public class ReticleEventTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private long startTime;
        private long durationConstant = 3;
        private StorageEditor storageEditor = new StorageEditor();
        private SceneEditor sceneEditor = new SceneEditor();

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            //todo fix the real joystick usage
            startTime = DateTime.Now.Ticks;
            if (transform.parent != null)
            {
                ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.pointerEnterHandler);
            }
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            long duration = (long)TimeSpan.FromTicks(DateTime.Now.Ticks - startTime).TotalSeconds;
            processReticleEvent(eventData, duration);
            if (transform.parent != null)
            {
                ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.pointerExitHandler);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            processReticleEvent(eventData, durationConstant);
        }

        private void processReticleEvent(PointerEventData eventData, long duration)
        {
            string id = eventData.pointerEnter.transform.parent.Find("Id").GetComponent<TextMeshPro>().text;
            string header = eventData.pointerEnter.transform.parent.Find("Header").GetComponent<TextMeshPro>().text;
            string sourceName = header.Contains("Silent:") ? Global.silentGroupKey :
                eventData.pointerEnter.transform.parent.Find("Source").GetComponent<TextMeshPro>().text;
            string name = eventData.pointerEnter.tag;
            if (duration >= durationConstant)
            {
                //todo fix the behaviour for different buttons
                if(name.Equals("Hide")) processHide(id, sourceName);
                if (name.Equals("MarkAsRead")) processMarkAsRead(id, sourceName);
                if (name.Equals("HideAll")) processHideAll(sourceName);
                if (name.Equals("MarkAsReadAll")) processMarkAsReadAll(sourceName);
            }
        }

        private void processHide(string id, string sourceName)
        {
            Dictionary<string, NotificationsStorage> orderedNotifications = storageEditor.removeFromStorage(id, sourceName);
            sceneEditor.rebuildScene(orderedNotifications);
        }

        private void processHideAll(string sourceName)
        {
            Dictionary<string, NotificationsStorage> orderedNotifications = storageEditor.removeAllFromStorage(sourceName);
            sceneEditor.rebuildScene(orderedNotifications);
        }

        private void processMarkAsRead(string id, string sourceName)
        {
            processMarkAsRead(id, sourceName);
            openApp(sourceName);
        }

        private void processMarkAsReadAll(string sourceName)
        {
            processHideAll(sourceName);
            openApp(sourceName);
        }

        private void openApp(string sourceName)
        {
            //todo check
            switch (sourceName)
            {
                case "Telegram":
                    Application.OpenURL("tg://");
                    break;
                case "WhatsApp":
                    Application.OpenURL("whatsapp://");
                    break;
                case "Яндекс.Почта":
                    Application.OpenURL("yandexmail://");
                    break;
                case "YouTube":
                    Application.OpenURL("youtube://");
                    break;
            }
        }
    }
}
