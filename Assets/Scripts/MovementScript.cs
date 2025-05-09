using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f;
    public bool dialogueFreeze = false;
    

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if(dialogueFreeze == false)
        {
            UnityEngine.Vector3 direction = new UnityEngine.Vector3(horizontal, 0f, vertical).normalized;
        

            if(direction.magnitude >= 0.1f)
            {
                controller.Move(direction * speed * Time.deltaTime);
    
            
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

