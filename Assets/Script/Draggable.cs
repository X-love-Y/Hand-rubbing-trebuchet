using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset; // �����ʱ��ƫ����
    private bool isDragging = false; // �Ƿ�������ק

    void OnMouseDown()
    {
        // ����갴��ʱ������ƫ����
        offset = transform.position - GetMouseAsWorldPosition();
        isDragging = true;
    }

    void OnMouseUp()
    {
        // ����ͷ�ʱֹͣ��ק
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            // ����λ��
            transform.position = GetMouseAsWorldPosition() + offset;
        }
    }

    // �����λ��ת��Ϊ��������
    private Vector3 GetMouseAsWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
