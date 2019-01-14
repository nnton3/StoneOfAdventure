using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy {

	//Время до появления
	public float bornDelay = 0f;

	public override void Alert (GameObject player)
	{
		base.Alert (player);
		StartCoroutine ("TimeToBorn");
	}

    public virtual void StartChase()
    {
        gameObject.layer = 9;
        idle = false;
        conditions.hp_bar.SetActive(true);
    }

	//Задержка перед воскрешением
	public virtual IEnumerator TimeToBorn() {
		yield return new WaitForSeconds (bornDelay);
		anim.SetTrigger ("born");
	}
}
