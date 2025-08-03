using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.TextCore.Text;
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
    DialogueRunner runner;
    [SerializeField] ProgressionManager progressionManager;
    [SerializeField] DialogueRunner overworldDialogueRunner;

    int guardCounter = 0;
    [SerializeField] List<Material> particles;


    // Start is called before the first frame update
    void Start()
    {
        //runner.VariableStorage.TryGetValue<bool>("$enemyIsAttacking", out bool enemyIsAttacking);
        //print(enemyIsAttacking);
        //runner.VariableStorage.SetValue("$enemyIsAttacking", true);
        //runner.VariableStorage.TryGetValue<bool>("$enemyIsAttacking", out enemyIsAttacking);
        //print(enemyIsAttacking);
    }

    public void BattleStart()
    {
        if (GameObject.Find("Nurse1(Clone)") == true)
        {
            Enemy = GameObject.Find("Nurse1(Clone)");
            print("found Nurse 1");
        }
        if (GameObject.Find("Nurse2(Clone)") == true)
        {
            Enemy = GameObject.Find("Nurse2(Clone)");
            print("found Nurse 2");
        }
        if (GameObject.Find("Nurse3(Clone)") == true)
        {
            Enemy = GameObject.Find("Nurse3(Clone)");
            print("found Nurse 3");
        }

        string tempString = Enemy.GetComponent<Unit>().unitName;
        runner.VariableStorage.SetValue("$enemy", tempString);

        //Updating all Healthbars and Manabars on start, just to be sure
        fashionista.GetComponent<BattleHUD>().UpdateHealthBar(fashionista.GetComponent<Unit>().currentHP);
        catGrandma.GetComponent<BattleHUD>().UpdateHealthBar(catGrandma.GetComponent<Unit>().currentHP);
        oldBag.GetComponent<BattleHUD>().UpdateHealthBar(oldBag.GetComponent<Unit>().currentHP);
        Enemy.GetComponent<BattleHUD>().UpdateHealthBar(Enemy.GetComponent<Unit>().currentHP);
        fashionista.GetComponent<BattleHUD>().UpdateManaBar(fashionista.GetComponent<Unit>().currentMP);
        catGrandma.GetComponent<BattleHUD>().UpdateManaBar(catGrandma.GetComponent<Unit>().currentMP);
        oldBag.GetComponent<BattleHUD>().UpdateManaBar(oldBag.GetComponent<Unit>().currentMP);
        CharacterHealthCheck(fashionista);
        CharacterHealthCheck(oldBag);
        CharacterHealthCheck(catGrandma);
        CharacterHealthCheck(Enemy);
        runner.StartDialogue("Combat");
    }
    public void BarUpdate()
    {
        fashionista.GetComponent<BattleHUD>().UpdateHealthBar(fashionista.GetComponent<Unit>().currentHP);
        catGrandma.GetComponent<BattleHUD>().UpdateHealthBar(catGrandma.GetComponent<Unit>().currentHP);
        oldBag.GetComponent<BattleHUD>().UpdateHealthBar(oldBag.GetComponent<Unit>().currentHP);
        Enemy.GetComponent<BattleHUD>().UpdateHealthBar(Enemy.GetComponent<Unit>().currentHP);
        fashionista.GetComponent<BattleHUD>().UpdateManaBar(fashionista.GetComponent<Unit>().currentMP);
        catGrandma.GetComponent<BattleHUD>().UpdateManaBar(catGrandma.GetComponent<Unit>().currentMP);
        oldBag.GetComponent<BattleHUD>().UpdateManaBar(oldBag.GetComponent<Unit>().currentMP);
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
        BarUpdate();
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
        character.GetComponentInChildren<Animator>().SetTrigger("Attack");
        CharacterHealthCheck(Enemy);
        Enemy.GetComponentInChildren<Animator>().SetTrigger("Block");
        runner.VariableStorage.SetValue("$tempNumber", currentDamage);
    }
    [YarnCommand("special")]
    public void Special(int turnController, int specialTarget)
    {
        GameObject Character = TurnDetector(turnController);
        GameObject characterSpecialTarget = TurnDetector(specialTarget);

        if (Character == TurnDetector(1))
        {
            if (Character.GetComponent<Unit>().currentMP >= 20)
            {
                int tempNumber = Random.Range(10, 21);
                characterSpecialTarget.GetComponent<Unit>().currentHP += tempNumber;

                runner.VariableStorage.SetValue("$tempNumber", tempNumber);
                Mathf.Clamp(characterSpecialTarget.GetComponent<Unit>().currentHP, 0, characterSpecialTarget.GetComponent<Unit>().maxHP);
                characterSpecialTarget.GetComponent<BattleHUD>().UpdateHealthBar(characterSpecialTarget.GetComponent<Unit>().currentHP);
                Character.GetComponent<Unit>().currentMP -= 20;
                Mathf.Clamp(Character.GetComponent<Unit>().currentMP, 0, Character.GetComponent<Unit>().maxMP);
                Character.GetComponentInChildren<Animator>().SetTrigger("Spellcasting");
            }
            else
            {
                runner.VariableStorage.SetValue("$specialBlocked", true);
            }
        }
        if (Character == TurnDetector(0))
        {
            //Fashionista's Special: A debuff inflicted on the enemy, making them either 1) drop their guard or 2) make them take double damage. The Enemy can't guard for the next turn
            if (Character.GetComponent<Unit>().currentMP >= 20)
            {
                Enemy.GetComponent<Unit>().guard = 0.5f;
                Character.GetComponent<Unit>().currentMP -= 20;
                guardCounter = 2;
                Character.GetComponent<BattleHUD>().UpdateManaBar(Character.GetComponent<Unit>().currentMP);

                Enemy.GetComponentInChildren<ParticleSystemRenderer>().material = particles[1];
                Enemy.GetComponentInChildren<ParticleSystem>().Play();
                Character.GetComponentInChildren<Animator>().SetTrigger("Taunt");
            }
            else
            {
                runner.VariableStorage.SetValue("$specialBlocked", true);
            }

        }
        if (Character == TurnDetector(2))
        {
            if (Character.GetComponent<Unit>().currentMP >= 30)
            {

                Character.GetComponent<Unit>().currentMP -= 30;

                int tempDamage = (int)(Random.Range(20, 31) / Enemy.GetComponent<Unit>().guard);
                int tempNumber = Random.Range(10, 20);

                runner.VariableStorage.SetValue("$tempNumber", tempDamage);
                runner.VariableStorage.SetValue("$tempNumber2", tempNumber);

                Enemy.GetComponent<Unit>().currentHP -= tempDamage;
                oldBag.GetComponent<Unit>().currentHP -= tempNumber;

                Character.GetComponent<BattleHUD>().UpdateHealthBar(Character.GetComponent<Unit>().currentHP);
                CharacterHealthCheck(Character);
                Enemy.GetComponent<BattleHUD>().UpdateHealthBar(Enemy.GetComponent<Unit>().currentHP);
                CharacterHealthCheck(Enemy);


            }
            else
            {
                runner.VariableStorage.SetValue("$specialBlocked", true);
            }


        }
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
        Debug.Log(Character + "used their SPECIAL");

        //Updating all important InfoBars after special
        Enemy.GetComponent<BattleHUD>().UpdateHealthBar(Enemy.GetComponent<Unit>().currentHP);
        Character.GetComponent<BattleHUD>().UpdateHealthBar(Character.GetComponent<Unit>().currentHP);
        Character.GetComponent<BattleHUD>().UpdateManaBar(Character.GetComponent<Unit>().currentMP);
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
        Character.GetComponentInChildren<Animator>().SetTrigger("Block");

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

            targetCharacter = Random.Range(0, 3);
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
                Enemy.GetComponentInChildren<ParticleSystemRenderer>().material = particles[0];
                Enemy.GetComponentInChildren<ParticleSystem>().Play();
                Enemy.GetComponentInChildren<Animator>().SetTrigger("Block");
            }

        }
    }

    public void EnemyAttack(int targetCharacter)
    {

        if (targetCharacter == 0)
        {
            if (CharacterDown(TurnDetector(targetCharacter)))
            {
                targetCharacter += 1;
                EnemyAttack(targetCharacter);
                return;
            }
            print("Enemy is Attacking Fashionista");
            fashionista.GetComponent<Unit>().currentHP -= (int)(Enemy.GetComponent<Unit>().damage / fashionista.GetComponent<Unit>().guard);
            fashionista.GetComponent<BattleHUD>().UpdateHealthBar(fashionista.GetComponent<Unit>().currentHP);
            runner.VariableStorage.SetValue("$enemyAttackController", 1);
            fashionista.GetComponentInChildren<Animator>().SetTrigger("Block");
            CharacterHealthCheck(TurnDetector(targetCharacter));
            runner.VariableStorage.SetValue("$tempNumber", (int)(Enemy.GetComponent<Unit>().damage / fashionista.GetComponent<Unit>().guard));
            return;

        }
        else if (targetCharacter == 1 && !CharacterDown(TurnDetector(targetCharacter)))
        {
            if (CharacterDown(TurnDetector(targetCharacter)))
            {
                targetCharacter += 1;
                EnemyAttack(targetCharacter);
                return;
            }
            print("Enemy is Attacking Cat Grandma");
            catGrandma.GetComponent<Unit>().currentHP -= (int)(Enemy.GetComponent<Unit>().damage / catGrandma.GetComponent<Unit>().guard);
            catGrandma.GetComponent<BattleHUD>().UpdateHealthBar(catGrandma.GetComponent<Unit>().currentHP);
            runner.VariableStorage.SetValue("$enemyAttackController", 2);
            CharacterHealthCheck(TurnDetector(targetCharacter));
            catGrandma.GetComponentInChildren<Animator>().SetTrigger("Block");
            runner.VariableStorage.SetValue("$tempNumber", (int)(Enemy.GetComponent<Unit>().damage / catGrandma.GetComponent<Unit>().guard));
            return;


        }
        else if (targetCharacter == 2 && !CharacterDown(TurnDetector(targetCharacter)))
        {
            if (CharacterDown(TurnDetector(targetCharacter)))
            {
                targetCharacter = 0;
                EnemyAttack(targetCharacter);
                return;
            }

            print("Enemy is Attacking Old Bag");
            oldBag.GetComponent<Unit>().currentHP -= (int)(Enemy.GetComponent<Unit>().damage / oldBag.GetComponent<Unit>().guard);
            oldBag.GetComponent<BattleHUD>().UpdateHealthBar(oldBag.GetComponent<Unit>().currentHP);
            runner.VariableStorage.SetValue("$enemyAttackController", 3);
            oldBag.GetComponentInChildren<Animator>().SetTrigger("Block");
            CharacterHealthCheck(TurnDetector(targetCharacter));
            runner.VariableStorage.SetValue("$tempNumber", (int)(Enemy.GetComponent<Unit>().damage / oldBag.GetComponent<Unit>().guard));
            return;


        }

        print("End of Enemy Attack");
        return;
        //Okay, about this method...
        //If EnemyAttack gets initiated, when all three characters are down, it basically crashes EVERYTHING, because of infinite looping back and forth
        //I am trying to fix it, but the best fix, is either a failsafe that adds more complexity, or (how I'm currently doing it) make sure EnemyAttack NEVER initiates after all three characters go down
        //I am praying that this code won't end up being a problem
    }
    public void CharacterHealthCheck(GameObject character)
    {
        if (character.GetComponent<Unit>().currentHP <= 0)
        {
            Mathf.Clamp(character.GetComponent<Unit>().currentHP, 0, character.GetComponent<Unit>().maxHP);
            if (character == TurnDetector(0))
            {
                //Fashionista gets downed
                runner.VariableStorage.SetValue("$char1Down", true);
                character.GetComponentInChildren<Animator>().SetBool("KO", true);
            }
            else if (character == TurnDetector(1))
            {
                //Cat Grandma gets downed
                runner.VariableStorage.SetValue("$char2Down", true);
                character.GetComponentInChildren<Animator>().SetBool("KO", true);
            }
            else if (character == TurnDetector(2))
            {
                //Old Bag gets downed
                runner.VariableStorage.SetValue("$char3Down", true);
                character.GetComponentInChildren<Animator>().SetBool("KO", true);
            }
            else if (character == TurnDetector(3))
            {
                runner.VariableStorage.SetValue("$enemyDown", true);
                character.GetComponentInChildren<Animator>().SetBool("KO", true);

            }

        }
        else
        {
            if (character == TurnDetector(0))
            {
                //Fashionista gets downed
                runner.VariableStorage.SetValue("$char1Down", false);
                character.GetComponentInChildren<Animator>().SetBool("KO", false);
            }
            else if (character == TurnDetector(1))
            {
                //Cat Grandma gets downed
                runner.VariableStorage.SetValue("$char2Down", false);
                character.GetComponentInChildren<Animator>().SetBool("KO", false);
            }
            else if (character == TurnDetector(2))
            {
                //Old Bag gets downed
                runner.VariableStorage.SetValue("$char3Down", false);
                character.GetComponentInChildren<Animator>().SetBool("KO", false);
            }
            else if (character == TurnDetector(3))
            {
                runner.VariableStorage.SetValue("$enemyDown", false);
                character.GetComponentInChildren<Animator>().SetBool("KO", false);

            }
        }
    }
    public bool CharacterDown(GameObject character)
    {
        if (character.GetComponent<Unit>().currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        // could also just be return (character.GetComponent<Unit>().currentHP <= 0)
        // But it's more readabe this way, otherwise I'm going to lose complete oversight over my code
    }

    public void WinLoseCheck()
    {

    }
    [YarnCommand("Lost")]
    public void Lost()
    {
        overworldDialogueRunner.VariableStorage.SetValue("$battleLost", true);
        fashionista.GetComponent<Unit>().currentHP += 1;
        oldBag.GetComponent<Unit>().currentHP += 1;
        catGrandma.GetComponent<Unit>().currentHP += 1;
        //Needs to return 1 HP to every downed character, getting them out of the downed state
        CharacterHealthCheck(fashionista);
        CharacterHealthCheck(oldBag);
        CharacterHealthCheck(catGrandma);
        GameObject.Destroy(Enemy);

        progressionManager.LeaveBattle();
    }
    [YarnCommand("Won")]
    public void Won()
    {
        overworldDialogueRunner.VariableStorage.SetValue("$battleWon", true);
        fashionista.GetComponent<Unit>().currentHP += 1;
        oldBag.GetComponent<Unit>().currentHP += 1;
        catGrandma.GetComponent<Unit>().currentHP += 1;
        //Needs to return 1 HP to every downed character, getting them out of the downed state
        CharacterHealthCheck(fashionista);
        CharacterHealthCheck(oldBag);
        CharacterHealthCheck(catGrandma);
        GameObject.Destroy(Enemy);

        progressionManager.LeaveBattle();

    }
}
