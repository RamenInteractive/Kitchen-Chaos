using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour {
    public AudioClip correctClip;
    public AudioClip incorrectClip;
    public AudioClip scrollClip;
    public AudioClip selectClip;
    public AudioClip cancelClip;
    public AudioClip popClip;
    public AudioClip malletGliss;
    public AudioClip whistleClip;

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

    public void playScroll() {
        source.clip = scrollClip;
        source.Play();
    }

    public void playSelect() {
        source.clip = selectClip;
        source.Play();
    }

    public void playCancel() {
        source.clip = cancelClip;
        source.Play();
    }

    public void playPop() {
        source.clip = popClip;
        source.Play();
    }

    public void playMalletGliss() {
        source.clip = malletGliss;
        source.Play();
    }

    public void playWhistle() {
        source.clip = whistleClip;
        source.Play();
    }
}
