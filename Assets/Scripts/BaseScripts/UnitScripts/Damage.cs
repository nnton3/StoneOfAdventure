﻿using System.Collections;
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
	public virtual void DefaultDamage(float damage, float direction) {
		unit.health -= damage;
	}

	/*! Получение урона, который нельзя заблокировать

		\param[in] damage величина входящего урона
		\param[in] direction направление удара (с какой стороны ударили слева или справа)
	*/
	public virtual void DamageThroughTheBlock (float damage, float direction) {
		//Если игрок не сделал перекат
		if (!conditions.invulnerability) {
			conditions.EnableStun (direction);
			ReduceHP (damage);
			anim.SetTrigger ("attackable");
		}
	}

	/*! Получение урона, от которого нельзя увернуться

		\param[in] damage величина входящего урона
		\param[in] direction направление удара (с какой стороны ударили слева или справа)
	*/
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

	/*! Получение критического урона, зависящего от множителя атаки

		\param[in] damage величина входящего урона
		\param[in] direction направление удара (с какой стороны ударили слева или справа)
		\param[in] criticalScale множитель атаки
	*/
	public virtual void CriticalDamage (float damage, float direction, float criticalScale) {
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
	public virtual void KnockDown(float damage, float direction, float impulsePower) {
		if (!conditions.invulnerability) {
			conditions.SetImpulse (impulsePower);
			conditions.EnableStun (direction);
			ReduceHP (damage);
			anim.SetTrigger ("knock_down");
		}
	}

	/*! Проверка: игрок стоит спиной к врагу?
		
		\param[in] direction направление удара
		\return удар был в спину или нет
	*/
	public bool BackToTheEnemyCheck (float direction) {
		return unit.direction == direction;
	}

	/*! Отвечает за уменьшение параметра здоровья при получении юнитом урона
		и отслеживает был ли этот урон смертельным

		\param[in] damage величина входящего урона
	*/ 
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
