using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_zombyZheron : Zombie {

	public float morgenstern_MinimumAttackRange = 5f;

	void Update () {
		//Если зомби ожил
		if (!idle) {
			//Если зомби может двигаться
			if (CanMove ()) {
			
				//Определение местоположения игрока
				FindTarget ();
				flipParam = inputX;
                Debug.Log("can move");
                //Если можно атаковать
                if (CanAttack ()) {
                    Debug.Log("can attack");
                    //Если игрок в зоне досягаемости и в зоне видимости
                    if ((targetRange < (attackRange - 0.5f)) && ((targetDirection < 0f && direction > 0f) || (targetDirection > 0f && direction < 0f)))
                    {
                        Debug.Log("change attack");
                        //Выбрать тип атаки:
                        //Если в области атаки моргенштерном
                        if (targetRange >= morgenstern_MinimumAttackRange)
                        {
                            Debug.Log("mille attack");
                            //Атаковать моргенштерном
                            Attack();
                            //Иначе
                        }
                        else
                        {
                            Debug.Log("shield attack");
                            //Сделать толчок щитом
                            JerkWithShield();
                        }
                    }
                    else
                    {
                        Debug.Log("change direction");
                        //Изменить направление движения
                        inputX = -targetDirection;
                    }
				}
			}
			Run ();
		}
	}

	//Атака
	public override void Attack() {
		//Остановиться
		inputX = 0;
		//Выйти из стана
		conditions.DisableStun();
		//Убрать блок если юнит в блоке
		CheckBlock ();
		//Перейти в состояние атаки
		conditions.attack = true;
		anim.SetTrigger ("attack");
	}

	//Рывок
	void JerkWithShield () {
        //Остановиться
        inputX = 0;
        //Выйти из стана
        conditions.DisableStun();
        //Убрать блок если юнит в блоке
        CheckBlock();
        conditions.attack = true;
		anim.SetTrigger ("attack2");
	}

}
