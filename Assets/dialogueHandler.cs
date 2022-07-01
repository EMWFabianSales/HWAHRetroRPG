using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueHandler : MonoBehaviour
{   
    public static dialogueHandler instance;
    
    public List<Dialogue> RecievedDialogue;
    public int currentDialogue = 0;
    void Start(){
        instance = this;
    }

    public void parseDialogueData(List<Dialogue> Diologues){
        //Recieve Dialogue from Interactable Object
        RecievedDialogue = Diologues;
    }

    public void InitiateDiologue(){
        //reset Dialogue
        currentDialogue = 0;
        
        //Open Dialogue Box
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().SetText(RecievedDialogue[currentDialogue].speach);
    }

    public void nextDialogue(){
        if(currentDialogue < RecievedDialogue.Count - 1){
        currentDialogue += 1;
        transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().SetText(RecievedDialogue[currentDialogue].speach);
        } 
        else {
            EndDialogue();
        }
    }

    public void EndDialogue(){
        transform.GetChild(0).gameObject.SetActive(false);
        PlayerController.instance.isInteracting = false;
    }
}
