using UnityEngine;

public class MatFix : MonoBehaviour
    //这个脚本是用于修复特定视角下的模型消失物体，通过修改设置深度写入功能来修复物体
    //随之而来的另一个问题是两个开启深度写入的物体会相互覆盖，里面的看不见
{
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Material material = renderer.material;
            material.SetInt("_ZWrite", 1); // 开启深度写入
        }
    }
}