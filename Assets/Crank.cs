using UnityEngine;
using UnityEngine.EventSystems;

public class Crank : Gear, IDragHandler
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Rotate(10 * Time.deltaTime, this);
    }

    public void OnDrag(PointerEventData eventData)
    {
       Vector2 pivotScreenPoint = mainCamera.WorldToScreenPoint(transform.position + transform.localRotation * localPivotPoint);
       Vector2 mouseScreenPoint = Input.mousePosition;
       Vector2 lastMouseScreenPoint = mouseScreenPoint - eventData.delta;
       
       Vector2 pivotToMouse = mouseScreenPoint - pivotScreenPoint;
       Vector2 pivotToLastMouse = lastMouseScreenPoint - pivotScreenPoint;
       
       float angle = Vector2.SignedAngle(pivotToLastMouse, pivotToMouse);
       Rotate(angle, this);
    }
}
