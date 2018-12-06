using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBlacksmithTiles : GameLevel {

	public override void GenerateLevel ()
	{
		parentObject = GameObject.Find ("BlacksmithBG").transform;
		CreateStartTile ();
		parentObject.gameObject.SetActive (false);
	}
}
