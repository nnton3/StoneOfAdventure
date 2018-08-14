using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombieBrian : Zombie {

	void Update () {
		//Если зомби ожил
		if (!idle) {
			//Если он может двигаться
			if (CanMove ()) {
				//Найти цель
				FindTarget ();
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
				Run ();
			}
		}
	}

	//Нанести урон
	public override void Attack () {
		//Остановиться
		inputX = 0f;
		//Выйти из стана
		conditions.DisableStun();
		//Убрать блок если юнит в блоке
		CheckBlock ();
		//Перейти в состояние атаки
		conditions.attack = true;
		//Выбрать одну из доступных атак
		SelectAttack ();
	}

	//Случайный выбор атаки: один удар или серия из двух
	void SelectAttack () {
		float attackAnim = Random.Range (0f, 1f);
		if (attackAnim <= 0.5f) {
			anim.SetTrigger ("attack1");
			return;
		}

		if (attackAnim > 0.5f) {
			anim.SetTrigger ("attack2");
			return;
		}
	}
}
