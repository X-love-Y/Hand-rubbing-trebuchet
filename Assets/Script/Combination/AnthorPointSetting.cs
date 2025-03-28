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
    private List<Renderer> renderers = new List<Renderer>();
    private bool isSingle;
    void Start()
    {
        InitializeMaterial();
    }
    private void InitializeMaterial()
    //���ʲ��������õ�ԭ����ֱ���޸�rend.material.color�ᶯ̬�����µĲ���ʵ����
    //OutCheck��ͨ��rend.material = originalMaterialǿ�лָ�ԭʼ���ʣ�����͸�����޸ı����ǡ�
    //һ��Ҫע���һ����rend.material = instanceMaterial;�����ģ�����instanceMaterial��Ϊrend.material���Ժ���instanceMaterial�����е��޸Ķ��Ƕ�rend.material���޸�
    //�����Ѿ���ͬһ�������ˣ������������ֱ���޸�rend.material��ֵ�����ֻᴴ��һ���µ�material��������ʱ��instanceMaterial�Ͳ���rend.material����instanceMaterial���޸�Ҳ���ٷ�������
    {
        rend = GetComponent<Renderer>();
        // �������û��Renderer���������������в���
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
                // ���������干��ͬһ������ʵ��
                originalMaterial = renderers[0].material;
                instanceMaterial = new Material(originalMaterial);
                foreach (Renderer r in renderers)
                {
                    r.material = instanceMaterial; // ͳһ�滻Ϊʵ������
                }
                originalColor = instanceMaterial.color;
            }
        }
        else 
        {
            isSingle = true;
            originalMaterial = rend.material;

            // ��������ʵ���������޸�ԭʼ����
            instanceMaterial = new Material(originalMaterial);
            rend.material = instanceMaterial;
            originalColor = instanceMaterial.color;
        }
    }
    public void BeChecked()
    //��ͬ��������ⷶΧ�󴥷�
    {
        if (!isColorChanged) 
        {
            instanceMaterial.color = Color.red; // �޸�ʵ������
            isColorChanged = true;
        }
    }

    public void OutCheck()
    //��ͬ����뿪��ⷶΧ�󴥷�
    {
            instanceMaterial.color = originalColor; // �ָ���ɫ����ʼ״̬
            isColorChanged = false;
    }

    public void CombinationOver()//��Ⲣ�ƶ���ֱ�Ӱ�ê��ɾ��
    {
        Destroy(gameObject);
    }

}
