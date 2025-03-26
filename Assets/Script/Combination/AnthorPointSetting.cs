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
    public bool s = false;
    void Start()
    {
        InitializeMaterial();
        SetAlpha(0f);
    }
    private void Update()
    {
        if (s)
            Debug.Log(rend.material.color);
    }
    private void InitializeMaterial()
    //材质不发挥作用的原因是直接修改rend.material.color会动态创建新的材质实例，
    //OutCheck中通过rend.material = originalMaterial强行恢复原始材质，导致透明度修改被覆盖。
    {
        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;

        // 创建材质实例，避免修改原始材质
        instanceMaterial = new Material(originalMaterial);
        rend.material = instanceMaterial;
        originalColor = instanceMaterial.color;
    }
    public void BeChecked()
    //相同物件进入检测范围后触发
    {
        if (!isColorChanged) 
        {
            Debug.Log(22121212);
            SetAlpha(1f);
            instanceMaterial.color = Color.red; // 修改实例材质
            isColorChanged = true;
        }
    }

    public void OutCheck()
    //相同物件离开检测范围后触发
    {
        SetAlpha(0f);
        instanceMaterial.color = originalColor; // 恢复颜色到初始状态
        rend.material.color = instanceMaterial.color;
        isColorChanged = false;
    }

    public void CombinationOver()//检测并移动后直接把锚点删了
    {
        Destroy(gameObject);
    }
    private void SetAlpha(float alpha)
        //本身color不让直接修改a值，所以只能拐弯抹角的来
        //我初始化设A为0可以，碰撞检测设为1可以，都有效果，我A再设为0也可以，但是没效果是什么意思 你妈的
    {
        Color newColor = instanceMaterial.color;
        newColor.a = alpha; // 改透明度 为0时就消失了
        instanceMaterial.color = newColor;
        rend.material.color = instanceMaterial.color;
    }
}
