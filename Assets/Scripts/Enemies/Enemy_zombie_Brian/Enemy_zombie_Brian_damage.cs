using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombie_Brian_damage : Damage {

	public override void DefaultDamage(float damage, float direction) {

		bool backToTheEnemy = BackToTheEnemyCheck (direction);

		if (conditions.block) {
			Debug.Log ("1");
			if (backToTheEnemy) {
				Debug.Log ("2");
				conditions.EnableStun (direction);
				anim.SetTrigger ("attackable");
				ReduceHP (damage);
			} else {
				Debug.Log ("3");
				conditions.EnableStun (direction);
				anim.SetTrigger ("attackableInBlock");
			}
		} else {
			Debug.Log ("4");
			ReduceHP (damage);
		}
	}
}
