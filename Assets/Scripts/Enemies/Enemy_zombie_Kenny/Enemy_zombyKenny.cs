using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombyKenny : Zombie {

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	void Update () {
		//Если зомби ожил
		if (!idle) {
			//Если он может двигаться
			if (CanMove()) {
				//Найти цель
				FindTarget();
				flipParam = input;
				anim.SetFloat ("run", Mathf.Abs (input * moveSpeed));

				//Если зомби может атаковать
				if (CanAttack ()) {
					//Если цель в пределах досягаемости
					if (targetRange < (attackRange - 0.5f) && ((targetDirection < 0f && direction > 0f) || (targetDirection > 0f && direction < 0f))) {
						//Атаковать
						Attack ();
						//Если цель не найдена
					} else
						//Изменить направление движения
						input = -targetDirection;
				}
			}
			Run ();
		}
	}

	//Атаковать
	public override void Attack (){
		//Остановиться
		input = 0f;
		//Ударить
		anim.SetTrigger ("attack");
	}

	//Местоположения относительно игрока
	float targetRange = 0f;
	float targetDirection =0f;

	//Определение местоположения игрока
	void FindTarget () {
		targetRange = Mathf.Abs (transform.position.x - target.transform.position.x);
		targetDirection = Mathf.Sign (transform.position.x - target.transform.position.x);
	}
}
