using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerEntryScreen : MenuScreen {
    private List<int> players;

    public GameObject gameInfoPrefab;

    public Text player1Text;
    public Text player1ControllerName;
    public Text player2Text;
    public Text player2ControllerName;
    public Text player3Text;
    public Text player3ControllerName;
    public Text player4Text;
    public Text player4ControllerName;

    public Text loadingText;

    private void Start() {
        players = new List<int>();
    }

    private void Update() {
        if (GetAnyAccept() && players.Count < 4) {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !players.Contains(0)) {
                players.Add(0);
            }
            if ((Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && !players.Contains(1)) {
                players.Add(1);
            }
            if ((Input.GetKeyDown(KeyCode.Joystick2Button1) || Input.GetKeyDown(KeyCode.Joystick2Button0)) && !players.Contains(2)) {
                players.Add(2);
            }
            if ((Input.GetKeyDown(KeyCode.Joystick3Button1) || Input.GetKeyDown(KeyCode.Joystick3Button0)) && !players.Contains(3)) {
                players.Add(3);
            }
            if ((Input.GetKeyDown(KeyCode.Joystick4Button1) || Input.GetKeyDown(KeyCode.Joystick4Button0)) && !players.Contains(4)) {
                players.Add(4);
            }
            if ((Input.GetKeyDown(KeyCode.Joystick5Button1) || Input.GetKeyDown(KeyCode.Joystick5Button0)) && !players.Contains(5)) {
                players.Add(5);
            }
            if ((Input.GetKeyDown(KeyCode.Joystick6Button1) || Input.GetKeyDown(KeyCode.Joystick6Button0)) && !players.Contains(6)) {
                players.Add(6);
            }
            if ((Input.GetKeyDown(KeyCode.Joystick7Button1) || Input.GetKeyDown(KeyCode.Joystick7Button0)) && !players.Contains(7)) {
                players.Add(7);
            }
            if ((Input.GetKeyDown(KeyCode.Joystick8Button1) || Input.GetKeyDown(KeyCode.Joystick8Button0)) && !players.Contains(8)) {
                players.Add(8);
            }
            UpdatePlayerTexts();
        }

        if (GetAnyStart() && players.Count > 0) {
            loadingText.gameObject.SetActive(true);
            GameObject gameInfoObj = Instantiate(gameInfoPrefab);
            GameInfo info = gameInfoObj.GetComponent<GameInfo>();
            info.numPlayers = players.Count();
            info.name = "GameInfo";
            for (int i = 0; i < info.numPlayers; i++) {
                switch(i) {
                    case 0:
                        info.player1Controller = players[i];
                        break;
                    case 1:
                        info.player2Controller = players[i];
                        break;
                    case 2:
                        info.player3Controller = players[i];
                        break;
                    case 3:
                        info.player4Controller = players[i];
                        break;
                }
            }
            SceneManager.LoadScene("MainScene");
        }
    }
    private void UpdatePlayerTexts() {
        string[] controllerNames = Input.GetJoystickNames();
        for (int i = 0; i < players.Count; i++) {
            switch(i) {
                case 0:
                    player1Text.gameObject.SetActive(true);
                    if (players[i] == 0) {
                        player1ControllerName.text = "Keyboard";
                    }
                    else {
                        player1ControllerName.text = controllerNames[players[i] - 1];
                    }
                    player1ControllerName.gameObject.SetActive(true);
                    break;
                case 1:
                    player2Text.gameObject.SetActive(true);
                    if (players[i] == 0) {
                        player2ControllerName.text = "Keyboard";
                    }
                    else {
                        player2ControllerName.text = controllerNames[players[i] - 1];
                    }
                    player2ControllerName.gameObject.SetActive(true);
                    break;
                case 2:
                    player3Text.gameObject.SetActive(true);
                    if (players[i] == 0) {
                        player3ControllerName.text = "Keyboard";
                    }
                    else {
                        player3ControllerName.text = controllerNames[players[i] - 1];
                    }
                    player3ControllerName.gameObject.SetActive(true);
                    break;
                case 3:
                    player4Text.gameObject.SetActive(true);
                    if (players[i] == 0) {
                        player4ControllerName.text = "Keyboard";
                    }
                    else {
                        player4ControllerName.text = controllerNames[players[i] - 1];
                    }
                    player4ControllerName.gameObject.SetActive(true);
                    break;
            }
        }
    }
    private bool GetAnyStart()
    {
        string[] controllerNames = Input.GetJoystickNames();
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == 0)
            {
                return Input.GetKey(KeyCode.Return);
            }
            else
            {
                int offset = players[i] * 20;
                if (controllerNames[players[i] - 1].Equals("Wireless Controller"))
                {
                    return Input.GetKey(KeyCode.JoystickButton9 + offset);
                }
                else
                {
                    return Input.GetKey(KeyCode.JoystickButton7 + offset);
                }
            }
        }
        return false;
    }
}
