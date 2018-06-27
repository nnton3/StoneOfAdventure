using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit {

	//Ссылка на игрока
	[HideInInspector]
	public GameObject target;
	//Состояния врага
	[HideInInspector]
	public bool idle = true;

	void Awake() {
		RegistrationInStack ();
	}

	//Тревога/Игрок замечен
	public virtual void Alert (GameObject player) {
		target = player;
	}
}
