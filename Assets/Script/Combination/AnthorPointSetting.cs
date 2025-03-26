using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnthorPointSetting : MonoBehaviour
    //��Ϊê�����������
{
    private Material originalMaterial;
    private Material instanceMaterial; // ʹ�ò���ʵ��
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
    //���ʲ��������õ�ԭ����ֱ���޸�rend.material.color�ᶯ̬�����µĲ���ʵ����
    //OutCheck��ͨ��rend.material = originalMaterialǿ�лָ�ԭʼ���ʣ�����͸�����޸ı����ǡ�
    {
        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;

        // ��������ʵ���������޸�ԭʼ����
        instanceMaterial = new Material(originalMaterial);
        rend.material = instanceMaterial;
        originalColor = instanceMaterial.color;
    }
    public void BeChecked()
    //��ͬ��������ⷶΧ�󴥷�
    {
        if (!isColorChanged) 
        {
            Debug.Log(22121212);
            SetAlpha(1f);
            instanceMaterial.color = Color.red; // �޸�ʵ������
            isColorChanged = true;
        }
    }

    public void OutCheck()
    //��ͬ����뿪��ⷶΧ�󴥷�
    {
        SetAlpha(0f);
        instanceMaterial.color = originalColor; // �ָ���ɫ����ʼ״̬
        rend.material.color = instanceMaterial.color;
        isColorChanged = false;
    }

    public void CombinationOver()//��Ⲣ�ƶ���ֱ�Ӱ�ê��ɾ��
    {
        Destroy(gameObject);
    }
    private void SetAlpha(float alpha)
        //����color����ֱ���޸�aֵ������ֻ�ܹ���Ĩ�ǵ���
        //�ҳ�ʼ����AΪ0���ԣ���ײ�����Ϊ1���ԣ�����Ч������A����Ϊ0Ҳ���ԣ�����ûЧ����ʲô��˼ �����
    {
        Color newColor = instanceMaterial.color;
        newColor.a = alpha; // ��͸���� Ϊ0ʱ����ʧ��
        instanceMaterial.color = newColor;
        rend.material.color = instanceMaterial.color;
    }
}
