using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {
    public const float MINUTE_VALUE = 0.1f;

    public static GameTime START_DAY = new GameTime("10:00");
    public static GameTime END_DAY = new GameTime("23:00");

    public static GameTime START_ORDERS = START_DAY + 30;
    public static GameTime END_ORDERS = END_DAY - 30;

    public static GameTime LUNCH_RUSH_START = new GameTime("12:00");
    public static GameTime LUNCH_RUSH_END = new GameTime("14:00");

    public static GameTime DINNER_RUSH_START = new GameTime("17:30");
    public static GameTime DINNER_RUSH_END = new GameTime("20:00");

    public static Color CORRECT_COLOUR = new Color(53, 231, 12);
    public static Color INCORRECT_COLOUR = new Color(229, 23, 51);

    public const int MAX_LIVES = 3;

    public const int MAX_TIME_BTWN_ORDERS = 150;
    public const int MIN_TIME_BTWN_ORDERS = 10;

    public const float C_VAL = 3f;

    private const int STATUS_NO_ORDERS = 0;
    private const int STATUS_NORMAL = 1;
    private const int STATUS_LUNCH_RUSH = 2;
    private const int STATUS_DINNER_RUSH = 3;
    private const int STATUS_END_DAY = 4;
    private const int STATUS_OVERTIME = 5;

    private const float LUNCH_DIFFICULTY_MOD = 1.32f;
    private const float DINNER_DIFFICULTY_MOD = 1.475f;

    // current minute counter
    private float minuteCount = 0;

    private float difficulty = 0.1f; // NOTE: 0.1 difficulty = 1 extra modifier
    private float difficultyMod = 0;

    private int daysCleared = 0;
    private int score = 0;
    private int lives = MAX_LIVES;
    private GameTime dayTime = new GameTime();

    private int dayStatus = STATUS_NORMAL;

    private float bVal;

    private GameTime lastOrder;
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
        GameObject infoObj = GameObject.Find("GameInfo");
        if (infoObj != null) {
            GameInfo info = infoObj.GetComponent<GameInfo>();
            numPlayers = info.numPlayers;
            spawnPlayers(info);
        }
        else {
            numPlayers = 1; // test play
            curPlayers.Add(Instantiate(playerPrefab) as Player);
            curPlayers[0].transform.Translate(spawnPoints[0]);
            ControllerFactory.AddControllerToObj(curPlayers[0].gameObject, 0);
        }

        lives = MAX_LIVES;
        bVal = Mathf.Pow(MAX_TIME_BTWN_ORDERS * Mathf.Pow(0.75f, numPlayers - 1), C_VAL);
        StartCoroutine("startDay");
    }

    // Update is called once per frame
    void Update () {
        if(dayStatus != STATUS_END_DAY) {
            minuteCount += Time.deltaTime;
            if (minuteCount > MINUTE_VALUE) {
                dayTime.addMinutes((int)(minuteCount / MINUTE_VALUE));
                minuteCount %= MINUTE_VALUE;
                updateStatus();
                if(dayStatus != STATUS_NO_ORDERS && dayStatus != STATUS_OVERTIME) {
                    randomChanceOrder();
                }
            }
            updateClock();
        }
    }

    private IEnumerator startDay()
    {
        dayTime = START_DAY;
        dayStatus = STATUS_NO_ORDERS;
        lastOrder = START_DAY - MIN_TIME_BTWN_ORDERS;
        minuteCount = 0;
        timeDiff = new List<int>();
        yield return displayMessage("Day " + (daysCleared + 1), 4f);
    }

    private IEnumerator endDay() {
        ticketBoard.resetTickets();
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
    
    public IEnumerator finishOrder(int timeSpent) {
        float timeRemaining = 1 - Mathf.Pow(1 - (float)timeSpent / TicketGen.TICKET_DURATION, 1.5f);
        int pointVal = Mathf.FloorToInt(100 + 100 * difficulty * difficultyMod * timeRemaining);
        score += pointVal;
        orderCompletionText.color = CORRECT_COLOUR;
        orderCompletionText.text = "Order complete!\n+" + pointVal;
        yield return new WaitForSeconds(3f);
        orderCompletionText.text = "";
    }

    public IEnumerator failOrder() {
        orderCompletionText.color = INCORRECT_COLOUR;
        orderCompletionText.text = "Incorrect Order!";
        yield return new WaitForSeconds(3f);
        orderCompletionText.text = "";
    }

    public void loseLife() {
        lives -= 1;
        if(lives <= 0) {
            // lose life
            StartCoroutine(displayMessage("You lost the game but we haven't coded the ending yet", 3f));
        } else {
            StartCoroutine(displayMessage("You lost a life", 3f));
        }
    }

    private void spawnOrder() {
        timeDiff.Add(dayTime - lastOrder);
        lastOrder = new GameTime(dayTime);
        ticketBoard.diffModifier = difficulty * difficultyMod;
        ticketBoard.newOrder();
    }

    private void setDisplayText(string str) {
        displayText.text = str;
    }

    private void spawnPlayers(GameInfo info)
    {
        for(int i = 0; i < numPlayers; i++)
        {
            int controllerID = 0;
            switch(i) {
                case 0:
                    controllerID = info.player1Controller;
                    break;
                case 1:
                    controllerID = info.player2Controller;
                    break;
                case 2:
                    controllerID = info.player3Controller;
                    break;
                case 3:
                    controllerID = info.player4Controller;
                    break;
            }
            curPlayers.Add(Instantiate(playerPrefab) as Player);
            curPlayers[i].transform.Translate(spawnPoints[i]);
            ControllerFactory.AddControllerToObj(curPlayers[i].gameObject, controllerID);
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

    private void randomChanceOrder() {
        int last = dayTime - lastOrder;
        if(last >= MIN_TIME_BTWN_ORDERS) { 
            float maxf = 0.5f;
            float floor = maxf * difficulty * difficultyMod;
            float rand = Random.Range(floor, 1f);
            float catchup = (Mathf.Pow(last, C_VAL) / bVal) * (1 - rand);
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
                } else if (dayTime == LUNCH_RUSH_START - 30) {
                    StartCoroutine(displayMessage("Prepare for the Lunch Rush!", 2f));
                } else if (dayTime == DINNER_RUSH_START - 30) {
                    StartCoroutine(displayMessage("Prepare for the Dinner Rush!", 2f));
                }
                break;
            case STATUS_NO_ORDERS:
                if (dayTime >= START_ORDERS && dayTime < END_ORDERS) {
                    StartCoroutine(displayMessage("The Kitchen is now open!", 2f));
                    StartCoroutine(setStatus(STATUS_NORMAL));
                } else if (dayTime >= END_DAY) {
                    if (ticketBoard.numOrders() == 0) {
                        StartCoroutine(setStatus(STATUS_END_DAY));
                        StartCoroutine("endDay");
                    } else {
                        StartCoroutine(setStatus(STATUS_OVERTIME));
                    }
                }
                break;
            case STATUS_LUNCH_RUSH:
                if (dayTime >= LUNCH_RUSH_END) {
                    StartCoroutine(displayMessage("Lunch Rush Completed", 2f));
                    StartCoroutine(setStatus(STATUS_NORMAL));
                }
                break;
            case STATUS_DINNER_RUSH:
                if (dayTime >= DINNER_RUSH_END) {
                    StartCoroutine(displayMessage("Dinner Rush Completed", 2f));
                    StartCoroutine(setStatus(STATUS_NORMAL));
                }
                break;
            case STATUS_OVERTIME:
                if(ticketBoard.numOrders() == 0) {
                    StartCoroutine(setStatus(STATUS_END_DAY));
                    StartCoroutine("endDay");
                } else {
                    score -= Mathf.FloorToInt(difficulty * 10);
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
        } else if (status == STATUS_OVERTIME) {
            clockText.color = new Color(1f, 0f, 0f);
            yield return displayMessage("Overtime!!", 3f);
        } else {
            clockText.color = new Color(1f, 1f, 1f);
            difficultyMod = 1f;
        }
    }

    private void updateClock() {
        clockText.text = dayTime.ToString();
    }

    public GameTime getTime()
    {
        return new GameTime(dayTime);
    }
}
