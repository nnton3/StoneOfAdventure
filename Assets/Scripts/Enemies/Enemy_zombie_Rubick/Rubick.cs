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
		rb.velocity = new Vector2 (inputX * moveSpeed, rb.velocity.y);
	}

	public override IEnumerator TimeToBorn () {
		inputX = 1;
		anim.SetTrigger ("born");
		yield return new WaitForSeconds (bornDelay);
		inputX = 0;
		rb.gravityScale = 1f;
	}
}
