using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic {
    public class ReticleEventTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private long startTime;
        private long durationConstant = 3;
        private StorageEditor storageEditor = new StorageEditor();
        private SceneEditor sceneEditor = new SceneEditor();

        public void OnPointerEnter(PointerEventData eventData)
        {
            startTime = DateTime.Now.Ticks;
            //todo pointer hover
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //double duration = TimeSpan.FromTicks(DateTime.Now.Ticks - startTime).TotalSeconds;
            //string id = notification.transform.Find("Id").GetComponent<TextMeshPro>().text;
            //string sourceName = notification.transform.Find("Source").GetComponent<TextMeshPro>().text;
            //string name = "";
            //if (duration >= durationConstant && name.Equals("Hide"))
            //{
            //    Dictionary<string, NotificationsStorage> orderedNotifications = storageEditor.removeFromStorage(id, sourceName);
            //    sceneEditor.rebuildScene(orderedNotifications);
            //}   
            //if (duration >= durationConstant && name.Equals("MarkAsRead"))
            //{
            //    Dictionary<string, NotificationsStorage> orderedNotifications = storageEditor.removeFromStorage(id, sourceName);
            //    sceneEditor.rebuildScene(orderedNotifications);
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
            //}
        }
    }
}
