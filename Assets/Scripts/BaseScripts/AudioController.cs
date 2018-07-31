using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	AudioSource audioSource;

	public AudioClip[] effects;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.J)) {
			audioSource.PlayOneShot (effects[0]);
		}
	}
}
