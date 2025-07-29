using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using Yarn.Unity;

public class BattleManager : MonoBehaviour
{
    public GameObject Enemy;
    [SerializeField]
    GameObject fashionista;
    [SerializeField]
    GameObject oldBag;
    [SerializeField]
    GameObject catGrandma;
    int targetCharacter;
    [SerializeField]
    DialogueRunner  runner;

    int guardCounter = 0;


    // Start is called before the first frame update
    void Start()
    {
        //runner.VariableStorage.TryGetValue<bool>("$enemyIsAttacking", out bool enemyIsAttacking);
        //print(enemyIsAttacking);
        //runner.VariableStorage.SetValue("$enemyIsAttacking", true);
        //runner.VariableStorage.TryGetValue<bool>("$enemyIsAttacking", out enemyIsAttacking);
        //print(enemyIsAttacking);
        BattleStart();

    }

    public void BattleStart()
    {
        string tempString = Enemy.GetComponent<Unit>().unitName;
        runner.VariableStorage.SetValue("$enemy", tempString);

        //Updating all Healthbars and Manabars on start, just to be sure
        fashionista.GetComponent<BattleHUD>().UpdateHealthBar(fashionista.GetComponent<Unit>().currentHP);
        catGrandma.GetComponent<BattleHUD>().UpdateHealthBar(catGrandma.GetComponent<Unit>().currentHP);
        oldBag.GetComponent<BattleHUD>().UpdateHealthBar(oldBag.GetComponent<Unit>().currentHP);
        Enemy.GetComponent<BattleHUD>().UpdateHealthBar(Enemy.GetComponent<Unit>().currentHP);
        fashionista.GetComponent<BattleHUD>().UpdateHealthBar(fashionista.GetComponent<Unit>().currentMP);
        catGrandma.GetComponent<BattleHUD>().UpdateHealthBar(catGrandma.GetComponent<Unit>().currentMP);
        oldBag.GetComponent<BattleHUD>().UpdateHealthBar(oldBag.GetComponent<Unit>().currentMP);
    }
    public GameObject TurnDetector(int turnTracker)
    {
        if (turnTracker == 0)
        {
            return fashionista;
        }
        else if (turnTracker == 1)
        {
            return catGrandma;
        }
        else if (turnTracker == 2)
        {
            return oldBag;
        }
        else if (turnTracker == 3)
        {
            return Enemy;
        }

        
        print("ERROR IN TURNDETECTOR, TurnTracker exceeds expected values");
        return null;
    }

