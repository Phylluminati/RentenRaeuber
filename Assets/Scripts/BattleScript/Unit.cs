using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using Yarn.Unity;

public class Unit : MonoBehaviour
{

	public string unitName;
	public string unitDescriptor;
	public int unitLevel;

	public int damage;

	public int maxHP;
	public int currentHP;

	public int maxMP;
	public int currentMP;

	public float guard;
	//public bool hasGuard;

	/*public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}*/
	[YarnCommand("EnemyAttack")]
	public void takeDamage(int dmg, GameObject targetCharacter)
	{
		this.currentHP -= (int)(dmg / guard);

		if (currentHP <= 0)
		{
			
		}
	}

	public void Heal(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

	void Start()
	{
		Unit Fashionista = new Unit();
		Fashionista.unitName = "Fashionista";
		Fashionista.unitDescriptor = "A cool, fashionable Grandma, that always knows what to say. She will never go out of style";
		Fashionista.maxHP = 100;
		Fashionista.currentHP = 100;
		Fashionista.maxMP = 100;
		Fashionista.currentMP = 100;
		Fashionista.damage = 10;

		Unit OldBag = new Unit();
		OldBag.unitName = "Old Bag";
		OldBag.unitDescriptor = "Reminiscing of the good ol' eighties. Old Bag's best times might be past him, but he enjoys life best he can (by being mildly annoyed at all times and knitting)";
		OldBag.maxHP = 140;
		OldBag.currentHP = 140;
		OldBag.maxMP = 100;
		OldBag.currentMP = 100;
		OldBag.damage = 20;

		Unit CatGranny = new Unit();
		CatGranny.unitName = "CatGranny";
		CatGranny.unitDescriptor = "";
		CatGranny.maxHP = 120;
		CatGranny.currentHP = 120;
		CatGranny.maxMP = 100;
		CatGranny.currentMP = 100;
		CatGranny.damage = 10;

    }
     

}
