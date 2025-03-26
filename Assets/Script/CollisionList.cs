using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionList : MonoBehaviour
{
    public List<GameObject> childs = new List<GameObject>();
    Transform parentTransform;

    private void Start()
    {
        parentTransform = transform;
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i);
            childs.Add(childTransform.gameObject);
        }
        foreach (var child in childs)
        {
            child.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void IsChecked()
    {
        foreach (var child in childs)
        {
            child.GetComponent<Rigidbody>().isKinematic = false;
            Rigidbody rd = GetComponent<Rigidbody>();
            rd.isKinematic = false;
        }
    }

}
