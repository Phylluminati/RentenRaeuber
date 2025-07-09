using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class inventoryManager : MonoBehaviour
{
    [SerializeField] GameObject OldBagText;
    [SerializeField] GameObject FashionistaText;
    [SerializeField] GameObject KatGranny;
    [SerializeField] GameObject GameManager;

    private InMemoryVariableStorage variableStorage;
    // Start is called before the first frame update
    void Start()
    {
        variableStorage = FindObjectOfType<InMemoryVariableStorage>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToInventory()
    {
        //variableStorage.TryGetValue("", out );

    }
    

}
