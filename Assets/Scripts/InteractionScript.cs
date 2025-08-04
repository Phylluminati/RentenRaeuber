using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class InteractionScript : MonoBehaviour
{
    public GameObject E2Interact;
    GameObject cube;
    [SerializeField]
    private DialogueRunner dialogueRunner;
    [SerializeField]
    private GameObject portrait;


    void onInteract()
    {
        //

    }

    private void OnTriggerEnter(Collider other)
    {
        print("Trigger Enter");
        print(other);
        cube = other.gameObject;
        if (cube.tag == "Item")
        {
            if (cube.GetComponent<Interactee>())
            {
                //This method is being used to force initiate dialogue without player input, if the Item Object has an Interactee script to point to
                dialogueRunner.StartDialogue(cube.GetComponent<Interactee>().dialogue);
            }
            //Destroy(cube);

        }
        if (cube.tag == "Interactable" && !E2Interact.activeSelf)
        {
            E2Interact.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        E2Interact.SetActive(false);
        print("Trigger Exit");
    }
    void Start()
    {
        //dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && E2Interact.activeSelf && cube.tag == "Interactable" && !dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue(cube.GetComponent<Interactee>().dialogue);

        }

    }

    //New Change Method, using an Array, instead of multiple public variables. Should be WAYYYY more streamlined and Modular 
    // (Previous System only allowed 3 different portraits, now Infinite Portraits for conversations are possible, yayyyyy)
    [YarnCommand("change")]
    public void ChangePortrait(int number)
    {
        Image image = portrait.GetComponent<Image>();
        if (cube.GetComponent<Interactee>().spriteList[number] != null)
        {
            image.enabled = true;
            image.sprite = cube.GetComponent<Interactee>().spriteList[number];
        }
        else
        {
            image.enabled = false;
            return;
        }
    }

    [YarnCommand("DisableFlag")]
    public void DisableInteractionFlag()
    {
        //The way the interactionsystem is coded, there is a problem with objects that are being deactivated at the end of their conversation
        //Since the InteractionScript Object isn't leaving the previous objects hitbox (since it has been deactivated) the option of starting a dialog is still there even if the object isn't
        //This often leads to soft-locking, due to a conversation starting with no object there. So this is a method to call on mid conversation to disable the interaction flag.

        E2Interact.SetActive(false);
    }

    // OBSOLETE METHODS FROM PREVIOUS PORTRAIT CHANGING WAY, KEPT HERE IN CASE WE NEED TO REVERT DUE TO SOMETHING



    /*[YarnCommand("change1")]
    public void ChangePortrait()
    {


        Image image = portrait.GetComponent<Image>();
        if (cube.GetComponent<Interactee>().portrait != null)
        {
            image.enabled = true;
            image.sprite = cube.GetComponent<Interactee>().portrait;
        }
        else
        {
            image.enabled = false;
        }
    }
    [YarnCommand("change2")]
    public void ChangePortrait2()
    {
        Image image = portrait.GetComponent<Image>();
        if (cube.GetComponent<Interactee>().altPortrait1 != null)
        {
            image.enabled = true;
            image.sprite = cube.GetComponent<Interactee>().altPortrait1;
        }
        else
        {
            image.enabled = false;
        }

    }
    [YarnCommand("change3")]
    public void ChangePortrait3()
    {
        Image image = portrait.GetComponent<Image>();
        if (cube.GetComponent<Interactee>().altPortrait2 != null)
        {
            image.enabled = true;
            image.sprite = cube.GetComponent<Interactee>().altPortrait2;
        }
         else
        {
            image.enabled = false;
        }

    }*/
}
