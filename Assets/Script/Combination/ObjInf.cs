using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjInf : MonoBehaviour
{
    [Header("部件属性")]
    public string partType; // 部件类型（如 "Wheel", "Base"）

    [HideInInspector]
    public bool isConnected = false; // 是否已组合
}
