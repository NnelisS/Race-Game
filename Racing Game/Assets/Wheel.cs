using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool powered = false;
    public float maxAngle = 90f;
    public float offset = 0f;
    public float rotationOffset = 90;

    private float turnAngle;
    private WheelCollider wcol;
    private Transform wmesh;


    private void Start()
    {
        wcol = GetComponentInChildren<WheelCollider>();
        wmesh = transform.Find("Wheel_Mesh");
    }

    private void Update()
    {
        Debug.Log(wcol.brakeTorque);
    }

    public void Steer(float steerInput)
    {
        turnAngle = steerInput * maxAngle + offset;
        wcol.steerAngle = turnAngle;
        wcol.motorTorque = 1000;
    }

    public void Accelerate(float powerInput)
    {
        if (powered)
        {
            wcol.motorTorque = powerInput;
        }
        else
        {
            wcol.motorTorque = 0f;
        }
    }

    public void UpdatePosition()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        wcol.GetWorldPose(out pos, out rot);
        wmesh.transform.position = pos;
        wmesh.transform.rotation = rot *= Quaternion.Euler(0, rotationOffset, 0);
    }
}
