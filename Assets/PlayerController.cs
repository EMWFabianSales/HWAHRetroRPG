using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    Rigidbody2D rb;
    Vector2 RJoyAxis;
    public GameObject[] SpriteList = new GameObject[8];
    public int spriteSet = 0;
    public string playerDir;
    [Space]
    public float walkSpeed; 
    [Space]
    public bool canInteract;
    public List<Transform> InteractableObjects;
    public bool isInteracting;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer(){
        if(!isInteracting){
            //North
            if(RJoyAxis.y >= 0.5f){
                rb.velocity = new Vector3(0,walkSpeed,0);
                spriteSet = 4;
                playerDir = "N";
            }

            //East
            else if(RJoyAxis.x >= 0.5f){
                rb.velocity = new Vector3(walkSpeed,0,0);
                spriteSet = 5;
                playerDir = "E";
            }

            //South
            else if(RJoyAxis.y <= -0.5f){
                rb.velocity = new Vector3(0,-walkSpeed,0);
                spriteSet = 6;
                playerDir = "S";
            }

            //West
            else if(RJoyAxis.x <= -0.5f){
                rb.velocity = new Vector3(-walkSpeed,0,0);
                spriteSet = 7;
                playerDir = "W";
            }

            //Idle
            else if(RJoyAxis.x == 0 && RJoyAxis.y == 0 && RJoyAxis.x > -0.5f && RJoyAxis.x < 0.5f && RJoyAxis.y > -0.5f && RJoyAxis.x < 0.5f){
                rb.velocity = new Vector3(0,0,0);
                if(spriteSet == 4){
                    spriteSet = 0;
                }
                else if ( spriteSet == 5){
                    spriteSet = 1;
                }
                else if (spriteSet == 6){
                    spriteSet = 2;
                }
                else if (spriteSet == 7){
                    spriteSet = 3;
                }else{
                    return;
                }
            }
        }

        
    

        for(int i = 0; i < SpriteList.Length; i++){
            if(i != spriteSet){
                SpriteList[i].SetActive(false);
            }
            if(i == spriteSet && !SpriteList[i].activeInHierarchy){
                SpriteList[i].SetActive(true);
            }
        }
    }

    //Interaction
    public void Interact(){
        //if player can interact
        if(canInteract && !isInteracting){
            for (int i = 0; i < InteractableObjects.Count; i++){
                //if interactable Object is North
                if(InteractableObjects[i].position.y > transform.position.y && InteractableObjects[i].position.x !> transform.position.x -0.2f && InteractableObjects[i].position.x !< transform.position.x + 0.2f && playerDir == "N"){
                    //Debug.Log("Interaction Initiated North");
                    isInteracting = true;
                    dialogueHandler.instance.parseDialogueData(InteractableObjects[i].gameObject.GetComponent<Interactable>().dialogues);
                    dialogueHandler.instance.InitiateDiologue();
                }
                //if interactable Object is East
                else if(InteractableObjects[i].position.x > transform.position.x && InteractableObjects[i].position.y !> transform.position.y -0.2f && InteractableObjects[i].position.y !< transform.position.y + 0.2f && playerDir == "E"){
                    //Debug.Log("Interaction Initiated East");
                    isInteracting = true;
                    dialogueHandler.instance.parseDialogueData(InteractableObjects[i].gameObject.GetComponent<Interactable>().dialogues);
                    dialogueHandler.instance.InitiateDiologue();
                }
                //if interactable Object is South
                else if(InteractableObjects[i].position.y < transform.position.x && InteractableObjects[i].position.x !> transform.position.x -0.2f && InteractableObjects[i].position.x !< transform.position.x + 0.2f && playerDir == "S"){
                    //Debug.Log("Interaction Initiated South");
                    isInteracting = true;
                    dialogueHandler.instance.parseDialogueData(InteractableObjects[i].gameObject.GetComponent<Interactable>().dialogues);
                    dialogueHandler.instance.InitiateDiologue();
                }
                //if interactable Object is West
                else if(InteractableObjects[i].position.x < transform.position.x && InteractableObjects[i].position.y !> transform.position.y -0.2f && InteractableObjects[i].position.y !< transform.position.y + 0.2f && playerDir == "W"){
                    //Debug.Log("Interaction Initiated West");
                    isInteracting = true;
                    dialogueHandler.instance.parseDialogueData(InteractableObjects[i].gameObject.GetComponent<Interactable>().dialogues);
                    dialogueHandler.instance.InitiateDiologue();
                }else{
                    //Debug.Log("Player Not Facing Interactable Object");
                }
            }
        }
        else if(canInteract && isInteracting){
            dialogueHandler.instance.nextDialogue();
        }
    }


    public void GetControllerAxis(InputAction.CallbackContext ctx){
        RJoyAxis = ctx.ReadValue<Vector2>();
    }

    public void GetInteractButtonState(InputAction.CallbackContext ctx){
        if(ctx.started){
            Interact();
        }
    }

    //Collisions
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.GetComponent<Interactable>() != null){
            canInteract = true;
            InteractableObjects.Add(collision.transform);

        }else{
            return;
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.GetComponent<Interactable>() != null){
            canInteract = false;
            InteractableObjects.Remove(collision.transform);
        }else{
            return;
        }
    }
}
