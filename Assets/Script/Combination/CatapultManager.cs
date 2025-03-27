using UnityEngine;

public class CatapultManager : MonoBehaviour
{
    public static CatapultManager Instance;

    [Header("总部件数量")]
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
            Debug.Log("投石车组装完成！");
            // 触发完成事件（如播放动画）
        }
    }
}