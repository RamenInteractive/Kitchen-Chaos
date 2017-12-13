using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour {
    public AudioClip startClip;
    public AudioClip loopClip;

    private AudioSource starter;
    private AudioSource looper;

	// Use this for initialization
	void Start () {
        starter = transform.Find("Start").GetComponent<AudioSource>();
        looper = transform.Find("Loop").GetComponent<AudioSource>();
        starter.clip = startClip;
        looper.clip = loopClip;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startMusic() {
        starter.PlayDelayed(0.25f);
        looper.PlayDelayed(2.53f);
    }

    public void pauseMusic() {
        looper.Pause();
    }

    public void resumeMusic() {
        looper.Play();
    }
}
