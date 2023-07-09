using UnityEngine;

namespace Drivetrain
{
    public class Clutch : DrivetrainJoint
    {
        //Created clutch component to make animation look good. Gear train in provided model is closed loop.
        //This way we can check that depending on witch clutch is engaged loop direction changes
        //Didn't implement clutch handle because clutch sleeve is not separated from crank shaft mesh
        //Still if needed this could be extended and used by other components that could drive clutch engagement
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