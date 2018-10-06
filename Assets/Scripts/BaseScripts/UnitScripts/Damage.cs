using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*!
	\biref Класс отвечающий за обработку входящего урона
	
	Каждый тип урона обрабатывается в отдельном методе
*/
public class Damage : MonoBehaviour {

	[HideInInspector]
	public Unit unit;   //< Ссылка на контроллер юнита
	[HideInInspector]
	public Conditions conditions;   //< Ссылка на контроллер состояний
	[HideInInspector]
	public Animator anim;   //< Ссылка на контроллер анимаций

	void Start() {
		unit = GetComponent<Unit> ();
		conditions = GetComponent<Conditions> ();
		anim = GetComponent<Animator> ();
	}

	/*! Получение обычного урона

		\param[in] damage величина входящего урона
		\param[in] direction направление удара (с какой стороны ударили слева или справа)
	*/
	public virtual void DefaultDamage(float damage, int direction) {
		unit.health -= damage;
	}

	/*! Получение урона, который нельзя заблокировать

		\param[in] damage величина входящего урона
		\param[in] direction направление удара (с какой стороны ударили слева или справа)
	*/
	public virtual void DamageThroughTheBlock (float damage, int direction) {
		//Если игрок не сделал перекат
		if (!conditions.invulnerability) {
			if (!conditions.stun) {
				conditions.EnableStun (direction);
				conditions.DisableBlock ();
				anim.SetTrigger ("attackable");
			}
			ReduceHP (damage);
		}
	}

	/*! Получение урона, от которого нельзя увернуться

		\param[in] damage величина входящего урона
		\param[in] direction направление удара (с какой стороны ударили слева или справа)
	*/
	public virtual void DamageIgnoresTheRoll (float damage, int direction) {
		
		bool backToTheEnemy = BackToTheEnemyCheck (direction);

		//Если игрок поставил блок
		if (conditions.block) {
			if (backToTheEnemy) {
				KnockDown (damage, direction, 3f);
			} else {
				if (!conditions.stun) {
					conditions.EnableStun (direction);
					anim.SetTrigger ("blocked");
				}
			}
		} else {
			conditions.DisableInvulnerability ();
			KnockDown (damage, direction, 3f);
		}
	}

	/*! Получение критического урона, зависящего от множителя атаки

		\param[in] damage величина входящего урона
		\param[in] direction направление удара (с какой стороны ударили слева или справа)
		\param[in] criticalScale множитель атаки
	*/
	public virtual void CriticalDamage (float damage, int direction, float criticalScale) {
		unit.health -= (damage * criticalScale);
	}

	/*! Получение лечения

		\param[in] HP величина входящего лечения
	*/
	public virtual void Healing (float HP)
	{
		unit.health += HP;
	}

	/*! Получение удара, сбивающего с ног и отталкивающего юнита на определенное расстояние

		\param[in] damage величина входящего урона
		\param[in] direction направление удара (с какой стороны ударили слева или справа)
		\param[in] impulsePower сила отталкивания, определяет насколько далеко улетит юнит
	*/
	public virtual void KnockDown(float damage, int direction, float impulsePower) {
		if (!conditions.invulnerability) {
			conditions.SetImpulse (impulsePower);
			if (!conditions.stun) {
				conditions.EnableStun (direction);
				anim.SetTrigger ("knock_down");
			}
			ReduceHP (damage);
		}
	}

	/*! Проверка: игрок стоит спиной к врагу?
		
		\param[in] direction направление удара
		\return удар был в спину или нет
	*/
	public bool BackToTheEnemyCheck (int direction) {
		return unit.direction == direction;
	}

	/*! Отвечает за уменьшение параметра здоровья при получении юнитом урона
		и отслеживает был ли этот урон смертельным

		\param[in] damage величина входящего урона
	*/ 
	public virtual void ReduceHP (float damage) {
		//Если урон больше, чем сумма брони и здоровья
		if ((unit.health + unit.armor) <= damage) {
			conditions.UnitDie ();
			//Юнит погибает
			unit.health -= damage;
			return;
		}
		//Иначе если есть броня, то вычесть урон сначала из брони
		if (unit.armor != 0f) {
			//Если брони достаточно, чтобы, блокировать весь урон, то из здоровья ничего не вычитаем
			if (unit.armor >= damage) {
				unit.armor -= damage;
				anim.SetTrigger ("attackable");
				return;
				//Иначе блокировать часть урона за счет брони, а остаток вычесть из здоровья
			} else {
				damage -= unit.armor;
				unit.armor = 0f;
				unit.health -= damage;
				anim.SetTrigger ("attackable");
			}
		} else {
			damage -= unit.armor;
			unit.health -= damage;
			anim.SetTrigger ("attackable");
		}
	}
}
