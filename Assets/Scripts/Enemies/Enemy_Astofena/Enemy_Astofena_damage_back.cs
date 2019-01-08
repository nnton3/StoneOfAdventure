using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Astofena_damage_back : Damage {

	void Start () {
        unit = GetComponentInParent<Unit>();
        conditions = GetComponentInParent<Conditions>();
        anim = GetComponentInParent<Animator>();	
	}

    public override void DefaultDamage(float damage, int direction)
    {
        Debug.Log("work");
        ReduceHP(damage);
        conditions.EnableStun(direction);
    }

    public override void ReduceHP(float damage)
    {
        //Если урон больше, чем сумма брони и здоровья
        if ((unit.health + unit.armor) <= damage)
        {
            conditions.UnitDie();
            //Юнит погибает
            unit.health -= damage;
            return;
        }
        //Иначе если есть броня, то вычесть урон сначала из брони
        if (unit.armor != 0f)
        {
            //Если брони достаточно, чтобы, блокировать весь урон, то из здоровья ничего не вычитаем
            if (unit.armor >= damage)
            {
                unit.armor -= damage;
                anim.SetTrigger("attackable_back");
                return;
                //Иначе блокировать часть урона за счет брони, а остаток вычесть из здоровья
            }
            else
            {
                damage -= unit.armor;
                unit.armor = 0f;
                unit.health -= damage;
                anim.SetTrigger("attackable_back");
            }
        }
        else
        {
            damage -= unit.armor;
            unit.health -= damage;
            anim.SetTrigger("attackable_back");
        }
    }
}
