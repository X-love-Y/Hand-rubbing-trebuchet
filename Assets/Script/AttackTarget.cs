using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarget : MonoBehaviour
{

    //private void OnTriggerEnter(Collider collision)
    //{
    //    Debug.Log(1);
    //    // �����ײ�����Ƿ�Ϊ Stone
    //    if (collision.gameObject.name == "Stone")
    //    {
    //        //Debug.Log("Stone collided with Target");
    //        // ���� Target ������������
    //        foreach (Transform child in transform)
    //        {
    //            // ȷ��������û�� Rigidbody ���
    //            if (child.GetComponent<Rigidbody>() == true)
    //            {
    //                // ��� Rigidbody ���
    //                Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
    //                // ���� Rigidbody ��һЩ����
    //                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    //                rb.isKinematic = false; // ȷ������������������Ӱ��
    //            }
    //        }
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.name == "Stone")
    //    {
    //        Transform parent = gameObject.GetComponent<Transform>().parent;
    //        Transform[] target = parent.GetComponentsInChildren<Transform>();
    //        for (int i = 0; i < target.Length; i++)
    //        {
    //            //��Ӹ��������ģ������Ч��
    //            target[i].gameObject.AddComponent<Rigidbody>();
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target")) 
        {
            other.GetComponent<CollisionList>().IsChecked();
        }
    }

}