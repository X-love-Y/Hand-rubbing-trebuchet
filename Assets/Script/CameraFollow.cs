using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;//С���������ƫ����
    [SerializeField] private Transform target;// Ŀ�����
    [SerializeField] private float translatSpeed;// ƽ���ٶ�
    [SerializeField] private float rotationSpeed;// ��ת�ٶ�
    [SerializeField] private float pitchAngle; // ������ĸ����Ƕ�

    private void FixedUpdate()
    {
        HandleTranslation();//�����������ƽ��
        HandleRotation();//�������������ת
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
