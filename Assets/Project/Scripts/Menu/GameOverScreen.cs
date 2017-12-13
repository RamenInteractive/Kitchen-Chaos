using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class GameOverScreen : MenuScreen {
    public Text scoreText;
    public Text newRecordText;
    void Start() {
        GameObject info = GameObject.Find("GameInfo");
        int score = 0;
        if (info != null) {
            score = info.GetComponent<GameInfo>().score;
        }
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore) {
            PlayerPrefs.SetInt("HighScore", score);
            newRecordText.gameObject.SetActive(true);
        }
        scoreText.text = score.ToString();
    }
    void Update() {
        if (GetAnyAccept()) {
            MenuController c = this.GetComponentInParent<MenuController>();
            c.SetScreen(0);
        }
    }
}
