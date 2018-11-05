using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit {

	public bool onLadder = false;
	public bool ladderBottomLine = false;
	public bool onPC = false;
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
		//Управление для ПК
		if (CanMove()) {
			if (onPC) {
				inputX = (int)Input.GetAxisRaw ("Horizontal");
				inputY = (int)Input.GetAxisRaw ("Vertical");
			}
			flipParam = inputX;
		}

		if (!onLadder) {
			rb.velocity = new Vector2 (inputX * moveSpeed, rb.velocity.y);
			anim.SetBool ("run", Mathf.Abs (inputX) > 0.1f);
		} else {
			if (ladderBottomLine) {
				rb.velocity = new Vector2 (inputX * moveSpeed, (Mathf.Clamp (inputY, 0, 1)) * moveSpeed);
				anim.SetBool ("up_down", false);
			} else {
				rb.velocity = new Vector2 (0f, inputY * moveSpeed);
				anim.SetBool ("up_down", Mathf.Abs (inputY) > 0.1f);
			}
		}
	}

	///Управление для Android
	public void HorizontalMoves (int input) {
		if (CanMove ()) {
			inputX = input;
		}
	}

	///Управление для Android
	public void VerticalMoves (int input) {
		if (CanMove ()) {
			inputY = input;
		}
	}

	//Атака
	public override void Attack () {
		if (CanAttack ()) {
			CheckBlock ();
			conditions.attack = true;
			anim.SetTrigger ("attack");
		}
	}

	//Использовать перекат
	public bool invulnerabilityIsRedy = true;
	public float rollCD = 3f;

	public void Roll() {
		if (CanAttack ()) {
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
		conditions.attack = true;
		anim.SetTrigger ("pullBow");
	}
}
