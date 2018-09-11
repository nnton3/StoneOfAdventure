﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour {

	TileGenerator tileGeneratorInstance = new TileGenerator();
	public Transform parentObject;

	void Start () {
		tileGeneratorInstance.SetStartPosition (transform.position);
		GenerateLevel ();
	}

	//Сколько всего будет тайлов без учета тайлов-переходом
	public int TileNumber = 5;
	public virtual void GenerateLevel () {
		//Создать начальный тайл
		CreateStartTile ();
		//Создатьтайлы уровня
		for (int i = 0; i < TileNumber; i++) {
			//Сгенерировать тайл для i-ой позиции
			SelectTile (i);
			CreateTransitionTile ();
		}
		//Создать конечный спрайт
		CreateEndTile ();
	}

	//Проверка: позиция зарезервирована под спец тайл или нет
	public virtual void SelectTile (int position) {
		//Если спец тайлов нет
		if (specialTilesSheet.Length == 0) {
			CreateDefaultTile ();
			return;
		}
		for (int tileNumber = 0; tileNumber < specialTilesSheet.Length; tileNumber++) {
			TileCrossroads tileScript = specialTilesSheet [tileNumber].GetComponent<TileCrossroads> ();
			Debug.Log ("spec pos = " + tileScript.tilePositionOnLevel + " and tile pos = " + position);
			//Если спец тайл должен находиться на этой позиции
			if (position == tileScript.tilePositionOnLevel) {
				CreateSpecialTile (tileNumber);
				Debug.Log ("special create");
				return;
			} 
		}
		//Иначе сгенерировать тайл с врагами
		CreateDefaultTile ();
		Debug.Log ("default create");
		return;
	}

	//Создать начальный тайл
	public GameObject startTile;

	public virtual void CreateStartTile () {
		if (startTile != null) {
			tileGeneratorInstance.CreateTile (startTile, parentObject);
		}
	}

	//Создать тайл с врагами
	public GameObject[] defaultTilesSheet;

	public virtual void CreateDefaultTile () {
		if (defaultTilesSheet [0] != null) {
			tileGeneratorInstance.CreateRandomTile (defaultTilesSheet, parentObject);
		}
	}

	//Создать спец тайл
	public GameObject[] specialTilesSheet;

	public virtual void CreateSpecialTile (int tileNumber) {
		if (specialTilesSheet [0] != null) {
			tileGeneratorInstance.CreateTile (specialTilesSheet[tileNumber], parentObject);
		}
	}

	//Создать тайл переход
	public GameObject[] transitionTilesSheet;

	public virtual void CreateTransitionTile () {
		if (transitionTilesSheet[0] != null) {
			tileGeneratorInstance.CreateRandomTile (transitionTilesSheet, parentObject);
		}
	}

	//Создать последний тайл
	public GameObject endTile;

	public virtual void CreateEndTile () {
		if (endTile != null) {
			tileGeneratorInstance.CreateTile (endTile, parentObject);
		}
	}
}