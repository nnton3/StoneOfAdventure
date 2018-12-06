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
	public int targetDirection =0;

	//Определение местоположения игрока
	public virtual void FindTarget () {
		targetRange = Mathf.Abs (transform.position.x - target.transform.position.x);
		targetDirection = (int)Mathf.Sign (transform.position.x - target.transform.position.x);
	}
}
