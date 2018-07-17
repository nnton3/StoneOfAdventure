using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombieZheron_damage : Damage {
	
	public override void DefaultDamage(float damage, float direction) {
	
		bool backToTheEnemy = BackToTheEnemyCheck (direction);
		Debug.Log ("check");
		//Если включен блок
		if (conditions.block) {
			//Если герой бьет в спину
			if (backToTheEnemy) {
				Debug.Log ("1damage");
				//Получить стан
				conditions.EnableStun (direction);
				//Получить урон
				anim.SetTrigger ("attackable");
				ReduceHP (damage);
				//Если игрок бьет спереди
			} else {
				Debug.Log ("2damage");
				//Заблокировать
				conditions.EnableStun (direction);
				anim.SetTrigger ("blocked");
			}
			//Если не включен блок
		} else {
			Debug.Log ("3damage");
			//Если не начата атака
			if (!unit.CanAttack()) {
				//Получить стан
				anim.SetTrigger ("attackable");
				conditions.EnableStun (direction);
			}
			//Получить урон
			ReduceHP (damage);
		}
	}
}
