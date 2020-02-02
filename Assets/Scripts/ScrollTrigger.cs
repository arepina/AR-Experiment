using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Logic
{
    public class ScrollTrigger : ScrollRect, IMoveHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private const float speedMultiplier = 0.01f;
        public float xSpeed = 0;
        public float ySpeed = 0;
        private float hPos, vPos;
        private Logger myLogger = new Logger(new LogHandler());

        void IMoveHandler.OnMove(AxisEventData e)
        {
            xSpeed += e.moveVector.x * (Mathf.Abs(xSpeed) + 0.1f);
            ySpeed += e.moveVector.y * (Mathf.Abs(ySpeed) + 0.1f);
        }

        void Update()
        {

            ySpeed = Input.GetAxis("Vertical");

            hPos = horizontalNormalizedPosition + xSpeed * speedMultiplier;
            vPos = verticalNormalizedPosition + ySpeed * speedMultiplier;

            xSpeed = Mathf.Lerp(xSpeed, 0, 0.1f);
            ySpeed = Mathf.Lerp(ySpeed, 0, 0.1f);

            if (movementType == MovementType.Clamped)
            {
                hPos = Mathf.Clamp01(hPos);
                vPos = Mathf.Clamp01(vPos);
            }

            normalizedPosition = new Vector2(hPos, vPos);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {

        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
            base.OnBeginDrag(eventData);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
}