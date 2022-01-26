using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool powered = false;
    public float maxAngle = 90f;
    public float offset = 0f;

    private float turnAngle;
    private WheelCollider wcol;
    private Transform wmeshL;
    private Transform wmeshR;
    private Transform wmeshBL;
    private Transform wmeshBR;

    private void Start()
    {
        wcol = GetComponentInChildren<WheelCollider>();
        wmeshL = transform.Find("Wheel_MeshL");
        wmeshR = transform.Find("Wheel_MeshR");
        wmeshBL = transform.Find("Wheel_MeshBL");
        wmeshBR = transform.Find("Wheel_MeshBR");
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
        wmeshL.transform.position = pos;
        wmeshR.transform.position = pos;
        wmeshBL.transform.position = pos;
        wmeshBR.transform.position = pos;
        wmeshL.transform.rotation = rot *= Quaternion.Euler(0, 0, 0);
        wmeshR.transform.rotation = rot *= Quaternion.Euler(0, 90, 0);
        wmeshBL.transform.rotation = rot *= Quaternion.Euler(0, 180, 0);
        wmeshBR.transform.rotation = rot *= Quaternion.Euler(0, 270, 0);
    }
}
