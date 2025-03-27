using NodeCanvas.DialogueTrees;
using UnityEngine;

public class CatapultManager : MonoBehaviour
{
    public static CatapultManager Instance;

    [SerializeField] DialogueTreeController dialogueTree;
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
            dialogueTree.StartDialogue();
            // 触发完成事件
        }
    }
}