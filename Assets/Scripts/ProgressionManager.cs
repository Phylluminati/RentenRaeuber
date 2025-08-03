using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ProgressionManager : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefab;
    [SerializeField] GameObject battleParent;
    [SerializeField] GameObject overworldParent;

    [SerializeField] DialogueRunner overworldDialogueRunner;
    [SerializeField] GameObject enemySpawnpoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [YarnCommand("Deactivate")]
    public void Deactivate(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    [YarnCommand("Activate")]
    public void Activate(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    [YarnCommand("Battle")]
    public void InitiateBattle(int enemyPrefabSelection)
    {
        GameObject enemy = enemyPrefab[enemyPrefabSelection];
        overworldParent.SetActive(false);
        battleParent.SetActive(true);
        Instantiate(enemy, enemySpawnpoint.transform);
        battleParent.GetComponentInChildren<BattleManager>().BattleStart();
    }

    [YarnCommand("EndBattle")]
    public void LeaveBattle()
    {

        battleParent.SetActive(false);
        overworldParent.SetActive(true);
    }
}
