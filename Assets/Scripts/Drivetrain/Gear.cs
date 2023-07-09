using System;
using UnityEngine;

namespace Drivetrain
{
    public class Gear : DrivetrainElement
    {
        [SerializeField] protected float gearRadius;

        protected internal override void TransmitRotation(float angularVelocity, DrivetrainElement drivetrainElement)
        {
            float adjustedVelocity = ApplyRatioToVelocity(angularVelocity, drivetrainElement);
            Rotate(adjustedVelocity);
        
            foreach (var connectedDrivetrainElement in connectedDrivetrainElements)
            {
                if (connectedDrivetrainElement != drivetrainElement)
                {
                    connectedDrivetrainElement.TransmitRotation(connectedDrivetrainElement is Gear ? -adjustedVelocity : adjustedVelocity, this);    
                }
            }
        
        }

        protected virtual float ApplyRatioToVelocity(float angularVelocity, DrivetrainElement drivetrainElement)
        {
            if (drivetrainElement is DrivetrainJoint)
                return angularVelocity;

            if (drivetrainElement is Gear gear)
                return angularVelocity * (gear.gearRadius / gearRadius);

            throw new Exception("Unknown drivetrain element type");
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
        
            Gizmos.color = Color.cyan;
            int steps = 20;
            Vector3 circleRotation = new Vector3(0, 90, 0);
        
            for(int i = 0; i < steps; i ++)
            {
                Vector3 start =  new Vector3(Mathf.Cos(i * (Mathf.PI * 2) / steps), Mathf.Sin(i * (Mathf.PI * 2) / steps), 0) * gearRadius;
                Vector3 end = new Vector3(Mathf.Cos((i + 1) * (Mathf.PI * 2) / steps), Mathf.Sin((i + 1) * (Mathf.PI * 2) / steps), 0) * gearRadius;
            
                Gizmos.DrawLine(globalPivotPoint + transform.TransformDirection((localRotationAxis * Quaternion.Euler(circleRotation)) * start),
                    globalPivotPoint + transform.TransformDirection((localRotationAxis * Quaternion.Euler(circleRotation)) * end));
            }
        }
    }
}
