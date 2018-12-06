using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombyKenny: Zombie {
	
	void Update () {
		//Если зомби ожил
		if (!idle) {
			//Если он может двигаться
			if (CanMove()) {
				//Найти цель
				FindTarget();
				flipParam = inputX;

				//Если зомби может атаковать
				if (CanAttack ()) {
					//Если цель в пределах досягаемости
					if (targetRange < (attackRange - 0.5f) && ((targetDirection < 0f && direction > 0f) || (targetDirection > 0f && direction < 0f))) {
						//Атаковать
						Attack ();
						//Если цель не найдена
					} else
						//Изменить направление движения
						inputX = -targetDirection;
				}
			}
			Run ();
		}
	}

	//Атаковать
	public override void Attack (){
		//Остановиться
		inputX = 0;
		conditions.attack = true;
		//Ударить
		anim.SetTrigger ("attack");
	}
		
	//Передать направление атаки
	public void GetDirectiOfHit () {
		FindTarget ();
		inputX = -targetDirection;
	}
}
