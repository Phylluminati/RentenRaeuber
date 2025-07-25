using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;

public class BattleManager : MonoBehaviour
{
    public GameObject Enemy;
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
        int currentDamage = Character.GetComponent<Unit>().damage / Enemy.GetComponent<Unit>().guard;
        Enemy.GetComponent<Unit>().currentHP =- currentDamage;
    }
    [YarnCommand("special")]
    public void Special(GameObject Character, GameObject Target)
    {
        Debug.Log(Character + " is trying to use their SPECIAL, but it hasn't been coded yet");
        //Character.GetComponent<Unit>().
    }
    [YarnCommand("guard")]
    public void Guard(GameObject Character)
    {
        Debug.Log(Character + " is trying to GUARD, but it hasn't been coded yet");

    }

}
