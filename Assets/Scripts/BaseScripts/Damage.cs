using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

	[HideInInspector]
	public Unit unit;
	[HideInInspector]
	public Conditions conditions;
	[HideInInspector]
	public Animator anim;

	void Start() {
		unit = GetComponent<Unit> ();
		conditions = GetComponent<Conditions> ();
		anim = GetComponent<Animator> ();
	}

	//Стандартный удар
	public virtual void DefaultDamage(float damage, float direction) {
		unit.health -= damage;
	}

	//Удар, игнорирующий блок
	public virtual void DamageThroughTheBlock (float damage, float direction) {
		//Если игрок не сделал перекат
		if (!conditions.invulnerability) {
			ReduceHP (damage);
			anim.SetTrigger ("attackable");
		}
	}

	//Удар игнорирующий перекат
	public virtual void DamageIgnoresTheRoll (float damage, float direction) {
		//Если игрок не поставил блок
		if (!conditions.block) {
			ReduceHP (damage);
			anim.SetTrigger ("attackable");
		}
	}

	//Критический урон
	public virtual void CriticalDamage (float damage, float direction, float criticalScale) {
		unit.health -= (damage * criticalScale);
	}

	//Лечение
	public virtual void Healing (float HP)
	{
		unit.health += HP;
	}

	//Проверка: игрок стоит спиной к врагу?
	public bool BackToTheEnemyCheck (float direction) {
		return unit.direction == direction;
	}

	//Уменьшить ХП + проверка на "смерть"
	public void ReduceHP (float damage) {
		if (unit.health <= damage) {
			conditions.UnitDie ();
		}
		unit.health -= damage;
	}
}
