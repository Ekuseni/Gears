using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Gear : MonoBehaviour
{
    [Serializable]
    protected class Connection
    {
        [SerializeField] public Gear ConnectedGear;
        [SerializeField] public bool Shaft;
        [SerializeField] public bool ClutchEngaged;
        [SerializeField] public bool SharedModel;
        
        public float GearRatio(Gear gear)
        {
            if (Shaft)
            {
                if (ClutchEngaged)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            
            return gear.gearRadius / ConnectedGear.gearRadius;
        }
    }
    
    [SerializeField] protected Quaternion localRotationAxis;
    [SerializeField] protected Vector3 localPivotPoint;
    [SerializeField] protected float gearRadius;
    [SerializeField] protected List<Connection> connections;

    protected Vector3 globalPivotPoint => transform.position + transform.rotation * localPivotPoint;
    protected void Rotate(float angle, Gear gear, bool sharedModel = false)
    {
        if (!sharedModel)
        {
            transform.RotateAround(globalPivotPoint, transform.TransformDirection(localRotationAxis * Vector3.left), angle);    
        }

        foreach (var connection in connections)
        {
            if (connection.ConnectedGear != gear)
            {
                connection.ConnectedGear.Rotate(angle * connection.GearRatio(this) * (connection.Shaft || connection.SharedModel ? 1 : -1), this ,connection.SharedModel);    
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(globalPivotPoint, 0.1f);
        Gizmos.DrawLine(globalPivotPoint, globalPivotPoint + transform.TransformDirection(localRotationAxis * Vector3.left));
        
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