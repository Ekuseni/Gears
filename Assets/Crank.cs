using UnityEngine;
using UnityEngine.EventSystems;

public class Crank : MonoBehaviour, IDragHandler
{
    [SerializeField] Quaternion localRotationAxis;
    [SerializeField] Vector3 localPivotPoint;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void OnDrag(PointerEventData eventData)
    {
       Vector2 pivotScreenPoint = mainCamera.WorldToScreenPoint(transform.position + transform.localRotation * localPivotPoint);
       Vector2 mouseScreenPoint = Input.mousePosition;
       Vector2 lastMouseScreenPoint = mouseScreenPoint - eventData.delta;
       
       Vector2 pivotToMouse = mouseScreenPoint - pivotScreenPoint;
       Vector2 pivotToLastMouse = lastMouseScreenPoint - pivotScreenPoint;
       
       float angle = Vector2.SignedAngle(pivotToLastMouse, pivotToMouse);
       transform.RotateAround(transform.position + transform.rotation * localPivotPoint, transform.TransformDirection(localRotationAxis * Vector3.left), angle);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position + (transform.rotation * localPivotPoint), 0.1f);
        Gizmos.DrawLine(transform.position + (transform.rotation * localPivotPoint), transform.position + (transform.rotation * localPivotPoint) + transform.TransformDirection(localRotationAxis * Vector3.left));
    }
}
