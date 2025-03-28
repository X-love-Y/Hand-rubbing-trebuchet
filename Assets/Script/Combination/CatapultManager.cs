using NodeCanvas.DialogueTrees;
using UnityEngine;

public class CatapultManager : MonoBehaviour
{
    public static CatapultManager Instance;

    [SerializeField] DialogueTreeController dialogueTree;
    [SerializeField] GameObject Sceneobj;
    [Header("�ܲ�������")]
    public int totalParts;
    private int connectedParts = 0;

    void Awake()
    {
        Instance = this;
        Sceneobj.SetActive(false);
    }

    public void CheckCompletion()
    {
        connectedParts++;
        if (connectedParts == totalParts)
        {
            Invoke("Dialogue", 1f);
            Sceneobj.SetActive(true);
        }
    }

    private void Dialogue()
    {
        dialogueTree.StartDialogue();
        // ��������¼�
    }
}