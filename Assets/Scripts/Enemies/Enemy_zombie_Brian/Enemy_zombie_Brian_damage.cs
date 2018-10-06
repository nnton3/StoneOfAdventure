using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombie_Brian_damage : Damage {

	public override void DefaultDamage(float damage, int direction) {

		bool backToTheEnemy = BackToTheEnemyCheck (direction);

		//Если включен блок
		if (conditions.block) {
			//Если герой бьет в спину
			if (backToTheEnemy) {
				//Получить стан
				conditions.EnableStun (direction);
				//Получить урон
				ReduceHP (damage);
				unit.CreateOutputBlood (direction);
				//Если игрок бьет спереди
			} else {
				//Получить стан
				conditions.EnableStun (direction);
				anim.SetTrigger ("attackableInBlock");
			}
			//Если не включен блок
		} else {
			//Если не начата атака
			if (unit.CanAttack()) {
				//Получить стан
				conditions.EnableStun (direction);
			}
			//Получить урон
			ReduceHP (damage);
			unit.CreateOutputBlood (direction);
		}
	}

	public override void ReduceHP (float damage) {
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
				if (!conditions.block) {
					anim.SetTrigger ("attackable");
				}
				return;
				//Иначе блокировать часть урона за счет брони, а остаток вычесть из здоровья
			} else {
				damage -= unit.armor;
				unit.armor = 0f;
				unit.health -= damage;
				if (conditions.block) {
					anim.SetTrigger ("attackable");
				}	
			}
		} else {
			damage -= unit.armor;
			unit.health -= damage;
			if (conditions.block) {
				anim.SetTrigger ("attackable");
			}
		}
		unit.StartChangeColor ();
	}
}
