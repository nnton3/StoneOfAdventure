using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zomby_miner_damage : Damage {

	public override void DefaultDamage (float damage, float direction) {
		ReduceHP (damage);
		conditions.EnableStun (direction);
	}
}
