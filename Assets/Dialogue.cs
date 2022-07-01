using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dialogue
{
    public string speach;
    public bool isPrompt;

    [HideInInspector]
    public bool showPromptResponses;
    public List<DialoguePrompt> PromptOptions;
}
