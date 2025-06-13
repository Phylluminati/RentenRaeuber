using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

	public string unitName;
	public string unitDescriptor;
	public int unitLevel;

	public int damage;

	public int maxHP;
	public int currentHP;

	public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
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
		Fashionista.damage = 10;

		Unit OldBag = new Unit();
		OldBag.unitName = "Old Bag";
		OldBag.unitDescriptor = "Reminiscing of the good ol' eighties. Old Bag's best times might be past him, but he enjoys life best he can (by being mildly annoyed at all times and knitting)";
		OldBag.maxHP = 140;
		OldBag.currentHP = 140;
		OldBag.damage = 20;

    }
     

}
