using UnityEngine;

public class CatapultManager : MonoBehaviour
{
    public static CatapultManager Instance;

    [Header("�ܲ�������")]
    public int totalParts = 5;
    private int connectedParts = 0;

    void Awake()
    {
        Instance = this;
    }

    public void CheckCompletion()
    {
        connectedParts++;
        if (connectedParts == totalParts)
        {
            Debug.Log("Ͷʯ����װ��ɣ�");
            // ��������¼����粥�Ŷ�����
        }
    }
}