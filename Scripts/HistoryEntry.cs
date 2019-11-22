using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryEntry : MonoBehaviour {

    int attemptsTotal;
    int timeTakenSeconds;
    List<int> answer;

    public GameObject clock;
    ClockManager cm;

    public GameObject ans0;
    public GameObject ans1;
    public GameObject ans2;
    public GameObject ans3;

    public GameObject atmL;
    public GameObject atmR;

    public GameObject point_one;
    public GameObject point_two;
    public GameObject point_three;

    int a_left;
    int a_right;

    int digit_one;
    int digit_two;
    int digit_three;

    SpriteRenderer ans0R;
    SpriteRenderer ans1R;
    SpriteRenderer ans2R;
    SpriteRenderer ans3R;

    SpriteRenderer digit_oneR;
    SpriteRenderer digit_twoR;
    SpriteRenderer digit_threeR;

    Counter attleft;
    Counter attright;

    public void SetData(int attemptsLeft, int attemptsRight, int time, List<int> ans, int points) {
        a_left = attemptsLeft;
        a_right = attemptsRight;
        timeTakenSeconds = time;
        answer = ans;

        setPoints(points);

        cm = clock.GetComponent<ClockManager>();
        cm.timeInSeconds = time;

        ans0R = ans0.GetComponent<SpriteRenderer>();
        ans1R = ans1.GetComponent<SpriteRenderer>();
        ans2R = ans2.GetComponent<SpriteRenderer>();
        ans3R = ans3.GetComponent<SpriteRenderer>();

        digit_oneR = point_one.GetComponent<SpriteRenderer>();
        digit_twoR = point_two.GetComponent<SpriteRenderer>();
        digit_threeR = point_three.GetComponent<SpriteRenderer>();

        attleft = atmL.GetComponent<Counter>();
        attright = atmR.GetComponent<Counter>();


        ans0R.sprite = AnswerManager.answers[answer[0]];
        ans1R.sprite = AnswerManager.answers[answer[1]];
        ans2R.sprite = AnswerManager.answers[answer[2]];
        ans3R.sprite = AnswerManager.answers[answer[3]];

        attleft.number = a_left;
        attright.number = a_right;
        attleft.SetSprite();
        attright.SetSprite();

        digit_oneR.sprite = AnswerManager.answers[digit_one];
        digit_twoR.sprite = AnswerManager.answers[digit_two];
        digit_threeR.sprite = AnswerManager.answers[digit_three];

        cm.DisplayTime();
    }

    void setPoints(int x) {
        string str = x.ToString();
        while (str.Length < 3) {
            str = "0" + str;
        }
        digit_one = System.Int32.Parse(str[0].ToString());
        digit_two = System.Int32.Parse(str[1].ToString());
        digit_three = System.Int32.Parse(str[2].ToString());
    }


}

