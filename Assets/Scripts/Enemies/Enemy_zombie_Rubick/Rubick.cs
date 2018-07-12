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

	public override IEnumerator TimeToBorn () {
		input = 1f;
		anim.SetTrigger ("born");
		yield return new WaitForSeconds (bornDelay);
		input = 0f;
		rb.gravityScale = 1f;
	}

	public void EnableGravity () {
		rb.gravityScale = 1f;
	}
}
