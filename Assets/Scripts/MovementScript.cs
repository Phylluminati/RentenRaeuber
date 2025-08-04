using System.Collections;
using System.Collections.Generic;
using System.Numerics;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f;
    public bool dialogueFreeze = false;
    [SerializeField]
    GameObject CharacterGFX;
    float xGFX;
    Animator myAnimator;

    void Start()
    {
        myAnimator = this.GetComponentInChildren<Animator>();
        xGFX = CharacterGFX.transform.localScale.x;
        //Doing this with a tempory Variable, so this variable will only have to be defined once at start, I think that should optimize the runtime abit (not that the game really needs that though)
    }


    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if(dialogueFreeze == false)
        {
            UnityEngine.Vector3 direction = new UnityEngine.Vector3(horizontal, 0f, vertical).normalized;
            //UnityEngine.Vector3 lTemp = transform.localScale;
            //lTemp.x = direction;
            if (horizontal != 0)
            {

            CharacterGFX.transform.localScale = new UnityEngine.Vector3(-horizontal, 1f, 1f);
            }
            myAnimator.SetBool("isWalking", direction.magnitude >= 0.1f);


            if (direction.magnitude >= 0.1f)
            {
                controller.Move(xGFX * direction * speed * Time.deltaTime);
            }
        }
    }
    public void Freeze() 
    {
        dialogueFreeze = true;
    }
    public void Unfreeze()
    {
        dialogueFreeze = false;
    }
}

