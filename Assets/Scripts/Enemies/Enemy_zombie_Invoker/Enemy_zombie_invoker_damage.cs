﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombie_invoker_damage : Damage {
	
	public override void DefaultDamage (float damage, int stunDirection) {
		conditions.EnableStun (stunDirection);
		ReduceHP (damage);
		anim.SetTrigger ("attackable");
	}
}
