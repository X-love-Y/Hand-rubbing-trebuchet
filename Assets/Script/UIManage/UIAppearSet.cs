using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class UIAppearSet : MonoBehaviour
//想法：canvas放一个图片为父物体的UI，获取鼠标指向，列个计时器，过时间之后把UI传送到这个位置，再调透明度使其出现或者SetActive（）
{
    [Header("UI显示信息")]
    public string objName;
    public string introduceInf;

    // Update is called once per frame
    void Update()
    {
        
    }

   
}

