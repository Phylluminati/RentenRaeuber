using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

	/*public Text nameText;
	public Text levelText;
	public Slider hpSlider;*/

	[SerializeField]
	private Image _healthBarSprite;
	private float _target;
	[SerializeField]
	private float _reduceSpeed;

	[SerializeField]
	private Image _manaBarSprite;
	void Start()
	{
		UpdateHealthBar(this.GetComponent<Unit>().currentHP);
		if (_manaBarSprite != null)
		{
			UpdateManaBar(this.GetComponent<Unit>().currentMP);
		}
	}
	public void UpdateHealthBar(float currentHealth)
	{
		float maxHealth = this.GetComponent<Unit>().maxHP;
		_target = currentHealth / maxHealth;
	}
	/*public void SetHUD(Unit unit)
	{
		nameText.text = unit.unitName;
		levelText.text = "Lvl " + unit.unitLevel;
		hpSlider.maxValue = unit.maxHP;
		hpSlider.value = unit.currentHP;
	}

	public void SetHP(int hp)
	{
		hpSlider.value = hp;
	} 
	*/
	public void UpdateManaBar(float currentMP)
	{
		float maxMana = this.GetComponent<Unit>().maxMP;
		_manaBarSprite.fillAmount = currentMP / maxMana;

	}

	void Update()
	{
		_healthBarSprite.fillAmount = Mathf.MoveTowards(_healthBarSprite.fillAmount, _target, _reduceSpeed * Time.deltaTime);
	}


}
