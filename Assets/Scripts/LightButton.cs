using UnityEngine;
using UnityEngine.EventSystems;

public class LightButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Color32 before;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        //before = eventData.pointerEnter.GetComponent<MeshRenderer>().material.color;
        //Color lightColor = before;
        //if (before.Equals(new Color32(50, 50, 255, 1))) lightColor = new Color(0, 0, 1);
        //if (before.Equals(new Color32(255, 50, 50, 1))) lightColor = new Color(1, 0, 0);
        //eventData.pointerEnter.GetComponent<MeshRenderer>().material.SetColor("_Color", lightColor);        
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
       //eventData.pointerEnter.GetComponent<MeshRenderer>().material.SetColor("_Color", before);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}