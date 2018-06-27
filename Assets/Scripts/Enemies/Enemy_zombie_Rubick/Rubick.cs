using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubick : Zombie {

	void Update () {
		//Если зомби ожил
		if (!idle) 
		{
			//Если он может двигаться и атаковать
			if (CanAttack() && CanMove()) 
			{
				flipParam = (target.transform.position.x > transform.position.x) ? 1 : -1;
				//Атаковать
				Attack ();
			} 
		}
		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);
	}

	public override void Attack ()
	{
		conditions.attack = true;
		anim.SetTrigger ("attack");
	}

	public override void Alert (GameObject player)
	{
		base.Alert (player);
	}
}
