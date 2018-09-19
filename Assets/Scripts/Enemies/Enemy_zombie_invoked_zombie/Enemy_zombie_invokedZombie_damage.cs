using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombie_invokedZombie_damage :  Damage {

	public override void DefaultDamage (float damage, int stunDirection) {
		conditions.EnableStun (stunDirection);	
		unit.health = 0f;
		conditions.UnitDie ();
	}
}
