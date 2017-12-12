using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour {
    public AudioClip correctClip;
    public AudioClip incorrectClip;

    private AudioSource source;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playCorrect() {
        source.clip = correctClip;
        source.Play();
    }

    public void playIncorrect() {
        source.clip = incorrectClip;
        source.Play();
    }
}
