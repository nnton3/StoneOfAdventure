using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Paladin : Enemy {

	void Update () {

		//Если началась битва
		if (!idle) {
			//Если персонаж может двигаться
			if (CanMove()) {
				//Определить местоположение противника
				FindTarget ();
				flipParam = input;
				if (CanAttack()) {
					//Если лицом к персонажу
					if ((targetDirection < 0f && direction > 0f) || (targetDirection > 0f && direction < 0f)) {
						//Если враг в пределах ближней атаки
						if (targetRange <= (attackRange - 0.5f)) { 
						//Использовать ближнюю атаку
							Attack();
							//Иначе
						} else {
							//Использовать дальнюю атаку
							RangeAttack();
						}
					//Иначе развернуться
					} else {
						input = -targetDirection;
					}
				}
				Run ();
			}
		}
	}

	public override void Attack ()
	{
		int numberOfAttack = 0;
		numberOfAttack = Random.Range (0, 5);
			
		switch (numberOfAttack) {
		case 0:
			Debug.Log ("case 0");
			break;
		case 1:
			Debug.Log ("case 1");
			break;
		case 2:
			Debug.Log ("case 2");
			break;
		case 3:
			Debug.Log ("case 3");
			break;
		case 4:
			Debug.Log ("case 4");
			break;
		case 5:
			Debug.Log ("case 5");
			break;
		}

	}

	void RangeAttack() 
	{
		int numberOfAttack = 0;
		numberOfAttack = Random.Range (0, 5);

		switch (numberOfAttack) {
		case 0:
			Debug.Log ("case 0");
			break;
		case 1:
			Debug.Log ("case 1");
			break;
		case 2:
			Debug.Log ("case 2");
			break;
		case 3:
			Debug.Log ("case 3");
			break;
		case 4:
			Debug.Log ("case 4");
			break;
		case 5:
			Debug.Log ("case 5");
			break;
		}
	}

	//Тревога/Игрок замечен
	public override void Alert (GameObject player) {
		target = player;
		idle = false;
	}
}
