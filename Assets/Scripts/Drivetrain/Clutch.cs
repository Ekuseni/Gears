using UnityEngine;

namespace Drivetrain
{
    public class Clutch : DrivetrainJoint
    {
        [SerializeField, Range(0f, 1f)] private float clutchEngagementRatio;

        protected internal override void TransmitRotation(float angularVelocity, DrivetrainElement drivetrainElement)
        {
            base.TransmitRotation(angularVelocity, drivetrainElement);

            foreach (var connectedDrivetrainElement in connectedDrivetrainElements)
            {
                if (connectedDrivetrainElement != drivetrainElement)
                {
                    connectedDrivetrainElement.TransmitRotation(angularVelocity * clutchEngagementRatio, this);
                }
            }
        }
    }
}