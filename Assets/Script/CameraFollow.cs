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
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

}
