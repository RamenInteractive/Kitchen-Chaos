using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public int currentScreen;
    MenuScreen[] screens;

    // Use this for initialization
    void Start () {
        screens = GetComponentsInChildren<MenuScreen>(true);
        SetScreen(0);
    }

    public void SetScreen(int id) {
        if (id >= 0) {
            for (int i = 0; i < screens.Length; i++) {
                screens[i].gameObject.SetActive(i == id);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	}
}
