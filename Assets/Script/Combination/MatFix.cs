using UnityEngine;

public class MatFix : MonoBehaviour
    //����ű��������޸��ض��ӽ��µ�ģ����ʧ���壬ͨ���޸��������д�빦�����޸�����
    //��֮��������һ�������������������д���������໥���ǣ�����Ŀ�����
{
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Material material = renderer.material;
            material.SetInt("_ZWrite", 1); // �������д��
        }
    }
}