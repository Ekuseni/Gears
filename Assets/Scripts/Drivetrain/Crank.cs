using UnityEngine;
using UnityEngine.EventSystems;
namespace Drivetrain
{
    public class Crank : Gear, IDragHandler
    {
        private Camera mainCamera;

#if UNITY_EDITOR
    [SerializeField] private bool autoRotate;
    [SerializeField] private float autoRotateSpeed;
#endif
    
        private void Start()
        {
            mainCamera = Camera.main;
        }
    
#if UNITY_EDITOR
    void Update()
    {
        if(!autoRotate) return;
        
        TransmitRotation(autoRotateSpeed * Time.deltaTime, this);
    }
#endif

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pivotScreenPoint = mainCamera.WorldToScreenPoint(globalPivotPoint);
            Vector2 mouseScreenPoint = Input.mousePosition;
            Vector2 lastMouseScreenPoint = mouseScreenPoint - eventData.delta;
       
            Vector2 pivotToMouse = mouseScreenPoint - pivotScreenPoint;
            Vector2 pivotToLastMouse = lastMouseScreenPoint - pivotScreenPoint;
       
            float angle = Vector2.SignedAngle(pivotToLastMouse, pivotToMouse);
            TransmitRotation(angle,this);
        }
    }
}

