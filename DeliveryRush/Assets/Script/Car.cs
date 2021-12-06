using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public enum ControlType { HumanInput, AI, Static }
    public ControlType controlType = ControlType.HumanInput;

    public Transform centerOfMass;
  
    public float motorTorque = 1200f;
    public float maxSteer = 20f;
    public float bensin { get; set; } = 500f;
    public float fNos { get; set; } = 500f;
    public float MultSpeed { get; set; } = 0f;
    public float MsgMultSpeed { get; set; } = 0f;


    public GameObject tail;
    private MeshRenderer[] tails;

    public float Steer { get; set; }
    public float Throttle { get; set; }
    Vector3 vel;
    private bool isNgisiBensin = false;

    private Rigidbody _rigidbody;
    private Wheel [] wheels;

    private float MaxBensin = 500f;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "bensin")
        {
            isNgisiBensin = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "bensin")
            isNgisiBensin = false;
    }

    void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
        tails = tail.GetComponentsInChildren<MeshRenderer>();
        bensin = 400;
        //FindObjectOfType<AudioManager>().PlaySound("CarEngine");
    }

    // Update is called once per frame
    void Update()
    {
        vel = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);

        //FindObjectOfType<AudioManager>().SetAudioPitch("CarEngine", 1f + (Mathf.Abs(vel.z)/10f));

        //Controll Type
        if (controlType == ControlType.HumanInput)
        {
            Steer = GameManager.Instance.InputController.SteerInput;
            Throttle = GameManager.Instance.InputController.ThrottleInput;
        }

        //Nos goes brrrr
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            fNos -= 2f;
            if (fNos <= 0f) fNos = 0f;
            if (fNos > 10)
                MultSpeed = 3f;
            else
                MultSpeed = 1f;
        }
        else
        {
            fNos += 0.2f;
            if (fNos >= 500f) fNos = 500f;
            MultSpeed = 1f;
        }

        //Isi Bensin
        if (isNgisiBensin)
        {
            if (Input.GetKey(KeyCode.F))
            {
                bensin += 1.5f;
                if (bensin > MaxBensin) bensin = MaxBensin;
            }
        }
        else
        {
            bensin-= Mathf.Abs(Throttle * 0.05f);
            if(vel.z > 12) bensin-= 0.05f;
            if (bensin < 0.0f) bensin = 0.0f;
              //Debug.Log("vel.z = " + vel.z);
        }


        if (vel.z > 1.0f && Throttle < -0.1f)
        {
            tails[0].enabled = true;
            tails[1].enabled = true;
        }
        else
        {
            tails[0].enabled = false;
            tails[1].enabled = false;
        }

        if(vel.z < -1.0f && Throttle < -0.1f)
        {
            tails[2].enabled = true;
            tails[3].enabled = true;
        }
        else
        {
            tails[2].enabled = false;
            tails[3].enabled = false;
        }


        foreach (var wheel in wheels)
        {
            wheel.SteerAngle = Steer * maxSteer;
            if (bensin > 0.2f) { 
                wheel.Torque = Throttle * motorTorque * MultSpeed * MsgMultSpeed;
            }
            else
            {
                if (Mathf.Abs(vel.z) > 1.0f)
                    wheel.Torque = -0.1f * vel.z * motorTorque * MultSpeed * MsgMultSpeed;
                else
                    wheel.Torque = 0.0f;
            }
        }
        
    }
}
