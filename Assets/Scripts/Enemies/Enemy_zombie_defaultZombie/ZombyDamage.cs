using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombyDamage : Damage {

	public override void DefaultDamage (float damage, float stunDirection) {
		conditions.EnableStun (stunDirection);	
		unit.health = 0f;
		conditions.UnitDie ();
	}
}
