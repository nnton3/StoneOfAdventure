using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit {



	void Update () {
		//Если игрок не находится в состоянии атаки или оглушения
		if (CanAttack()) {
			//Управление
			if (Input.GetKeyDown (KeyCode.F)) {       //Атака мечом
				Attack ();
			}

			if (Input.GetKeyDown (KeyCode.R)) {       //Перекат
				Roll ();
			}

			if (Input.GetKeyDown (KeyCode.B)) {       //Блок
				UseShield ();
			}

			if (Input.GetKeyDown (KeyCode.P)) {       //Атака из лука
				PullBow ();
			}
		}	
			
		if (CanMove()) {
			inputX = Input.GetAxisRaw ("Horizontal");
			flipParam = inputX;
		}

		rb.velocity = new Vector2 (inputX * moveSpeed, inputY * moveSpeed);
		anim.SetBool ("run", Mathf.Abs (inputX) > 0.1f);
	}

	//Атака
	public override void Attack () {
		CheckBlock ();
		conditions.attack = true;
		anim.SetTrigger ("attack");
	}

	//Использовать перекат
	public bool invulnerabilityIsRedy = true;
	public float rollCD = 3f;

	void Roll() {
		//Если игрок не выполняет перекат
		if (!conditions.invulnerability && invulnerabilityIsRedy) {
			//Проверить на использование щита
			CheckBlock ();
			//Сделать перекат
			anim.SetTrigger ("roll");
			StartCoroutine ("ResetRollCheck");	
			conditions.EnableInvulnerability ();
		}
	}

	//КД на использование "Переката"
	IEnumerator ResetRollCheck () {
		invulnerabilityIsRedy = false;
		yield return new WaitForSeconds (rollCD);
		invulnerabilityIsRedy = true;
	}

	//Стрельба из лука
	void PullBow () {
		CheckBlock ();
		//Игрок в режиме атаки
		conditions.attack = false;
		anim.SetTrigger ("pullBow");
	}
		
	//Проверка на возможность что-либо делать
	public override bool CanMove ()
	{
		return base.CanMove ();
	}

	public override bool CanAttack ()
	{
		return base.CanAttack ();
	}
}
