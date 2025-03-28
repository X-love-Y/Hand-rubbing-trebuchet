using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;//小车到相机得偏移量
    [SerializeField] private Transform target;// 目标对象
    [SerializeField] private float translatSpeed;// 平移速度
    [SerializeField] private float rotationSpeed;// 旋转速度
    [SerializeField] private float pitchAngle; // 摄像机的俯仰角度

    private void FixedUpdate()
    {
        HandleTranslation();//处理摄像机的平移
        HandleRotation();//处理摄像机的旋转
    }

    private void HandleTranslation()
    {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translatSpeed * Time.deltaTime);
    }
    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        Quaternion pitchRotation = Quaternion.Euler(pitchAngle, 0, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation * pitchRotation, rotationSpeed * Time.deltaTime);
    }

}
