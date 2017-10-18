using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {
    // The time the day started
    private float dayStartTime = 0;
    private float dayTime = 0;

    private float difficulty = 0; // NOTE: 0.1 difficulty = 1 extra modifier
    private float difficultyMod = 0;

    private int daysCleared = 0;
    private int score = 0;
    private float dayProgress = 0; // 0 = morning; 0.5 = midday; 1 = night;

    public Light sun;
    public TicketGen ticketBoard;
    public Text displayText;
    public Text orderCompletionText;

	// Use this for initialization
	void Start () {
        dayStartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        dayTime = (Time.time - dayStartTime) / 360;
	}

    private IEnumerator endDay() {
        daysCleared++;
        setDisplayText("Day " + daysCleared + " completed!\nScore: " + score);
        yield return new WaitForSeconds(4f);

        difficulty += 0.05f;
        if (difficulty >= 1f) {
            difficulty = 1f;
        }
    }

    private IEnumerator finishOrder() {
        score += Mathf.FloorToInt(100 + 1000 * difficulty * difficultyMod);
        orderCompletionText.text = "Order complete!";
        yield return new WaitForSeconds(3f);
        orderCompletionText.text = "";
    }

    private void spawnOrder() {
        ticketBoard.diffModifier = difficulty;
        ticketBoard.newOrder();
    }

    private void setDisplayText(string str) {
        displayText.text = str;
    }

    private IEnumerator startDay() {
        dayStartTime = Time.time;
        dayTime = 0;
        setDisplayText("Day " + (daysCleared + 1));
        yield return new WaitForSeconds(4f);
        setDisplayText("");
        StartCoroutine("orderLoop");
    }

    private IEnumerator orderLoop() {
        while (dayTime < 1) {
            if (dayTime > 0.2 && dayTime < 35) {
                difficultyMod = 1.32f;
            }
            else if (dayTime > 0.7 && dayTime < 0.9) {
                difficultyMod = 1.475f;
            }
            yield return new WaitForSeconds(Random.Range(10, 30 + 80 * (1 - difficulty * difficultyMod)));
            spawnOrder();
        }
        endDay();
    }
}
