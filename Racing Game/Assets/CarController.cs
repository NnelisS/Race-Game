using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class CarController : MonoBehaviour
{
    public Transform frontRightTransform;
    public Transform frontLeftTransform;

    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    public float acceleration = 500f;
    public float breakingForce = 300f;

    private float currentAcceleratiom = 0f;
    private float currenBreakForce = 0f;

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");



        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);

            // update wheel meshes
            UpdateWheel(axleInfo.leftWheel, frontLeftTransform);
            UpdateWheel(axleInfo.rightWheel, frontRightTransform);
        }
    }

    private void UpdateWheel(WheelCollider col, Transform trans)
    {
        // get wheel collider
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        //set wheel transform state
        trans.position = position;
        trans.rotation = rotation *= Quaternion.Euler(0, 90, 0);
    }
}