using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesList : MonoBehaviour {

	public GameObject[] Tier1Enemie;  //Самые слабые зомби 
	public GameObject[] Tier2Enemie;  //Кенни и брайан
	public GameObject[] Tier3Enemie;  //Жерон
	public GameObject[] Tier4Enemie;  //Рубик и инвокер

	//Выбрать случайного моба из определенного тира
	public GameObject GetRandomEnemieFromTier (int tierNumber) {
		switch (tierNumber) {
		case 1:
			return Tier1Enemie [Random.Range (0, Tier1Enemie.Length)];
		case 2:
			return Tier2Enemie [Random.Range (0, Tier2Enemie.Length)];
		case 3:
			return Tier3Enemie [Random.Range (0, Tier3Enemie.Length)];
		case 4:
			return Tier4Enemie [Random.Range (0, Tier4Enemie.Length)];
		default:
			return Tier1Enemie [0];
		}
	}

	//Набор готовых стаков мобов
	int[][] enemiesPatternArray = new int[][]
	{
		new int [] {1, 1},
		new int [] {1, 1, 1},
		new int [] {2, 1},
		new int [] {2, 1, 1},
		new int [] {2, 2},
		new int [] {4, 1, 1},
		new int [] {2, 4},
		new int [] {3},
		new int [] {3, 1},
	};

	//Выбрать паттерн
	public int[] ChooseEnemieStackPattern (int complexityIndex) {
		int chosenPattern = Random.Range ((0 + complexityIndex), (2 + complexityIndex));
		if (chosenPattern > 6) {
			return enemiesPatternArray [6];
		}
		return enemiesPatternArray [chosenPattern];
	}
}
