using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour {

    public List<int> number;
    public List<int> guess;

    string gameId = "2951524";
    bool testMode = false;

    float timeTaken = 0.0f;
    public GameObject InGameClock;
    public GameObject EndGameClock;
    public GameObject Camera;

    CameraScript camScript;

    public AtttemptDisplay attemptDisplay;

    public bool running;
    public bool runningClock;

    ResultsManager RM;
    DigitManager DM;
    Logger log;
    ClockManager cm;
    ClockManager endCM;
    HistoryManager hm;

    int attempts = 0;
    int attempts_left = 0;
    int attempts_right = 0;

    bool won=false;

    void fix_attempts() {
        if (attempts < 10) {
            attempts_right = attempts;
            attempts_left = 0;
        } else {
            attempts_left = (int)System.Math.Floor((double)attempts / 10.0);
            attempts_right = attempts - (attempts_left*10);
        }
    }

    // Use this for initialization
    void Start() {
        number = new List<int>();
        guess = new List<int>();
        RM = GetComponent<ResultsManager>();
        DM = GetComponent<DigitManager>();
        log = GetComponent<Logger>();
        cm = InGameClock.GetComponent<ClockManager>();
        endCM = EndGameClock.GetComponent<ClockManager>();
        attemptDisplay = GetComponent<AtttemptDisplay>();
        camScript = Camera.GetComponent<CameraScript>();
        hm = GetComponent<HistoryManager>();
        Advertisement.Initialize(gameId, testMode);
        running = false;
    }

    // Update is called once per frame
    void Update() {
        DM.won = won;
        if (running == false && won == false) {
            NewGame();
        }
        if (DM.NumberSelected == true && running == true) {
            if (runningClock == false) {
                runningClock = true;
            }
            if (attempts > 50) {
                NewGame();
            }
            guess = DM.currentNumber;
            int balls = Factory.GetBalls(guess, number);
            int strikes = Factory.GetStrikes(guess, number);
            won = RM.Display(strikes, balls);
            attempts += 1;
            fix_attempts();
            log.AddResult(guess, strikes, balls);
            if (won == true) {
                int points = ((int)Mathf.Floor(100 - (attempts + (0.25f* timeTaken))));
                if (points < 0) {
                    points = 0;
                }
                attemptDisplay.Display(attempts_left, attempts_right, (int)Mathf.Floor(timeTaken), points);
                running = false;
                camScript.SwitchScreen(-1);
                hm.AddNewEntry(attempts_left, attempts_right, cm.timeInSeconds, guess, points);
            } else {
                DM.currentNumber = new List<int>();
                DM.NumberSelected = false;
            }
        }
        if (running == true && runningClock==true) {
            timeTaken += Time.deltaTime;
            cm.timeInSeconds = (int)Mathf.Floor(timeTaken);
            cm.DisplayTime();
            
        }
    }

    public void NewGame() {

        cm.Hide();
        if (Advertisement.IsReady("video") && DisableAds.advertise == true) {
            Advertisement.Show("video");
        }
        runningClock = false;
        log.ClearLogs();
        attempts = 0;
        RM.Display(0, 0);
        DM.resetLastNumbers();
        DM.ResetNumbers();
        DM.clearNumbersPressed();
        number = Factory.GetNumber();
        running = true;
        attemptDisplay.Hide();
        DM.currentNumber = new List<int>();
        DM.NumberSelected = false;
        timeTaken = 0;
        Debug.Log(number[0] + " " + number[1] + " " +  number[2] + " " + number[3]);

    }
}
