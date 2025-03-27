using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CombinationChecker : MonoBehaviour
{
    private ObjInf thisPart;
    private AnthorPointSetting setting;
    public bool CheckOver = false;//防止一个相同物体触发多个锚点的显现
    private ObjInf AuchorInf;
    void Start()
    {
        thisPart = GetComponent<ObjInf>();
    }

    void OnTriggerStay(Collider other)
    {
        ObjInf otherPart = other.GetComponent<ObjInf>();
        if (otherPart == null || otherPart.isConnected) //防空 防止相同组件已经拼上但是周围还有一个一样的组件再次检测
            return;

        // 检查部件类型是否匹配（例如：轮子只能连接到底座）
        if (otherPart.partType == thisPart.partType && !CheckOver)
        {
            Debug.Log(2);
            setting = otherPart.GetComponent<AnthorPointSetting>();
            setting.BeChecked();//来点视觉效果
            AuchorInf = otherPart.GetComponent<ObjInf>();
            CheckOver = true;
        }
        if (!GetComponent<Draggable>().isDragging && other.GetComponent<ObjInf>() == AuchorInf)//放开物体时才进行拼装
                SnapParts(otherPart.transform);


        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ObjInf>().partType == thisPart.partType)
            other.GetComponent<AnthorPointSetting>().OutCheck();

    }

    void SnapParts(Transform targetConnectPoint)
        //拼接之后禁用一切物理行为和拖拽
    {
        // 禁用物理和拖拽
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Draggable>().enabled = false;
        thisPart.isConnected = true;

        // 对齐到目标连接点
        transform.position = targetConnectPoint.position;
        transform.rotation = targetConnectPoint.rotation;
        targetConnectPoint.GetComponent<AnthorPointSetting>().CombinationOver();

        // 通知管理器更新进度
        CatapultManager.Instance.CheckCompletion();
    }
}
