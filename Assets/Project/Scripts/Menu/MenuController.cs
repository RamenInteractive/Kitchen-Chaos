using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public int currentScreen;
    public AudioClip titleBGM;

    private AudioSource source;
    MenuScreen[] screens;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        screens = GetComponentsInChildren<MenuScreen>(true);
        GameObject info = GameObject.Find("GameInfo");
        if (info != null) {
            GameInfo gi = info.GetComponent<GameInfo>();
            if (gi.gameOver) {
                SetScreen(2);
            }
            else {
                SetScreen(0);
            }
        }
        else {
            SetScreen(0);
        }
        source.Play();
    }

    public void SetScreen(int id) {
        if (id >= 0) {
            for (int i = 0; i < screens.Length; i++) {
                screens[i].gameObject.SetActive(i == id);
            }
        }
        switch(id) {
            case 0:
                source.clip = titleBGM;
                break;
            case 2:
                //Application.Quit();
                break;
            default:
                source.clip = titleBGM;
                break;
        }
    }

    // Update is called once per frame
    void Update () {
	}
}
