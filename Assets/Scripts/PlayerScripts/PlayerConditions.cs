using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditions : Conditions {

	//ПЕРЕКАТ
	//Параметры "Переката"
	public float rollImpulsePower = 10f;

	public override void EnableInvulnerability ()
	{
		//Если игрок не находится в стане и КД на перекат прошел
		SetImpulse (rollImpulsePower);
		base.EnableInvulnerability ();
		EnableStun (unit.direction);
		Physics2D.IgnoreLayerCollision (9, 8, true);
		attack = true;
		unit.inputX = (int)Mathf.Sign (unit.direction);
	}

	public override void DisableInvulnerability ()
	{
		DisableStun ();
		base.DisableInvulnerability ();
		Physics2D.IgnoreLayerCollision (9, 8, false);
	}

	//АТАКА МЕЧОМ

	public override void Default_Attack ()
	{
		base.Default_Attack ();
		//Меняем скорость атаки в зависимости от заданного параметра
		anim.speed = 1 / unit.attackSpeed;
	}

	//Сбросить чек атаки
	public override IEnumerator FinishAttack () {
		yield return new WaitForSeconds (0.75f);
		attack = false;
		anim.speed = 1;
	}

	//СМЕРТЬ
	public override void UnitDie () {
		alive = false;
	}
}
