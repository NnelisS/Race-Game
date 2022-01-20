using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfo
{
	public WheelCollider leftWheelCollider;
	public WheelCollider rightWheelCollider;
	public Transform leftWheelMesh;
	public Transform rightWheelMesh;
	public bool motor;
	public bool steering;
}

public class NewCarController : MonoBehaviour
{
	public List<AxleInfo> axleInfos;
	public float maxMotorTorque;
	public float maxSteeringAngle;
	public float brakeTorque;
	public float decelerationForce;

	private float fall = -0.9f;
	private Rigidbody rb;

	public void ApplyLocalPositionToVisuals(AxleInfo axleInfo)
	{
		Vector3 position;
		Quaternion rotation;
		axleInfo.leftWheelCollider.GetWorldPose(out position, out rotation);
		axleInfo.leftWheelMesh.transform.position = position;
		axleInfo.leftWheelMesh.transform.rotation = rotation *= Quaternion.Euler(0, 90, 0);
		axleInfo.rightWheelCollider.GetWorldPose(out position, out rotation);
		axleInfo.rightWheelMesh.transform.position = position;
		axleInfo.rightWheelMesh.transform.rotation = rotation *= Quaternion.Euler(0, 90, 0);
	}

	void FixedUpdate()
	{
		rb = GetComponent<Rigidbody>();
		rb.centerOfMass = new Vector3(0, fall, 0);

		float motor = maxMotorTorque * Input.GetAxis("Vertical");
		float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
		for (int i = 0; i < axleInfos.Count; i++)
		{
			if (axleInfos[i].steering)
			{
				Steering(axleInfos[i], steering);
			}
			if (axleInfos[i].motor)
			{
				Acceleration(axleInfos[i], motor);
			}
			if (Input.GetKey(KeyCode.Space))
			{
				Brake(axleInfos[i]);
			}
			ApplyLocalPositionToVisuals(axleInfos[i]);
		}
	}

	private void Acceleration(AxleInfo axleInfo, float motor)
	{
		if (motor != 0f)
		{
			axleInfo.leftWheelCollider.brakeTorque = 0;
			axleInfo.rightWheelCollider.brakeTorque = 0;
			axleInfo.leftWheelCollider.motorTorque = motor;
			axleInfo.rightWheelCollider.motorTorque = motor;
		}
		else
		{
			Deceleration(axleInfo);
		}
	}

	private void Deceleration(AxleInfo axleInfo)
	{
		axleInfo.leftWheelCollider.brakeTorque = decelerationForce;
		axleInfo.rightWheelCollider.brakeTorque = decelerationForce;
	}

	private void Steering(AxleInfo axleInfo, float steering)
	{
		axleInfo.leftWheelCollider.steerAngle = steering;
		axleInfo.rightWheelCollider.steerAngle = steering;
	}

	private void Brake(AxleInfo axleInfo)
	{
		axleInfo.leftWheelCollider.brakeTorque = brakeTorque;
		axleInfo.rightWheelCollider.brakeTorque = brakeTorque;
	}
}
