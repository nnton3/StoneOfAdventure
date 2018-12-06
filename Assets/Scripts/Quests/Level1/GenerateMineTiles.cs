using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMineTiles : GameLevel {

	public override void GenerateLevel ()
	{
		parentObject = GameObject.Find ("MineBG").transform;
		base.GenerateLevel();
		parentObject.gameObject.SetActive (false);
	}
}
