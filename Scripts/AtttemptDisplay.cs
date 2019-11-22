using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class AtttemptDisplay : MonoBehaviour {

    public bool display;

    public GameObject left;
    public GameObject right;
    public GameObject banner;
    public GameObject clk;
    public GameObject one;
    public GameObject two;
    public GameObject avg_one;
    public GameObject avg_two;
    public GameObject change;
    public GameObject Logo;
    public GameObject Tutorial;

    SpriteRenderer lft;
    SpriteRenderer rght;
    SpriteRenderer ban;
    SpriteRenderer ChangeIndicator;
    SpriteRenderer TutorialR;
    ClockManager clock;

    SpriteRenderer Points_one;
    SpriteRenderer Points_two;
    SpriteRenderer Average_Points_one;
    SpriteRenderer Average_Points_two;
    SpriteRenderer LogoR;

    int digit_one;
    int digit_two;
    int avgDigit_two;
    int avgDigit_one;
    int changeAVG;
    int average;

    public bool tutorialOpen = false;

    public Sprite bannerSprite;
    public Sprite LogoSprite;
    public Sprite ChangeNone;
    public Sprite ChangeUp;
    public Sprite ChangeDown;
    public Sprite TutorialSprite;

	// Use this for initialization
	void Start () {
        lft = left.GetComponent<SpriteRenderer>();
        rght = right.GetComponent<SpriteRenderer>();
        ban = banner.GetComponent<SpriteRenderer>();
        clock = clk.GetComponent<ClockManager>();
        Points_one = one.GetComponent<SpriteRenderer>();
        Points_two = two.GetComponent<SpriteRenderer>();
        Average_Points_one = avg_one.GetComponent<SpriteRenderer>();
        Average_Points_two = avg_two.GetComponent<SpriteRenderer>();
        ChangeIndicator = change.GetComponent<SpriteRenderer>();
        TutorialR = Tutorial.GetComponent<SpriteRenderer>();
        LogoR = Logo.GetComponent<SpriteRenderer>();
    }

    public void Display(int a, int b, int time, int points) {
        lft.sprite = AnswerManager.answers[a];
        rght.sprite = AnswerManager.answers[b];
        ban.sprite = bannerSprite;
        clock.timeInSeconds = time;
        setPoints(points);
        setAveragePoints(points);

        Points_one.sprite = AnswerManager.answers[digit_one];
        Points_two.sprite = AnswerManager.answers[digit_two];
        Average_Points_one.sprite = AnswerManager.answers[avgDigit_one];
        Average_Points_two.sprite = AnswerManager.answers[avgDigit_two];
        LogoR.sprite = null;
        HideTutorial();
        clock.DisplayTime();
    }
    public void ShowTutorial() {
        TutorialR.sprite = TutorialSprite;
        tutorialOpen = true;

    }
    public void HideTutorial() {
        TutorialR.sprite = null;
        tutorialOpen = false;
    }

    public void Hide() {
        clock.Hide();
        lft.sprite = null;
        rght.sprite = null;
        ban.sprite = null;
        Points_one.sprite = null;
        Points_two.sprite = null;
        Average_Points_one.sprite = null;
        Average_Points_two.sprite = null;
        ChangeIndicator.sprite = null;
        LogoR.sprite = LogoSprite;
        TutorialR.sprite = null;
    }

    void setPoints(int x) {
        string str = x.ToString();
        while (str.Length < 2) {
            str = "0" + str;
        }
        digit_one = System.Int32.Parse(str[0].ToString());
        digit_two = System.Int32.Parse(str[1].ToString());
    }

    void SaveGamesData(int totalPoints, int totalGames) {
        GamesData gd = new GamesData();
        gd.totalPoints = totalPoints;
        gd.gamesPlayed = totalGames;
        PlayerPrefs.SetString("GamesData", Serializer.Serialize(gd));
    }

    void setAveragePoints(int points) {
        GamesData gd;
        int gamesPlayed;
        int totalPoints;
        float oldAverage;
        float newAverage;
        if (PlayerPrefs.HasKey("GamesData")) {
            gd = Serializer.Deserialize<GamesData>(PlayerPrefs.GetString("GamesData"));
            gamesPlayed = gd.gamesPlayed;
            totalPoints = gd.totalPoints;
            oldAverage = gd.totalPoints / gd.gamesPlayed;
            newAverage = (gd.totalPoints + points) / (gd.gamesPlayed + 1);
            if (newAverage > oldAverage) {
                ChangeIndicator.sprite = ChangeUp;
            } else if (newAverage < oldAverage) {
                ChangeIndicator.sprite = ChangeDown;
                ;
            } else {
                ChangeIndicator.sprite = ChangeNone;
            }
        } else {
            newAverage = points;
            totalPoints = points;
            gamesPlayed = 1;
        }

        string str = newAverage.ToString();
        Debug.Log(str);
        while (str.Length < 2) {
            str = "0" + str;
        }
        avgDigit_one = System.Int32.Parse(str[0].ToString());
        avgDigit_two = System.Int32.Parse(str[1].ToString());

        SaveGamesData((totalPoints + points), (gamesPlayed + 1));
    }
	
	// Update is called once per frame
	void Update () {

	}
}

[Serializable]
public class GamesData {
    public int gamesPlayed;
    public int totalPoints;
}
