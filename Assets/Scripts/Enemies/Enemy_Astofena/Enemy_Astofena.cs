using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Astofena : Enemy {

    private void Update()
    {
        //Если юнит ожил
        if (!idle)
        {
            //Если он может двигаться
            if (CanMove())
            {
                //Найти цель
                FindTarget();
                flipParam = inputX;

                //Если юнит может атаковать
                if (CanAttack())
                {
                    Debug.Log("can attack");
                    if (targetRange < (attackRange - 0.5f) && ((targetDirection < 0f && direction > 0f) || (targetDirection > 0f && direction < 0f)))
                    {
                        Debug.Log("attack");
                        //Атаковать вблизи
                        Attack();
                    } /*else if (targetRange > (attackRange + 1f) && ((targetDirection < 0f && direction > 0f) || (targetDirection > 0f && direction < 0f)))
                    {
                        Debug.Log("range attack");
                        //Дальняя атака
                        RangeAttack();
                    }*/
                    else
                        //Изменить направление движения
                        inputX = -targetDirection;
                }
                Run();
            }
        }
    }

    public override void Attack()
    {
        conditions.attack = true;
        //Остановиться
        inputX = 0;
        anim.SetTrigger("attack");
    }

    void RangeAttack()
    {
        conditions.attack = true;
        //Остановиться
        inputX = 0;
        anim.SetTrigger("attack2");
    }

    public override void Alert(GameObject player)
    {
        base.Alert(player);
        idle = false;
        StartCoroutine("AbilityTimer");
    }

    IEnumerator AbilityTimer ()
    {
        yield return new WaitForSeconds(15f);
        StartCoroutine("AbilityTimer");

    }

    void SelectAbility ()
    {

    }
}
