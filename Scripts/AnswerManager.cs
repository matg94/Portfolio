using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : MonoBehaviour {

    public GameObject ansOne;
    public GameObject ansTwo;
    public GameObject ansThree;
    public GameObject ansFour;

    SpriteRenderer ansOner;
    SpriteRenderer ansTwor;
    SpriteRenderer ansThreer;
    SpriteRenderer ansFourr;

    public GameObject lastAnsOne;
    public GameObject lastAnsTwo;
    public GameObject lastAnsThree;
    public GameObject lastAnsFour;

    SpriteRenderer lastAnsOneR;
    SpriteRenderer lastAnsTwoR;
    SpriteRenderer lastAnsThreeR;
    SpriteRenderer lastAnsFourR;

    public Sprite ans0;
    public Sprite ans1;
    public Sprite ans2;
    public Sprite ans3;
    public Sprite ans4;
    public Sprite ans5;
    public Sprite ans6;
    public Sprite ans7;
    public Sprite ans8;
    public Sprite ans9;

    public Sprite DefaultSprite;

    public static List<Sprite> answers;

    // Use this for initialization
    void Start () {
        ansOner = ansOne.GetComponent<SpriteRenderer>();
        ansTwor = ansTwo.GetComponent<SpriteRenderer>();
        ansThreer = ansThree.GetComponent<SpriteRenderer>();
        ansFourr = ansFour.GetComponent<SpriteRenderer>();

        lastAnsOneR = lastAnsOne.GetComponent<SpriteRenderer>();
        lastAnsTwoR = lastAnsTwo.GetComponent<SpriteRenderer>();
        lastAnsThreeR = lastAnsThree.GetComponent<SpriteRenderer>();
        lastAnsFourR = lastAnsFour.GetComponent<SpriteRenderer>();

        answers = new List<Sprite>();
        answers.Add(ans0);
        answers.Add(ans1);
        answers.Add(ans2);
        answers.Add(ans3);
        answers.Add(ans4);
        answers.Add(ans5);
        answers.Add(ans6);
        answers.Add(ans7);
        answers.Add(ans8);
        answers.Add(ans9);
    }
	
	public void SetNumbers(int a, int b, int c, int d) {
        if (a != -1) {
            ansOner.sprite = answers[a];
        } else {
            ansOner.sprite = DefaultSprite;
        }
        if (b != -1) {
            ansTwor.sprite = answers[b];
        } else {
            ansTwor.sprite = DefaultSprite;
        }
        if (c != -1) {
            ansThreer.sprite = answers[c];
        } else {
            ansThreer.sprite = DefaultSprite;
        }
        if (d != -1) {
            ansFourr.sprite = answers[d];
        } else {
            ansFourr.sprite = DefaultSprite;
        }
    }

    public void SetLastNumbers(int a, int b, int c, int d) {
        if (a != -1) {
            lastAnsOneR.sprite = answers[a];
        } else {
            lastAnsOneR.sprite = DefaultSprite;
        }
        if (b != -1) {
            lastAnsTwoR.sprite = answers[b];
        } else {
            lastAnsTwoR.sprite = DefaultSprite;
        }
        if (c != -1) {
            lastAnsThreeR.sprite = answers[c];
        } else {
            lastAnsThreeR.sprite = DefaultSprite;
        }
        if (d != -1) {
            lastAnsFourR.sprite = answers[d];
        } else {
            lastAnsFourR.sprite = DefaultSprite;
        }
    }
}
