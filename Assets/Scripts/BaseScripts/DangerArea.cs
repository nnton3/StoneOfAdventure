using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerArea : MonoBehaviour {

	alert enemieAlert;
	List<Unit> enemies = new List<Unit>();
	bool stackActivated;
    [SerializeField]
    int deadEnemies = 0;
    [SerializeField]
    int allEnemies = 0;

	void Start () {
		FindUnits ();
	}

	public void FindUnits () {
		foreach (Enemy enemy in enemies) {
			enemieAlert += enemy.Alert;
			allEnemies += 1;
		}
	}

	public void AddEnemie(Unit newEnemie) {
		enemies.Add (newEnemie);
	}

	//Если игрок вошел в зону видимости
	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			if (!stackActivated) {
				GameManager.EnableBattleMode(true);
				enemieAlert (other.gameObject);
				stackActivated = true;
			}
		}
	}

	//Зафиксировать потери среди врагов
	public void AddCorpse () {
		deadEnemies += 1;
		//Проверить остались ли живые враги
		StackIsDead (deadEnemies);
	}

	//Проверить остались ли живые враги 
	void StackIsDead (int corpses) {
		//Сравнить количество трупов с исходным количеством врагов в стаке
		if (corpses == allEnemies) {
			//Зафиксировать уничтожение врагов в этом стаке
            GameManager.EnableBattleMode(false);
        }
    }
}
