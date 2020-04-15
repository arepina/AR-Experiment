using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic
{
    public class ActionButtonsTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private long startTime;
        private Logger myLogger = new Logger(new LogHandler());
        public GameObject markAsRead;
        public GameObject hide;
        public GameObject markAsReadAll;
        public GameObject hideAll;
        public GameObject notification;

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            startTime = DateTime.Now.Ticks;
            if (eventData.pointerEnter.tag.Equals("GroupIcon")
                || eventData.pointerEnter.tag.Equals("HideAll")
                || eventData.pointerEnter.tag.Equals("MarkAsReadAll"))
            {
                hide.SetActive(false);
                markAsRead.SetActive(false);
                hideAll.SetActive(true);
                markAsReadAll.SetActive(true);
            }
            if (eventData.pointerEnter.tag.Equals("Notification")
                || eventData.pointerEnter.tag.Equals("Hide")
                || eventData.pointerEnter.tag.Equals("MarkAsRead"))
            {
                if (!FindObjectOfType<GlobalCommon>().typeName.Contains("Sticker"))
                {
                    hideAll.SetActive(false);
                    markAsReadAll.SetActive(false);
                }
                hide.SetActive(true);
                markAsRead.SetActive(true);
            }
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerEnter.tag.Equals("GroupIcon"))
            {
                hideAll.SetActive(false);
                markAsReadAll.SetActive(false);
            }
            if (eventData.pointerEnter.tag.Equals("Notification"))
            {
                hide.SetActive(false);
                markAsRead.SetActive(false);
            }
            long duration = (long)TimeSpan.FromTicks(DateTime.Now.Ticks - startTime).TotalSeconds;
            processReticleEvent(eventData, duration);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            processReticleEvent(eventData, FindObjectOfType<GlobalCommon>().waitForActionToBeAcceptedPeriod);
        }

        private void processReticleEvent(PointerEventData eventData, float duration)
        {
            string tag = eventData.pointerEnter.tag;
            if (duration >= FindObjectOfType<GlobalCommon>().waitForActionToBeAcceptedPeriod)
            {
                if (tag.Equals("Notification"))
                {
                    actionOpenSourceApplication();
                }
                else
                {
                    if (tag.Equals("Hide") || tag.Equals("MarkAsRead"))
                    {
                        actionProcessLocalAction();
                    }
                    else 
                    {
                        actionProcessGroup();
                    }
                }
            }
        }

        private void actionOpenSourceApplication()
        {
            try
            {
                string id = notification.transform.Find("Id").GetComponent<TextMeshPro>().text;
                Color groupColor;
                string sourceName;
                string author;
                string creationTime;
                if (!FindObjectOfType<GlobalCommon>().typeName.Contains("Sticker"))
                {
                    groupColor = notification.transform.Find("GroupIcon").GetComponent<MeshRenderer>().material.color;
                    sourceName = groupColor.Equals(Color.gray) ? GlobalCommon.silentGroupKey :
                        notification.transform.Find("Source").GetComponent<TextMeshPro>().text;
                    author = notification.transform.Find("Author").GetComponent<TextMeshPro>().text;
                    creationTime = notification.transform.Find("Timestamp").GetComponent<TextMeshPro>().text;
                }
                else
                {
                    groupColor = notification.transform.Find("Box").GetComponent<SpriteRenderer>().material.color;
                    sourceName = groupColor.Equals(Color.gray) ? GlobalCommon.silentGroupKey :
                        notification.transform.Find("Source").GetComponent<TextMeshPro>().text;
                    author = notification.transform.Find("Author").GetComponent<TextMeshPro>().text;
                    creationTime = notification.transform.Find("Timestamp").GetComponent<TextMeshPro>().text;
                }
                processExperimentData(id, sourceName);
                processHideAndMarkAsRead(id, sourceName, tag);
            }
            catch (NullReferenceException e)
            {
                myLogger.LogError("Error", e);
            }
        }

        private void actionProcessLocalAction()
        {
            try
            {
                string id = notification.transform.Find("Id").GetComponent<TextMeshPro>().text;
                Color groupColor;
                if (!FindObjectOfType<GlobalCommon>().typeName.Contains("Sticker"))
                {
                    groupColor = notification.transform.Find("GroupIcon").GetComponent<MeshRenderer>().material.color;
                }
                else
                {
                    groupColor = notification.transform.Find("Box").GetComponent<SpriteRenderer>().material.color;
                }
                string sourceName = groupColor.Equals(Color.gray) ? GlobalCommon.silentGroupKey :
                    notification.transform.Find("Source").GetComponent<TextMeshPro>().text;
                string creationTime = notification.transform.Find("Timestamp").GetComponent<TextMeshPro>().text;
                processExperimentData(id, sourceName);
                processHideAndMarkAsRead(id, sourceName, tag);
            }
            catch (NullReferenceException e)
            {
                myLogger.LogError("Error", e);
            }
        }

        private void actionProcessGroup()
        {
            try
            {
                Color groupColor = notification.transform.GetComponent<MeshRenderer>().material.color;
                string sourceName = groupColor.Equals(Color.gray) ? GlobalCommon.silentGroupKey :
                    notification.transform.Find("Source").GetComponent<TextMeshPro>().text;
                string creationTime = notification.transform.Find("Timestamp").GetComponent<TextMeshPro>().text;
                string id = notification.transform.Find("Id").GetComponent<TextMeshPro>().text;
                processExperimentData(id, sourceName);
                processHideAndMarkAsReadAll(sourceName, tag);
            }
            catch (NullReferenceException e)
            {
                myLogger.LogError("Error", e);
            }
        }

        private void processExperimentData(string id, string sourceName)
        {
            var storage = FindObjectOfType<Storage>();
            Notification notification = storage.getFromStorage(id, sourceName);
            long reactionDuration = DateTime.Now.Ticks - notification.Timestamp;
            if (notification.isCorrect)
            {
                FindObjectOfType<ExperimentData>().numberOfNonIgnoredHaveToActNotifications += 1;
                FindObjectOfType<ExperimentData>().sumOfReactionTimeToNonIgnoredHaveToActNotifications += reactionDuration;
            }
            else
            {
                FindObjectOfType<ExperimentData>().numberOfInCorrectlyActedNotifications += 1;
            }
            string logInfo = notification.ToString(FindObjectOfType<ExperimentData>(), FindObjectOfType<GlobalCommon>().typeName, "REACTED", reactionDuration.ToString());
            FindObjectOfType<LogDataStorage>().NextLog(logInfo);
            FindObjectOfType<LogDataStorage>().SaveLogData();
        }

        private void processHideAndMarkAsRead(string id, string sourceName, string tag)
        {
            myLogger.Log(string.Format("Notification with id {0} from source {1} was chosen to {2}", id, sourceName, tag));
            var storage = FindObjectOfType<Storage>();
            storage.removeFromStorage(id, sourceName);
            var scene = FindObjectOfType<Scene>();
            scene.rebuildScene();
        }

        private void processHideAndMarkAsReadAll(string sourceName, string tag)
        {
            myLogger.Log(string.Format("Notifications from source {0} were chosen to {1}", sourceName, tag));
            var storage = FindObjectOfType<Storage>();
            storage.removeAllFromStorage(sourceName);
            var scene = FindObjectOfType<Scene>();
            scene.rebuildScene();
        }
    }
}
