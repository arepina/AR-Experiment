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

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            startTime = DateTime.Now.Ticks;
            if (eventData.pointerEnter.tag.Equals("GroupIcon"))
            {
                groupActionButtonsShow(eventData);
            }
            else
            {
                localActionButtonsShow(eventData);
            }
        }

        private void groupActionButtonsShow(PointerEventData eventData)
        {
            try
            {
                eventData.pointerEnter.transform.parent.transform.Find("Hide").gameObject.SetActive(false);
                eventData.pointerEnter.transform.parent.transform.Find("MarkAsRead").gameObject.SetActive(false);
                eventData.pointerEnter.transform.Find("HideGroup").gameObject.SetActive(true);
                eventData.pointerEnter.transform.Find("MarkAsReadGroup").gameObject.SetActive(true);
            }
            catch (NullReferenceException e)
            {
                myLogger.LogError("Error", e);
            }
        }

        private void localActionButtonsShow(PointerEventData eventData)
        {
            Debug.Log("localActionButtonsShow");
            try
            {
                if (!FindObjectOfType<GlobalCommon>().typeName.Contains("Sticker"))
                {
                    eventData.pointerEnter.transform.parent.transform.Find("GroupIcon").transform.Find("HideGroup").gameObject.SetActive(false);
                    eventData.pointerEnter.transform.parent.transform.Find("GroupIcon").transform.Find("MarkAsReadGroup").gameObject.SetActive(false);
                }
                eventData.pointerEnter.transform.parent.transform.Find("Hide").gameObject.SetActive(true);
                eventData.pointerEnter.transform.parent.transform.Find("MarkAsRead").gameObject.SetActive(true);
            }
            catch (NullReferenceException e)
            {
                myLogger.LogError("Error", e);
            }
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerEnter.tag.Equals("GroupIcon"))
            {
                groupActionButtonsHide(eventData);
            }
            else
            {
                localActionButtonsHide(eventData);
            }
            long duration = (long)TimeSpan.FromTicks(DateTime.Now.Ticks - startTime).TotalSeconds;
            processReticleEvent(eventData, duration);
        }

        private void groupActionButtonsHide(PointerEventData eventData)
        {
            try
            {
                eventData.pointerEnter.transform.Find("HideGroup").gameObject.SetActive(false);
                eventData.pointerEnter.transform.Find("MarkAsReadGroup").gameObject.SetActive(false);
            }
            catch (NullReferenceException e)
            {
                myLogger.LogError("Error", e);
            }
        }

        private void localActionButtonsHide(PointerEventData eventData)
        {
            try
            {
                eventData.pointerEnter.transform.parent.transform.Find("Hide").gameObject.SetActive(false);
                eventData.pointerEnter.transform.parent.transform.Find("MarkAsRead").gameObject.SetActive(false);
            }
            catch (NullReferenceException e)
            {
                myLogger.LogError("Error", e);
            }
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
                    actionOpenSourceApplication(eventData);
                }
                else
                {
                    if (tag.Equals("Hide") || tag.Equals("MarkAsRead"))
                    {
                        actionProcessLocalAction(eventData);
                    }
                    else 
                    {
                        actionProcessGroup(eventData);
                    }
                }
            }
        }

        private void actionOpenSourceApplication(PointerEventData eventData)
        {
            try
            {
                string id = eventData.pointerEnter.transform.parent.transform.Find("Id").GetComponent<TextMeshPro>().text;
                Color groupColor;
                string sourceName;
                string author;
                string creationTime;
                if (!FindObjectOfType<GlobalCommon>().typeName.Contains("Sticker"))
                {
                    groupColor = eventData.pointerEnter.transform.parent.transform.Find("GroupIcon").GetComponent<MeshRenderer>().material.color;
                    sourceName = groupColor.Equals(Color.gray) ? GlobalCommon.silentGroupKey :
                        eventData.pointerEnter.transform.parent.transform.Find("Source").GetComponent<TextMeshPro>().text;
                    author = eventData.pointerEnter.transform.parent.transform.Find("Author").GetComponent<TextMeshPro>().text;
                    creationTime = eventData.pointerEnter.transform.parent.transform.Find("Timestamp").GetComponent<TextMeshPro>().text;
                }
                else
                {
                    groupColor = eventData.pointerEnter.transform.parent.transform.Find("Box").GetComponent<SpriteRenderer>().material.color;
                    sourceName = groupColor.Equals(Color.gray) ? GlobalCommon.silentGroupKey :
                        eventData.pointerEnter.transform.parent.transform.Find("Source").GetComponent<TextMeshPro>().text;
                    author = eventData.pointerEnter.transform.parent.transform.Find("Author").GetComponent<TextMeshPro>().text;
                    creationTime = eventData.pointerEnter.transform.parent.transform.Find("Timestamp").GetComponent<TextMeshPro>().text;
                }
                processExperimentData(id, sourceName);
                processHideAndMarkAsRead(id, sourceName, tag);
            }
            catch (NullReferenceException e)
            {
                myLogger.LogError("Error", e);
            }
        }

        private void actionProcessLocalAction(PointerEventData eventData)
        {
            try
            {
                string id = eventData.pointerEnter.transform.parent.transform.Find("Id").GetComponent<TextMeshPro>().text;
                Color groupColor;
                if (!FindObjectOfType<GlobalCommon>().typeName.Contains("Sticker"))
                {
                    groupColor = eventData.pointerEnter.transform.parent.transform.Find("GroupIcon").GetComponent<MeshRenderer>().material.color;
                }
                else
                {
                    groupColor = eventData.pointerEnter.transform.parent.transform.Find("Box").GetComponent<SpriteRenderer>().material.color;
                }
                string sourceName = groupColor.Equals(Color.gray) ? GlobalCommon.silentGroupKey :
                    eventData.pointerEnter.transform.parent.Find("Source").GetComponent<TextMeshPro>().text;
                string creationTime = eventData.pointerEnter.transform.parent.Find("Timestamp").GetComponent<TextMeshPro>().text;
                processExperimentData(id, sourceName);
                processHideAndMarkAsRead(id, sourceName, tag);
            }
            catch (NullReferenceException e)
            {
                myLogger.LogError("Error", e);
            }
        }

        private void actionProcessGroup(PointerEventData eventData)
        {
            try
            {
                Color groupColor = eventData.pointerEnter.transform.parent.GetComponent<MeshRenderer>().material.color;
                string sourceName = groupColor.Equals(Color.gray) ? GlobalCommon.silentGroupKey :
                    eventData.pointerEnter.transform.parent.transform.parent.Find("Source").GetComponent<TextMeshPro>().text;
                string creationTime = eventData.pointerEnter.transform.parent.transform.parent.Find("Timestamp").GetComponent<TextMeshPro>().text;
                string id = eventData.pointerEnter.transform.parent.transform.parent.Find("Id").GetComponent<TextMeshPro>().text;
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
