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
			conditions.EnableStun (direction);
			ReduceHP (damage);
			anim.SetTrigger ("attackable");
		}
	}

	//Удар игнорирующий перекат
	public virtual void DamageIgnoresTheRoll (float damage, float direction) {
		
		bool backToTheEnemy = BackToTheEnemyCheck (direction);

		//Если игрок поставил блок
		if (conditions.block) {
			if (backToTheEnemy) {
				conditions.EnableStun (direction);
				ReduceHP (damage);
				anim.SetTrigger ("attackable");
			} else {
				conditions.EnableStun (direction);
				anim.SetTrigger ("blocked");
			}
		} else {
			conditions.DisableInvulnerability ();
			conditions.EnableStun (direction);
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

	//Сбит с ног
	public virtual void KnockDown(float damage, float direction, float impulsePower) {
		if (!conditions.invulnerability) {
			conditions.SetImpulse (impulsePower);
			conditions.EnableStun (direction);
			ReduceHP (damage);
			anim.SetTrigger ("knock_down");
		}
	}

	//Проверка: игрок стоит спиной к врагу?
	public bool BackToTheEnemyCheck (float direction) {
		return unit.direction == direction;
	}

	//Уменьшить ХП + проверка на "смерть"
	public void ReduceHP (float damage) {
		if (unit.health <= damage) {
			conditions.UnitDie ();
			unit.health -= damage;
			return;
		}
		unit.health -= damage;
		anim.SetTrigger ("attackable");
	}
}
