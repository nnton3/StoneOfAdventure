using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombieZheron_damage : Damage {
	
	public override void DefaultDamage(float damage, int direction) {
	
		bool backToTheEnemy = BackToTheEnemyCheck (direction);
		//Если включен блок
		if (conditions.block) {
			//Если герой бьет в спину
			if (backToTheEnemy) {
				Debug.Log ("damage 1");
				//Получить стан
				conditions.EnableStun (direction);
				//Получить урон
				ReduceHP (damage);
				//Если игрок бьет спереди
			} else {
				Debug.Log ("damage 2");
				//Заблокировать
				conditions.EnableStun (direction);
				anim.SetTrigger ("blocked");
			}
			//Если не включен блок
		} else {
			//Если не начата атака
			if (!unit.CanAttack()) {
				Debug.Log ("damage 3");
				ReduceHP (damage);
				//Получить стан
				conditions.EnableStun (direction);
			}
			Debug.Log ("damage 4");
			//Получить урон
			//ReduceHP (damage);
		}
	}
}
