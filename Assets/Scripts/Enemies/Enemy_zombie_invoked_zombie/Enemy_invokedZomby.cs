﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_invokedZomby : Zombie {

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
						SetTargetSpeedAndDirection ();
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

	//Местоположения относительно игрока
	float targetRange = 0f;
	float targetDirection =0f;

	//Определение местоположения игрока
	void FindTarget () {
		targetRange = Mathf.Abs (transform.position.x - target.transform.position.x);
		targetDirection = Mathf.Sign (transform.position.x - target.transform.position.x);
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
		input = -targetDirection;
	}
}