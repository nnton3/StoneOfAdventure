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

	//Местоположения относительно игрока
	[HideInInspector]
	public float targetRange = 0f;
	[HideInInspector]
	public float targetDirection =0f;

	//Определение местоположения игрока
	public virtual void FindTarget () {
		targetRange = Mathf.Abs (transform.position.x - target.transform.position.x);
		targetDirection = Mathf.Sign (transform.position.x - target.transform.position.x);
	}
}
