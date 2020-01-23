using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic
{
    public class CubeReticleEventTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            //eventData.pointerEnter.transform.Find("Hide").gameObject.SetActive(true);
            //eventData.pointerEnter.transform.Find("MarkAsRead").gameObject.SetActive(true);
            if(transform.parent != null)
            {
                ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.pointerEnterHandler);
            }
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            //eventData.pointerEnter.transform.Find("Hide").gameObject.SetActive(false);
            //eventData.pointerEnter.transform.Find("MarkAsRead").gameObject.SetActive(false);
            if (transform.parent != null)
            {
                ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.pointerExitHandler);
            }
        }
    }
}
