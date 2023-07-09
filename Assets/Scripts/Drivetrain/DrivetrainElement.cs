using System.Collections.Generic;
using UnityEngine;

namespace Drivetrain
{
    public abstract class DrivetrainElement : MonoBehaviour
    {
        [SerializeField] private bool applyRotationToTransform;
        [SerializeField] protected Quaternion localRotationAxis;
        [SerializeField] protected Vector3 localPivotPoint;
        [SerializeField] protected List<DrivetrainElement> connectedDrivetrainElements;
        
        protected Vector3 globalPivotPoint => transform.position + transform.rotation * localPivotPoint;
    
        protected void Rotate(float angle)
        {
            if (!applyRotationToTransform) return;
            transform.RotateAround(globalPivotPoint, transform.TransformDirection(localRotationAxis * Vector3.left), angle);
        }

        protected internal abstract void TransmitRotation(float angularVelocity, DrivetrainElement drivetrainElement);
    
        protected virtual void OnDrawGizmos()
        {
            if (applyRotationToTransform)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(globalPivotPoint, 0.1f);
                Gizmos.DrawLine(globalPivotPoint, globalPivotPoint + transform.TransformDirection(localRotationAxis * Vector3.left));   
            }
        }
    }
}

