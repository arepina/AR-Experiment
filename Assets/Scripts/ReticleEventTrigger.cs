using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic {
    public class ReticleEventTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private long startTime;
        private long durationConstant = 3;
        private StorageEditor storageEditor = new StorageEditor();
        private SceneEditor sceneEditor = new SceneEditor();

        public void OnPointerEnter(PointerEventData eventData)
        {
            startTime = DateTime.Now.Ticks;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            long duration = (long)TimeSpan.FromTicks(DateTime.Now.Ticks - startTime).TotalSeconds;
            processReticleEvent(eventData, duration);
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
            if (duration >= durationConstant && name.Equals("Hide"))
            {
                Dictionary<string, NotificationsStorage> orderedNotifications = storageEditor.removeFromStorage(id, sourceName);
                sceneEditor.rebuildScene(orderedNotifications);
            }
            if (duration >= durationConstant && name.Equals("MarkAsRead"))
            {
                Dictionary<string, NotificationsStorage> orderedNotifications = storageEditor.removeFromStorage(id, sourceName);
                sceneEditor.rebuildScene(orderedNotifications);
                //switch (sourceName)
                //{
                //    case "Telegram":
                //        Application.OpenURL("tg://");
                //        break;
                //    case "WhatsApp":
                //        Application.OpenURL("whatsapp://");
                //        break;
                //    case "Яндекс.Почта":
                //        Application.OpenURL("yandexmail://");
                //        break;
                //    case "YouTube":
                //        Application.OpenURL("youtube://");
                //        break;
                //}
            }
        }
    }
}
