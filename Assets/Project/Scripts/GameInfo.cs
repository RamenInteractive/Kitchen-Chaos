using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameInfo : MonoBehaviour {
    public int numPlayers;
    public int player1Controller;
    public int player2Controller;
    public int player3Controller;
    public int player4Controller;
    private void Start() {
        DontDestroyOnLoad(gameObject);
    }
}
