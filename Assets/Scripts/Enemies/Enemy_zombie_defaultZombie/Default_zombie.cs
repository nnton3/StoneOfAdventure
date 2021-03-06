﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Default_zombie : Zombie {

	public float runDistance = 7f;
	public float runSpeed = 5f;
	//GameObject hpBar;

	void Update () {
		//Если зомби ожил
		if (!idle) {
			//Если он может двигаться
			if (CanMove())
            {
				//Найти цель
				FindTarget ();
				flipParam = inputX;

				//Если юнит не находится в состоянии атаки
				if (CanAttack())
                {
					//Если расстояние до цели меньше указанного и юнит стоит лицом к цели
					if (targetRange < (attackRange - 0.3f) && ((targetDirection < 0f && direction > 0f) || (targetDirection > 0f && direction < 0f)))
                    {
						//Остановиться и атаковать
						Attack ();
					}
                    else
                    {
						SetTargetSpeedAndDirection ();
					}
				}
			}
			Run ();
		}
	}

	//Нанести урон
	public override void Attack () {
		inputX = 0;
		if (!conditions.attack) {
			conditions.attack = true;
			anim.SetTrigger ("attack");
		}
	}

	//Задать направление движения и скорость
	void SetTargetSpeedAndDirection () {
		//Если дистанция до цели меньше расстояния рывка
		if (targetRange < runDistance) {
			//Перейти на бег
			conditions.SetMovespeed (runSpeed);
		} else
			//Иначе - идти пешком
			conditions.SetMovespeed (conditions.defaultMovespeed);
		//Двигаться в сторону игрока
		inputX = -targetDirection;
	}

	public override void Run() {
		rb.velocity = new Vector2 (inputX * moveSpeed, rb.velocity.y);
		if (CanMove ()) {
			anim.SetFloat ("speed", Mathf.Abs (inputX * moveSpeed));
		}
	}
}
