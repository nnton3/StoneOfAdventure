using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoker : Zombie {
	
	public GameObject zombie;

	GameObject hpBar;

	void Update () {
		//Если зомби ожил
		if (!idle) {
			//Если он может двигаться и атаковать
			if (CanAttack() && CanMove()) {
				//Атаковать
				Attack ();
			} 
		}
		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);
	}

	public override void Attack (){
		base.Attack ();
	}

	public override void Alert (GameObject player)
	{
		base.Alert (player);
	}

}
