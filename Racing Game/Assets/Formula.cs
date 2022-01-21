using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formula : MonoBehaviour
{
    public Transform gravityTarget;

    public float power = 15000f;
    public float torque = 500f;
    public float gravity = 9.81f;
    public float breakForce = 100000f;

    private float fall = -0.2f;

    public bool autoOrient = false;
    public float autoOrientSpeed = 1f;

    public Vector3 currentVelocity;

    private float horInput;
    private float verInput;
    private float brakeInput;
    private float steerAngle;

    public Wheel[] wheels;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessInput();
        Vector3 diff = transform.position - gravityTarget.position;
        if (autoOrient) { AutoOrient(-diff); }
    }

    void FixedUpdate()
    {
        rb.centerOfMass = new Vector3(0, fall, 0);

        ProcessForces();
        ProcessGravity();
    }

    void ProcessInput()
    {
        verInput = Input.GetAxis("Vertical");
        horInput = Input.GetAxis("Horizontal");
        brakeInput = Input.GetAxis("Jump");
    }

    void ProcessForces()
    {
        Vector3 force = new Vector3(0f, 0f, verInput * power);
        rb.AddRelativeForce(force);

        /*Vector3 rforce = new Vector3(0f, horInput* torque, 0f);
        rb.AddRelativeTorque(rforce);*/

        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.fixedDeltaTime * 1.5f);
        }

        foreach (Wheel w in wheels)
        {
            w.Steer(horInput);
            w.Accelerate(verInput * power);
            w.UpdatePosition();
/*            w.Brake(breakForce);*/
        }
    }

    void ProcessGravity()
    {
        Vector3 diff = transform.position - gravityTarget.position;
        rb.AddForce(-diff.normalized * gravity * (rb.mass));
    }

    void AutoOrient(Vector3 down)
    {
        Quaternion orientationDirection = Quaternion.FromToRotation(-transform.up, down) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, orientationDirection, autoOrientSpeed * Time.deltaTime);
    }
}
