using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	[HideInInspector]
	public Vector2 startPosition;
	[HideInInspector]
	public Vector2 endPosition;

	public int complexity = 0;
}
