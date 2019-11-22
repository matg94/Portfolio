using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEntry : MonoBehaviour {

    int number;
    int numL=0;
    int numR=0;
    List<int> digit;
    int Strikes;
    int Balls;

    public GameObject ans0;
    public GameObject ans1;
    public GameObject ans2;
    public GameObject ans3;

    SpriteRenderer ans0R;
    SpriteRenderer ans1R;
    SpriteRenderer ans2R;
    SpriteRenderer ans3R;

    public GameObject digitGOL;
    public GameObject digitGOR;
    Counter digitM_Left;
    Counter digitM_Right;

    public GameObject resultM;
    ResultsManager resultManager;

    public void set(int count, List<int> digits, int strikes, int balls) {
        Strikes = strikes;
        Balls = balls;
        number = count;
        digit = digits;

        digitM_Left = digitGOL.GetComponent<Counter>();
        digitM_Right = digitGOR.GetComponent<Counter>();

        ans0R = ans0.GetComponent<SpriteRenderer>();
        ans1R = ans1.GetComponent<SpriteRenderer>();
        ans2R = ans2.GetComponent<SpriteRenderer>();
        ans3R = ans3.GetComponent<SpriteRenderer>();

        resultManager = resultM.GetComponent<ResultsManager>();

        ans0R.sprite = AnswerManager.answers[digit[0]];
        ans1R.sprite = AnswerManager.answers[digit[1]];
        ans2R.sprite = AnswerManager.answers[digit[2]];
        ans3R.sprite = AnswerManager.answers[digit[3]];

        resultManager.Display(Strikes, Balls);

        fix_number();

        digitM_Left.number = numL;
        digitM_Right.number = numR;

        digitM_Right.SetSprite();
        digitM_Left.SetSprite();

    }

    public void fix_number() {
        if (number < 10) {
            numR = number;
            numL = 0;
        } else {
            numL = (int)System.Math.Floor((double)number / 10.0);
            numR = number - numL*10;
        }
    }

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
