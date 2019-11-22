using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour {

    public int timeInSeconds;

    int totalSeconds;
    int totalMinutes;
    int totalHours;

    int seconds_left;
    int seconds_right;

    int minutes_left;
    int minutes_right;

    int hours_left;
    int hours_right;

    public GameObject secondsright;
    public GameObject secondsleft;
    public GameObject minutesright;
    public GameObject minutesleft;
    public GameObject hoursleft;
    public GameObject hoursright;
    public GameObject columnL;
    public GameObject columnR;

    public Sprite ColSprite;

    SpriteRenderer secR;
    SpriteRenderer secL;
    SpriteRenderer minR;
    SpriteRenderer minL;
    SpriteRenderer hrL;
    SpriteRenderer hrR;
    SpriteRenderer clL;
    SpriteRenderer clR;

    // Use this for initialization
    void Awake() {
        secL = secondsleft.GetComponent<SpriteRenderer>();
        secR = secondsright.GetComponent<SpriteRenderer>();
        minL = minutesleft.GetComponent<SpriteRenderer>();
        minR = minutesright.GetComponent<SpriteRenderer>();
        hrL = hoursleft.GetComponent<SpriteRenderer>();
        hrR = hoursright.GetComponent<SpriteRenderer>();
        clL = columnL.GetComponent<SpriteRenderer>();
        clR = columnR.GetComponent<SpriteRenderer>();
        Hide();
    }

    void SetTime() {
        totalHours = (int)Mathf.Floor(timeInSeconds / 3600);
        totalMinutes = (int)Mathf.Floor((timeInSeconds / 60) - (totalHours * 60));
        totalSeconds = (int)Mathf.Floor(timeInSeconds - ((totalMinutes * 60) + (totalHours * 3600)));
    }


    void SetDigits() {
        if (totalHours < 10) {
            hours_right = totalHours;
            hours_left = 0;
        } else {
            hours_left = (int)System.Math.Floor((double)totalHours / 10.0);
            hours_right = totalHours - (hours_left * 10);
        }
        if (totalMinutes < 10) {
            minutes_right = totalMinutes;
            minutes_left = 0;
        } else {
            minutes_left = (int)System.Math.Floor((double)totalMinutes / 10.0);
            minutes_right = totalMinutes - (minutes_left * 10);
        }
        if (totalSeconds < 10) {
            seconds_right = totalSeconds;
            seconds_left = 0;
        } else {
            seconds_left = (int)System.Math.Floor((double)totalSeconds / 10.0);
            seconds_right = totalSeconds - (seconds_left * 10);
        }
    }

    public void DisplayTime() {
        SetTime();
        SetDigits();

        secL.sprite = AnswerManager.answers[seconds_left];
        secR.sprite = AnswerManager.answers[seconds_right];

        minL.sprite = AnswerManager.answers[minutes_left];
        minR.sprite = AnswerManager.answers[minutes_right];

        hrL.sprite = AnswerManager.answers[hours_left];
        hrR.sprite = AnswerManager.answers[hours_right];

        clL.sprite = ColSprite;
        clR.sprite = ColSprite;

    }

    public void Hide() {
        secL.sprite = null;
        secR.sprite = null;

        minL.sprite = null;
        minR.sprite = null;

        hrL.sprite = null;
        hrR.sprite = null;

        clL.sprite = null;
        clR.sprite = null;
    }

	

	// Update is called once per frame
	void Update () {
		
	}
}
