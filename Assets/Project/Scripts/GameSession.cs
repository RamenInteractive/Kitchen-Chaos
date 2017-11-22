using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {
    public const float MINUTE_VALUE = 0.5f;

    public const int START_DAY = 1000;
    public const int END_DAY = 2300;

    public const int START_ORDERS = 1030;
    public const int END_ORDERS = 2230;

    public const int LUNCH_RUSH_START = 1200;
    public const int LUNCH_RUSH_END = 1400;

    public const int DINNER_RUSH_START = 1730;
    public const int DINNER_RUSH_END = 2000;

    public const int MAX_TIME_BTWN_ORDERS = 60;
    public const int MIN_TIME_BTWN_ORDERS = 10;

    public const float C_VAL = 3f;

    private const int STATUS_NO_ORDERS = 0;
    private const int STATUS_NORMAL = 1;
    private const int STATUS_LUNCH_RUSH = 2;
    private const int STATUS_DINNER_RUSH = 3;
    private const int STATUS_END_DAY = 4;

    private const float LUNCH_DIFFICULTY_MOD = 1.32f;
    private const float DINNER_DIFFICULTY_MOD = 1.475f;

    // current minute counter
    private float minuteCount = 0;

    private float difficulty = 0.1f; // NOTE: 0.1 difficulty = 1 extra modifier
    private float difficultyMod = 0;

    private int daysCleared = 0;
    private int score = 0;
    private int dayTime = 0; // hhmm format

    private int dayStatus = STATUS_NORMAL;

    private int lastOrder = 0;
    private List<int> timeDiff;

    private int numPlayers;
    private List<Player> curPlayers;

    public Player playerPrefab;
    public Light sun;
    public TicketGen ticketBoard;
    public Text displayText;
    public Text orderCompletionText;
    public Text clockText;

    public static Vector3[] spawnPoints = { new Vector3(5, 1, 2), new Vector3(5, 1, 7), new Vector3(5, 1, -2), new Vector3(5, 1, -7) };

	// Use this for initialization
	void Start () {
        curPlayers = new List<Player>();
        numPlayers = 1;
        spawnPlayers();
        StartCoroutine("startDay");
    }

    // Update is called once per frame
    void Update () {
        if(dayStatus != STATUS_END_DAY) {
            minuteCount += Time.deltaTime;
            if (minuteCount > MINUTE_VALUE) {
                incrementTime((int)(minuteCount / MINUTE_VALUE));
                minuteCount %= MINUTE_VALUE;
                updateStatus();
                if(dayStatus != STATUS_NO_ORDERS) {
                    randomChanceOrder();
                }
            }
            updateClock();
        }
    }

    private IEnumerator endDay() {
        daysCleared++;
        float sum = 0;
        foreach(int t in timeDiff) {
            sum += t;
        }
        float avg = sum / timeDiff.Count;
        Debug.Log("Orders made: " + timeDiff.Count + " | Tavg: " + avg + " | Tmed: " + timeDiff[timeDiff.Count / 2]);
        yield return displayMessage("Day " + daysCleared + " completed!\nScore: " + score, 4f);

        if (difficulty < 1f) {
            difficulty += 0.15f;
        }

        StartCoroutine("startDay");
    }

    public IEnumerator finishOrder() {
        score += Mathf.FloorToInt(100 + 1000 * difficulty * difficultyMod * (daysCleared + 1));
        orderCompletionText.text = "Order complete!";
        yield return new WaitForSeconds(3f);
        orderCompletionText.text = "";
    }

    private void spawnOrder() {
        timeDiff.Add(dayTime - lastOrder);
        lastOrder = dayTime;
        ticketBoard.diffModifier = difficulty;
        ticketBoard.newOrder();
    }

    private void setDisplayText(string str) {
        displayText.text = str;
    }

    private void spawnPlayers()
    {
        for(int i = 0; i < numPlayers; i++)
        {
            curPlayers.Add(Instantiate(playerPrefab) as Player);
            curPlayers[i].transform.Translate(spawnPoints[i]);
        }

        switch(numPlayers)
        {
            case 2:
                curPlayers[0].transform.GetChild(0).GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 0.5f);
                curPlayers[1].transform.GetChild(0).GetComponent<Camera>().rect = new Rect(0, 0, 1, 0.5f);
                break;
            case 3:
                curPlayers[0].transform.GetChild(0).GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                curPlayers[1].transform.GetChild(0).GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                curPlayers[2].transform.GetChild(0).GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
                break;
            case 4:
                curPlayers[0].transform.GetChild(0).GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                curPlayers[1].transform.GetChild(0).GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                curPlayers[2].transform.GetChild(0).GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
                curPlayers[3].transform.GetChild(0).GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                break;
            default:
                break;
        }
    }

    private IEnumerator startDay() {
        dayTime = START_DAY;
        dayStatus = STATUS_NO_ORDERS;
        lastOrder = START_DAY - MIN_TIME_BTWN_ORDERS;
        minuteCount = 0;
        timeDiff = new List<int>();
        yield return displayMessage("Day " + (daysCleared + 1), 4f);
    }

    private void randomChanceOrder() {
        int last = dayTime - lastOrder;
        if(last >= MIN_TIME_BTWN_ORDERS) { 
            float maxf = 0.5f;
            float floor = maxf * difficulty * difficultyMod;
            float rand = Random.Range(floor, 1f);
            float catchup = (Mathf.Pow(last, C_VAL) / Mathf.Pow(MAX_TIME_BTWN_ORDERS, C_VAL)) * (1 - rand);
            if((rand + catchup) > 0.984f) {
                spawnOrder();
            }
            //Debug.Log("Last: " + lastOrder + " | Time: " + dayTime + " | f: " + floor + " | r: " + rand + " | c: " + catchup + " | val: " + (rand + catchup));
        }
    }

    private void updateStatus() {
        switch (dayStatus) {
            case STATUS_NORMAL:
                if (dayTime >= LUNCH_RUSH_START && dayTime < LUNCH_RUSH_END) {
                    StartCoroutine(setStatus(STATUS_LUNCH_RUSH));
                } else if (dayTime >= DINNER_RUSH_START && dayTime < DINNER_RUSH_END) {
                    StartCoroutine(setStatus(STATUS_DINNER_RUSH));
                } else if (dayTime >= END_ORDERS) {
                    StartCoroutine(setStatus(STATUS_NO_ORDERS));
                }
                break;
            case STATUS_NO_ORDERS:
                if (dayTime >= START_ORDERS && dayTime < END_ORDERS) {
                    StartCoroutine(setStatus(STATUS_NORMAL));
                } else if (dayTime >= END_DAY) {
                    StartCoroutine(setStatus(STATUS_END_DAY));
                    StartCoroutine("endDay");
                }
                break;
            case STATUS_LUNCH_RUSH:
                if (dayTime >= LUNCH_RUSH_END) {
                    StartCoroutine(setStatus(STATUS_NORMAL));
                }
                break;
            case STATUS_DINNER_RUSH:
                if (dayTime >= DINNER_RUSH_END) {
                    StartCoroutine(setStatus(STATUS_NORMAL));
                }
                break;
            default:
                break;
        }
    }

    private IEnumerator displayMessage(string text, float delay) {
        setDisplayText(text);
        yield return new WaitForSeconds(delay);
        setDisplayText("");
    }

    private IEnumerator setStatus(int status) {
        dayStatus = status;
        if (status == STATUS_LUNCH_RUSH) {
            difficultyMod = LUNCH_DIFFICULTY_MOD;
            yield return displayMessage("Lunch Rush!", 3f);
        } else if (status == STATUS_DINNER_RUSH) {
            difficultyMod = DINNER_DIFFICULTY_MOD;
            yield return displayMessage("Dinner Rush!", 3f);
        } else {
            difficultyMod = 1f;
        }
    }

    private void updateClock() {
        int hour = dayTime / 100;
        int min = dayTime % 100;
        int dHour = hour % 12;
        clockText.text = (dHour == 0 ? 12 : dHour) + ":" + (min < 10 ? "0" + min : min.ToString()) + (hour >= 12 ? "PM" : "AM");
    }

    private int incrementTime(int increment) {
        dayTime += increment;
        if(dayTime % 100 >= 60) {
            dayTime += 40;
            lastOrder += 40;
        }
        return dayTime;
    }
}
