using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombie_Brian_damage : Damage {

	public override void DefaultDamage(float damage, float direction) {

		bool backToTheEnemy = BackToTheEnemyCheck (direction);

		if (conditions.block) {
			if (backToTheEnemy) {
				conditions.EnableStun (direction);
				anim.SetTrigger ("attackable");
				ReduceHP (damage);
			} else {
				conditions.EnableStun (direction);
				anim.SetTrigger ("attackableInBlock");
			}
		} else 
			ReduceHP (damage);
	}
}
