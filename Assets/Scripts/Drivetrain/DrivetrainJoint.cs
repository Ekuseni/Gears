namespace Drivetrain
{
    public class DrivetrainJoint : DrivetrainElement
    {
        protected internal override void TransmitRotation(float angularVelocity, DrivetrainElement drivetrainElement)
        {
            Rotate(angularVelocity);
        }
    }
}


