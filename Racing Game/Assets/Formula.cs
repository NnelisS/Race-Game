using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Formula : MonoBehaviour
{
    [Header("Car Info")]
    public Transform gravityTarget;
    public Vector3 currentVelocity;
    public GameObject steeringWheel;
    
    public float power = 15000f;
    public float torque = 500f;
    public float gravity = 9.81f;
    public float breakForce = 1f;

    [Header("UI")]
    public TextMeshProUGUI speedLabel;
    public float maxSpeed = 0.0f;
    private float speed = 0.0f;

    [Header("Car Cams")]
    public GameObject cameraOne;
    public GameObject cameraTwo;
    public GameObject cameraThree;

    private float fall = -0.2f;

    [Header("Car Orientation")]
    public bool autoOrient = false;
    public float autoOrientSpeed = 1f;

    private float horInput;
    private float verInput;
    private float brakeInput;
    private float steerAngle;

    public bool camOne;
    public bool camTwo;
    public bool camThree;

    [Header("Wheels")]
    public Wheel[] wheels;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
/*        SteeringWheel();*/
        Cursor.visible = false;

        speed = rb.velocity.magnitude * 3.0f;
        speedLabel.text = ((int)speed + " km/h");

        ProcessInput();
        Vector3 diff = transform.position - gravityTarget.position;
        if (autoOrient) { AutoOrient(-diff); }

        CamChange();
    }

    public void CamChange()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            if (camOne == true)
            {
                cameraThree.SetActive(false);
                cameraTwo.SetActive(false);
                cameraOne.SetActive(true);
                
                camOne = false;
                camTwo = true;
                camThree = false;
            }
            else if (camTwo == true)
            {
                cameraThree.SetActive(false);
                cameraTwo.SetActive(true);
                cameraOne.SetActive(false);
                camOne = false;
                camTwo = false;
                camThree = true;
            }
            else if (camThree == true)
            {
                cameraTwo.SetActive(false);
                cameraThree.SetActive(true);
                cameraOne.SetActive(false);
                camOne = true;
                camTwo = false;
                camThree = false;
            }
            Debug.Log($" {camOne} {cameraTwo} {cameraThree}");
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            cameraThree.SetActive(false);
            cameraTwo.SetActive(false);
            cameraOne.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            cameraOne.SetActive(false);
            cameraThree.SetActive(false);
            cameraTwo.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            cameraTwo.SetActive(false);
            cameraThree.SetActive(true);
            cameraOne.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        rb.centerOfMass = new Vector3(0, fall, 0);

        ProcessForces();
        ProcessGravity();
    }

/*    public void SteeringWheel()
    {
        //steeringWheel.transform.rotation = Quaternion.Euler(steeringWheel.transform.rotation.x * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.A))
        {
            Quaternion a = Quaternion.Euler(steeringWheel.transform.rotation.x, -90, 0);
            Quaternion b = Quaternion.Euler(45, -90, 0);
            steeringWheel.transform.rotation = Quaternion.Slerp(a, b, 1);
        }
    }*/

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
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.fixedDeltaTime * 1.0f);
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
