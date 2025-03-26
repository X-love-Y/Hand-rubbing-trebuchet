using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarget : MonoBehaviour
{

    //private void OnTriggerEnter(Collider collision)
    //{
    //    Debug.Log(1);
    //    // 检查碰撞物体是否为 Stone
    //    if (collision.gameObject.name == "Stone")
    //    {
    //        //Debug.Log("Stone collided with Target");
    //        // 遍历 Target 的所有子物体
    //        foreach (Transform child in transform)
    //        {
    //            // 确保子物体没有 Rigidbody 组件
    //            if (child.GetComponent<Rigidbody>() == true)
    //            {
    //                // 添加 Rigidbody 组件
    //                Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
    //                // 设置 Rigidbody 的一些属性
    //                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    //                rb.isKinematic = false; // 确保子物体受物理引擎影响
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
    //            //添加刚体组件，模拟下落效果
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