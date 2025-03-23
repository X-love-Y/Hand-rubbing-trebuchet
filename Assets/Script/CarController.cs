using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelConllider;
    [SerializeField] private WheelCollider frontRightWheelConllider;
    [SerializeField] private WheelCollider BehindLeftWheelConllider;
    [SerializeField] private WheelCollider BehindRightWheelConllider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform BehindLeftWheelTransform;
    [SerializeField] private Transform BehindRightWheelTransform;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheelConllider.motorTorque = verticalInput * motorForce;
        frontRightWheelConllider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        if (isBreaking)
        {
            ApplyBreaking();
        }
        else
        {
            // 如果没有刹车，清除刹车力
            frontRightWheelConllider.brakeTorque = 0f;
            frontLeftWheelConllider.brakeTorque = 0f;
            BehindLeftWheelConllider.brakeTorque = 0f;
            BehindRightWheelConllider.brakeTorque = 0f;
        }
    }

    private void ApplyBreaking()
    {
        frontRightWheelConllider.brakeTorque = currentbreakForce;
        frontLeftWheelConllider.brakeTorque = currentbreakForce;
        BehindLeftWheelConllider.brakeTorque = currentbreakForce;
        BehindRightWheelConllider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelConllider.steerAngle = currentSteerAngle;
        frontRightWheelConllider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheels(frontLeftWheelConllider, frontLeftWheelTransform);
        UpdateSingleWheels(frontRightWheelConllider, frontRightWheelTransform);
        UpdateSingleWheels(BehindRightWheelConllider, BehindRightWheelTransform);
        UpdateSingleWheels(BehindLeftWheelConllider, BehindLeftWheelTransform);
    }

    private void UpdateSingleWheels(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
