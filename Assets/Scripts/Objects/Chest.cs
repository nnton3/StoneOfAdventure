using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

	public bool chestOpened = false;
	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	public void OpenChest () {
		anim.SetTrigger ("open");
		chestOpened = true;
	}

}
