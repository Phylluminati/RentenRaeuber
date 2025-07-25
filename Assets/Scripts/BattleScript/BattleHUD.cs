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
    }
	public void UpdateHealthBar(float maxHealth, float currentHealth)
	{
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

	void Update()
	{
		_healthBarSprite.fillAmount = Mathf.MoveTowards(_healthBarSprite.fillAmount, _target, _reduceSpeed * Time.deltaTime);
	}

}
