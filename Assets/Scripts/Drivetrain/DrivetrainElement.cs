using System.Collections.Generic;
using UnityEngine;

//This class is the base class for all drivetrain elements it controls movement and rotation of the drivetrain elements
//Proper implementation of different types of drivetrain elements is done in the derived classes
//This allows for different types of drivetrain elements to be added as connected elements
//allowing for a more complex drivetrain to be created with simple function call resembling a tree traversal

namespace Drivetrain
{
    public abstract class DrivetrainElement : MonoBehaviour
    {
        //Remove need for rotating same mesh twice if two differently sized gears are part of same mesh
        [SerializeField] private bool applyRotationToTransform;
        //Added serialized field for pivot point and rotation axis
        //Used local space so that when gear is far off center of Scene gizmos will appear in proximity to mesh being worked on
        //Created possibility of custom rotation pivot and axis for each gear to enable gears connecting for example at 90 degree angle
        [SerializeField] protected Quaternion localRotationAxis;
        [SerializeField] protected Vector3 localPivotPoint;
        [SerializeField] protected List<DrivetrainElement> connectedDrivetrainElements;
        
        protected Vector3 globalPivotPoint => transform.position + transform.rotation * localPivotPoint;
    
        protected void Rotate(float angle)
        {
            //Remove need for rotating same mesh twice if two differently sized gears are part of same mesh
            if (!applyRotationToTransform) return;
            transform.RotateAround(globalPivotPoint, transform.TransformDirection(localRotationAxis * Vector3.left), angle);
        }

        //Could be implemented for ratchets or worm gears
        protected void Translate()
        {
            
        }

        //If needed torque could be implemented here
        //Sanity check for impossible gear trains could be implemented here
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

