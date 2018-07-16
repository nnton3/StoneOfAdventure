using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_creepyZomby : Zombie {

	public float runDistance = 7f;
	public float runSpeed = 5f;
	//GameObject hpBar;

	void Update () {
		//Если зомби ожил
		if (!idle) {
			//Если он может двигаться
			if (CanMove()) {
				//Найти цель
				FindTarget ();
				flipParam = input;

				//Если юнит не находится в состоянии атаки
				if (CanAttack()) {
					//Если расстояние до цели меньше указанного и юнит стоит лицом к цели
					if (targetRange < (attackRange - 0.3f) && ((targetDirection < 0f && direction > 0f) || (targetDirection > 0f && direction < 0f))) {
						//Остановиться и атаковать
						Attack ();
					} else {
						//Двигаться в сторону игрока
						input = -targetDirection;
					}
				}
			}
			Run ();
		}
	}

	//Нанести урон
	public override void Attack () {
		input = 0f;
		if (!conditions.attack) {
			conditions.attack = true;
			anim.SetTrigger ("attack");
		}
	}
}
