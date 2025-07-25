using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Yarn;

public class Interactee : MonoBehaviour
{
    public string dialogue;

    //Previously used for portrait changes, replaced by List<Sprite> spriteList

    /*public Sprite portrait;
    public Sprite altPortrait1;
    public Sprite altPortrait2;*/

    public List<Sprite> spriteList;
    //private Sprite portrait;

    // Start is called before the first frame update
    void Start()
    {
            spriteList.Add(null);
        

    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
