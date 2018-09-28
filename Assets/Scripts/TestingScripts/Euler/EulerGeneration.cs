using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EulerGeneration : MonoBehaviour {
	
	void Start () {
		LevelGenerate ();
	}

	Vector2 wayPoint = new Vector2 ();
	public GameObject defaultTile;
	public Vector2 startPoint;
	public int lines;
	void LevelGenerate () {
		wayPoint = startPoint;
		for (int i = 0; i <= lines; i++ ) {
			GenerateLine ();
		}
	}

	public float tileDx;
	public float tileDy;
	public int columns;
	void GenerateLine () {
		wayPoint.x = startPoint.x;
		for (int j = 0; j <= columns; j++) {
			lineTileArray[j] = CreateTile (defaultTile);
			wayPoint.x += tileDx;
		}
		CreateRightBounds (lineTileArray);
		wayPoint.y -= tileDy;
	}

	GameObject[] lineTileArray = new GameObject[3];
	GameObject CreateTile (GameObject tile) {
		GameObject tileInstance = Instantiate (tile, wayPoint, Quaternion.identity);
		return tileInstance;
	}

	public GameObject rightBound;
	GameObject[][] lineArrays = new GameObject[3][];
	void CreateRightBounds (GameObject[] lineTileArray) {
		int arrayNumber = 0;
		for (int k = 0; k <= (lineTileArray.Length - 2); k++) {
			float chance = Random.Range (0, 2);
			if (chance > 0.5f) {
				Instantiate (rightBound, lineTileArray [k].transform);
				lineArrays [arrayNumber][k] = lineTileArray [k];
				arrayNumber += 1;
			} else {
				lineArrays [arrayNumber][k] = lineTileArray [k];
			}
		}
	}

	void CreateBottomBound () {
		for (int l = 0; l <= (lineArrays.Length - 1); l++) {
			int tileNotHaveBottomBound = Random.Range (0, lineArrays[l].Length);
			Debug.Log (tileNotHaveBottomBound);
		}
	}
}
