using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombie_Kenny_damage : Damage {

	public override void DefaultDamage (float damage, int direction)
	{
		ReduceHP (damage);
		conditions.EnableStun (direction);
	}
}
