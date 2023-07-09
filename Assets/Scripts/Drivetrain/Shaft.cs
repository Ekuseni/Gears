using System.Collections.Generic;

namespace Drivetrain
{
    public class Shaft : DrivetrainJoint
    {
        protected internal override void TransmitRotation(float angularVelocity, DrivetrainElement drivetrainElement)
        {
            base.TransmitRotation(angularVelocity, drivetrainElement);
        
            foreach (var connectedDrivetrainElement in connectedDrivetrainElements)
            {
                if (connectedDrivetrainElement != drivetrainElement)
                {
                    connectedDrivetrainElement.TransmitRotation(angularVelocity, this);    
                }
            }
        }
    }
}