    [YarnCommand("newTurn")]
    public void NewTurn(int turnTracker)
    {
        //Runs every newly started turn to check things (atm it resets everyones guard back from 2 to 1, but I expect to use this more often)
        TurnDetector(turnTracker).GetComponent<Unit>().guard = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    [YarnCommand("fight")]
    public void Fight(int characterIndex)
    {
        GameObject character = TurnDetector(characterIndex);

        /*if (characterIndex == 0)
        {
            character = fashionista;
        }
        else if (characterIndex == 1)
        {
            character = catGrandma;
        }
        else if (characterIndex == 2)
        {
            character = oldBag;
        }
        else
        {
            character = null;
            print("ERROR: Turn Order outside the of expected Index range");
        }*/

        Debug.Log(character + " is using FIGHT");
        int currentDamage = (int)(character.GetComponent<Unit>().damage / Enemy.GetComponent<Unit>().guard);
        Enemy.GetComponent<Unit>().currentHP -= currentDamage;
        Enemy.GetComponent<BattleHUD>().UpdateHealthBar(Enemy.GetComponent<Unit>().currentHP);
    }
    [YarnCommand("special")]
    public void Special(GameObject Character, int specialTarget)
    {
        GameObject characterSpecialTarget = TurnDetector(specialTarget);

        int tempNumber = Random.Range(10, 21);
        characterSpecialTarget.GetComponent<Unit>().currentHP += tempNumber;

        runner.VariableStorage.SetValue("$tempNumber", tempNumber);
        Mathf.Clamp(characterSpecialTarget.GetComponent<Unit>().currentHP, 0, characterSpecialTarget.GetComponent<Unit>().maxHP);
        characterSpecialTarget.GetComponent<BattleHUD>().UpdateHealthBar(characterSpecialTarget.GetComponent<Unit>().currentHP);

        /*if (specialTarget != 0)
        {
            if (specialTarget == 1)
            {
                print("Healing Fashionista");
                //Targets Fashionista
                int tempNumber = 10 + Random.Range(0, 11);
                fashionista.GetComponent<Unit>().currentHP += tempNumber;
                print("Fashionista got healed by " + tempNumber + " HP.");
                runner.VariableStorage.SetValue("$tempNumber", tempNumber);
                Mathf.Clamp(fashionista.GetComponent<Unit>().currentHP, 0, fashionista.GetComponent<Unit>().maxHP);
                fashionista.GetComponent<BattleHUD>().UpdateHealthBar(fashionista.GetComponent<Unit>().currentHP);

            }
            else if (specialTarget == 2)
            {
                print("Healing Cat Grandma");
                //targets Cat Grandma
            }
            else if (specialTarget == 3)
            {
                print("Healing Old Bag");
                // targets Old Bag
            }
        }*/
        Debug.Log(Character + " is trying to use their SPECIAL, but it hasn't been coded yet");
        //Character.GetComponent<Unit>().
    }
    [YarnCommand("guard")]
    public void Guard(int turnTracker)
    {
        GameObject Character = TurnDetector(turnTracker);

        int tempNumber;
        if (Character.GetComponent<Unit>().guard != 2)
        {
            Character.GetComponent<Unit>().guard = 2;
        }
        else
        {
            print("Something is going wrong here, the value Guard should be resetted to one at the beginning of their turns");
            return;
        }
        tempNumber = Random.Range(10, 21);
        Character.GetComponent<Unit>().currentMP += tempNumber;
        Mathf.Clamp(Character.GetComponent<Unit>().currentMP, 0, Character.GetComponent<Unit>().maxMP);
        runner.VariableStorage.SetValue("$tempNumber", tempNumber);
        Character.GetComponent<BattleHUD>().UpdateManaBar(Character.GetComponent<Unit>().currentMP);

    }
    [YarnCommand("EnemyTurn")]
    public void EnemyTurn()
    {
        
        int randomNumber = Random.Range(0, 10);
        if (guardCounter > 0)
        {
            guardCounter -= 1;
            if (guardCounter == 0)
            {
                
                if (Enemy.GetComponent<Unit>().guard == 0.5)
                {
                    runner.VariableStorage.SetValue("$enemySpecialString", " is no longer weakened.");
                }
                else if (Enemy.GetComponent<Unit>().guard == 2)
                {
                    runner.VariableStorage.SetValue("$enemySpecialString", "'s guard has ran out.");
                }
                Enemy.GetComponent<Unit>().guard = 1;

                print("Enemy Guard ran out, resetting guard value to 1");
            }
        }
        if (randomNumber > 2)

        {
            //InsertEnemyAttack here

            //The Attacker has to pick between the 3 Characters

            targetCharacter = Random.Range(1, 4);
            EnemyAttack(targetCharacter);
        }
        else
        {
            //InsertGuard Action here

            if (Enemy.GetComponent<Unit>().guard != 1)
            {
                //If guard is already active, Make them attack instead (or give them a special move for weakening the guard of MCs)
                print("Enemy is already guard, they will instead now Attack");

                targetCharacter = Random.Range(1, 4);
                EnemyAttack(targetCharacter);

            }
            else
            {
                runner.VariableStorage.SetValue("$enemyAttackController", 0);
                Enemy.GetComponent<Unit>().guard = 2;
                guardCounter = 3;
            }

        }
    }

    public void EnemyAttack(int targetCharacter)
    {
        targetCharacter = Random.Range(1, 4);

        if (targetCharacter == 1)
        {
            print("Enemy is Attacking Fashionista");
            fashionista.GetComponent<Unit>().currentHP -= (int)(Enemy.GetComponent<Unit>().damage / fashionista.GetComponent<Unit>().guard);
            fashionista.GetComponent<BattleHUD>().UpdateHealthBar(fashionista.GetComponent<Unit>().currentHP);
            runner.VariableStorage.SetValue("$enemyAttackController", 1);

        }
        else if (targetCharacter == 2)
        {
            print("Enemy is Attacking Cat Grandma");
            catGrandma.GetComponent<Unit>().currentHP -= (int)(Enemy.GetComponent<Unit>().damage / catGrandma.GetComponent<Unit>().guard);
            catGrandma.GetComponent<BattleHUD>().UpdateHealthBar(catGrandma.GetComponent<Unit>().currentHP);
            runner.VariableStorage.SetValue("$enemyAttackController", 2);


        }
        else if (targetCharacter == 3)
        {
            print("Enemy is Attacking Old Bag");
            oldBag.GetComponent<Unit>().currentHP -= (int)(Enemy.GetComponent<Unit>().damage / oldBag.GetComponent<Unit>().guard);
            oldBag.GetComponent<BattleHUD>().UpdateHealthBar(oldBag.GetComponent<Unit>().currentHP);
            runner.VariableStorage.SetValue("$enemyAttackController", 3);
        }
                print("End of Enemy Attack");
                return;
    }
}
