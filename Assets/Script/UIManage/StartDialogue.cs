using NodeCanvas.DialogueTrees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    private DialogueTreeController DialogueTreeController;
    void Start()
    {
        DialogueTreeController = GetComponent<DialogueTreeController>();
        DialogueTreeController.StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
