﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCrossroads : Tile {
	//Позиция тайла на уровне
	public int tilePositionOnLevel = 0;
	//Количество тайлов 
	public int TileNumber = 5;

	TileGenerator tileGeneratorInstance = new TileGenerator();

	void Start () {
		
	}
}
