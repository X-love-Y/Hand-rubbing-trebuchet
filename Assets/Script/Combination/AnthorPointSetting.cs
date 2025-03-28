using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnthorPointSetting : MonoBehaviour
    //作为锚点物件的设置
{
    private Material originalMaterial;
    private Material instanceMaterial; // 使用材质实例
    private Renderer rend;
    private Color originalColor;
    public bool isColorChanged = false;
    private List<Renderer> renderers = new List<Renderer>();
    private bool isSingle;
    void Start()
    {
        InitializeMaterial();
    }
    private void InitializeMaterial()
    //材质不发挥作用的原因是直接修改rend.material.color会动态创建新的材质实例，
    //OutCheck中通过rend.material = originalMaterial强行恢复原始材质，导致透明度修改被覆盖。
    //一定要注意的一点是rend.material = instanceMaterial;这样的，是让instanceMaterial成为rend.material，以后在instanceMaterial中所有的修改都是对rend.material的修改
    //他们已经是同一个概念了，但是如果你再直接修改rend.material的值，就又会创建一个新的material副本，这时候instanceMaterial就不是rend.material，在instanceMaterial的修改也不再发挥作用
    {
        rend = GetComponent<Renderer>();
        // 如果自身没有Renderer组件，则从子物体中查找
        if (rend == null)
        {
            isSingle = false;
            foreach (Transform child in transform)
            {
                Renderer childRenderer = child.GetComponent<Renderer>();
                if (childRenderer != null)
                {
                    renderers.Add(childRenderer);
                }
            }
            if (renderers.Count > 0)
            {
                // 所有子物体共享同一个材质实例
                originalMaterial = renderers[0].material;
                instanceMaterial = new Material(originalMaterial);
                foreach (Renderer r in renderers)
                {
                    r.material = instanceMaterial; // 统一替换为实例材质
                }
                originalColor = instanceMaterial.color;
            }
        }
        else 
        {
            isSingle = true;
            originalMaterial = rend.material;

            // 创建材质实例，避免修改原始材质
            instanceMaterial = new Material(originalMaterial);
            rend.material = instanceMaterial;
            originalColor = instanceMaterial.color;
        }
    }
    public void BeChecked()
    //相同物件进入检测范围后触发
    {
        if (!isColorChanged) 
        {
            instanceMaterial.color = Color.red; // 修改实例材质
            isColorChanged = true;
        }
    }

    public void OutCheck()
    //相同物件离开检测范围后触发
    {
            instanceMaterial.color = originalColor; // 恢复颜色到初始状态
            isColorChanged = false;
    }

    public void CombinationOver()//检测并移动后直接把锚点删了
    {
        Destroy(gameObject);
    }

}
