using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubick_patron : Patron {

	bool hit;
    public override void TrueHit(Collider2D target)
    {
        //Если еще не было попадания
        if (!hit) {
			//Если попало во врагов
			if (target.CompareTag ("Enemy")) {
				HitTheMark ();
				//Восстановить здоровье
				target.GetComponent<Damage> ().Healing (attackPoints);
				//Если попало в героя
			} else if (target.CompareTag ("Player")) {
				if (!target.GetComponent<Conditions> ().invulnerability) {
					HitTheMark ();
					//Нанести урон
					target.GetComponent<Damage> ().DefaultDamage (attackPoints, input);
				}
			}
		}
	}

	//Уничтожить патрон
	public void DestroyPatron () {
		Destroy (gameObject);
	}

	//Зафиксировать попадание в цель
	void HitTheMark () {
		hit = true;
		anim.SetTrigger ("hit");
	}
}