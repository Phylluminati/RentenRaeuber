using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class InteractionScript : MonoBehaviour
{
    public GameObject E2Interact;
    GameObject cube;
    private DialogueRunner dialogueRunner;
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

            Destroy(cube);

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
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        portrait = GameObject.Find("Portrait");
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && E2Interact.activeSelf && cube.tag == "Interactable" && !dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue(cube.GetComponent<Interactee>().dialogue);

        }

    }
    [YarnCommand("change1")]
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

    }
}
