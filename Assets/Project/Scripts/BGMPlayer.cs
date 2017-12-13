using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour {
    public AudioClip startClip;
    public AudioClip loopClip;

    public AudioSource starter;
    public AudioSource looper;

    private bool playing;

	// Use this for initialization
	void Start () {
        starter.clip = startClip;
        looper.clip = loopClip;
        playing = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startMusic() {
        if(!playing) {
            starter.PlayDelayed(0.25f);
            looper.PlayDelayed(2.53f);
            playing = true;
        }
    }

    public void pauseMusic() {
        looper.Pause();
    }

    public void resumeMusic() {
        looper.Play();
    }
}
