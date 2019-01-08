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
                    if (targetRange < (attackRange - 0.5f) && ((targetDirection < 0f && direction > 0f) || (targetDirection > 0f && direction < 0f)))
                    {
                        //Атаковать вблизи
                        Attack();
                    } else if (targetRange > (attackRange + 1f) && ((targetDirection < 0f && direction > 0f) || (targetDirection > 0f && direction < 0f)))
                    {
                        //Дальняя атака
                        RangeAttack();
                    }
                    else
                        //Изменить направление движения
                        inputX = -targetDirection;
                }
                Run();
            }
        }
    }

    void RangeAttack()
    {
        conditions.attack = true;
        anim.SetTrigger("rangeAttack");
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
