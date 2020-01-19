using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic
{
    public class CubeReticleEventTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            eventData.pointerEnter.transform.parent.Find("Hide").gameObject.SetActive(true);
            eventData.pointerEnter.transform.parent.Find("MarkAsRead").gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            eventData.pointerEnter.transform.parent.Find("Hide").gameObject.SetActive(false);
            eventData.pointerEnter.transform.parent.Find("MarkAsRead").gameObject.SetActive(false);
        }
    }
}
