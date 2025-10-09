using UnityEngine;
using System;
using System.Collections.Generic;

public class CarPhysicsController : MonoBehaviour
{
    public enum Axel
    {
        Front,
        Rear
    }
    
    public enum ControlMode
    {
        Keyboard,
        Buttons
    };

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public Axel axel;
    }

    public ControlMode control;
    
    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 20.0f;

    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 15.0f;
    
    float steerInput;

    public List<Wheel> wheels;

    float moveInput;

    private Rigidbody carRb;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInputs();
        AnimateWheels();
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    public void MoveInput(float input)
    {
        moveInput = input;
    }
    
    public void SteerInput(float input)
    {
        steerInput = input;
    }

    void GetInputs()
    {
        if(control == ControlMode.Keyboard)
        {
            moveInput = Input.GetAxis("Vertical");
            steerInput = Input.GetAxis("Horizontal");
        }
    }
    
    
    void Move()
    {
        foreach(var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * maxAcceleration;
        }
    }
    
    
    void Steer()
    {
        foreach(var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }
    
    
    void Brake()
    {
        if (Input.GetKey(KeyCode.Space) || moveInput == 0)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration;
            }
            
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
            
        }
    }
    
    
    void AnimateWheels()
    {
        foreach(var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }
    
    void OnDrawGizmos()
    {
        if (carRb != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(carRb.worldCenterOfMass, 0.2f);
        }
    }


}
