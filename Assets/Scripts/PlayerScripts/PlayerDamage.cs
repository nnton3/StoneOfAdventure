﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : Damage {

	public override void DefaultDamage(float damage, int stunDirection) {

		bool backToTheEnemy = BackToTheEnemyCheck (stunDirection);

		//Если игрок поставил блок
		if (conditions.block) {
			//Если игрок стоит к врагу спиной
			if (backToTheEnemy) {
				//Нанести урон
				ReduceHP (damage);
			} else {
				//Если игрок стоит лицом к врагу
				if (!conditions.stun) {
					conditions.EnableStun (stunDirection);
					anim.SetTrigger ("blocked");
				}
			}
			//Если игрок не заблокировал и не использовал перекат
		} else if (!conditions.invulnerability) {
			ReduceHP (damage);
		}
		if (!conditions.invulnerability) {
			//Получить оглушение
			if (!conditions.stun) {
				conditions.EnableStun (stunDirection);
			}
		}
	}

	public override void CriticalDamage (float damage, int stunDirection, float criticalScale) {
		
		bool backToTheEnemy = BackToTheEnemyCheck (stunDirection);
		float criticalDamageValue = (damage * criticalScale);

		//Если игрок поставил блок
		if (conditions.block) {
			//Если игрок стоит к врагу спиной
			if (backToTheEnemy) {
				//Нанести урон
				ReduceHP (criticalDamageValue);
				//Получить оглушение
				if (!conditions.stun) {
					conditions.EnableStun (stunDirection);
				}
				//Анимация получения урона
			} else {
				//Если игрок стоит лицом к врагу
				if (!conditions.stun) {
					conditions.EnableStun (stunDirection);
				
					anim.SetTrigger ("blocked");
				}
			}
		//Если игрок не заблокировал и не использовал перекат
		} else if (!conditions.invulnerability) {
			ReduceHP (criticalDamageValue);
		}
	}
}
