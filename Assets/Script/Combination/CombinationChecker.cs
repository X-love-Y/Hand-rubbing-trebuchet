using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CombinationChecker : MonoBehaviour
{
    private ObjInf thisPart;
    private AnthorPointSetting setting;
    public bool CheckOver = false;//��ֹһ����ͬ���崥�����ê�������
    private ObjInf AuchorInf;
    void Start()
    {
        thisPart = GetComponent<ObjInf>();
    }

    void OnTriggerStay(Collider other)
    {
        ObjInf otherPart = other.GetComponent<ObjInf>();
        if (otherPart == null || otherPart.isConnected) //���� ��ֹ��ͬ����Ѿ�ƴ�ϵ�����Χ����һ��һ��������ٴμ��
            return;

        // ��鲿�������Ƿ�ƥ�䣨���磺����ֻ�����ӵ�������
        if (otherPart.partType == thisPart.partType && !CheckOver && otherPart.CompareTag("Author"))
        {
            setting = otherPart.GetComponent<AnthorPointSetting>();
            setting.BeChecked();//�����Ӿ�Ч��
            AuchorInf = otherPart.GetComponent<ObjInf>();
            CheckOver = true;
        }
        if (!GetComponent<Draggable>().isDragging && other.GetComponent<ObjInf>() == AuchorInf)//�ſ�����ʱ�Ž���ƴװ
                SnapParts(otherPart.transform);


        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Author") && other.GetComponent<ObjInf>().partType == thisPart.partType)
        {
            other.GetComponent<AnthorPointSetting>().OutCheck();
            CheckOver = false;
        }
        else { }

    }

    void SnapParts(Transform targetConnectPoint)
        //ƴ��֮�����һ��������Ϊ����ק
    {
        // �����������ק
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Draggable>().enabled = false;
        thisPart.isConnected = true;

        // ���뵽Ŀ�����ӵ�
        transform.position = targetConnectPoint.position;
        transform.rotation = targetConnectPoint.rotation;
        targetConnectPoint.GetComponent<AnthorPointSetting>().CombinationOver();

        // ֪ͨ���������½���
        CatapultManager.Instance.CheckCompletion();
    }
}
