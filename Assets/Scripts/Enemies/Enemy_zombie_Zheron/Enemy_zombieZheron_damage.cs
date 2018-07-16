using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombieZheron_damage : Damage {
	public override void DefaultDamage(float damage, float direction) {
	
		bool backToTheEnemy = BackToTheEnemyCheck (direction);

		//Если включен блок
		if (conditions.block) {
			//Если герой бьет в спину
			if (backToTheEnemy) {
				//Получить стан
				conditions.EnableStun (direction);
				//Получить урон
				anim.SetTrigger ("attackable");
				ReduceHP (damage);
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
				anim.SetTrigger ("attackable");
				conditions.EnableStun (direction);
			}
			//Получить урон
			ReduceHP (damage);
		}
	}
}
