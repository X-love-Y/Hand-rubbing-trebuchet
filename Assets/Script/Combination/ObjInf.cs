using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjInf : MonoBehaviour
{
    [Header("��������")]
    public string partType; // �������ͣ��� "Wheel", "Base"��

    [HideInInspector]
    public bool isConnected = false; // �Ƿ������
}
