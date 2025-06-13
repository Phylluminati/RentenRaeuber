using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class BattleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    [YarnCommand("fight")]
    public void Fight(GameObject Character)
    {
        Debug.Log(Character + " is trying to FIGHT, but is hasn't been coded yet");
    }
    [YarnCommand("special")]
    public void Special(GameObject Character)
    {
        Debug.Log(Character + " is trying to use their SPECIAL, but it hasn't been coded yet");

    }
    [YarnCommand("guard")]
    public void Guard(GameObject Character)
    {
        Debug.Log(Character + " is trying to GUARD, but it hasn't been coded yet");

    }

}
