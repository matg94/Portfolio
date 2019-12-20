using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitManager : MonoBehaviour {

    public List<int> currentNumber;
    public bool NumberSelected;
    AnswerManager am;
    public List<NumberScript> buttons;
    public bool won=false;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public GameObject button7;
    public GameObject button8;
    public GameObject button9;
    public GameObject button0;

    // Use this for initialization
    void Start () {
        currentNumber = new List<int>();
        NumberSelected = false;
        am = GetComponent<AnswerManager>();
        buttons = new List<NumberScript>();
        buttons.Add(button0.GetComponent<NumberScript>());
        buttons.Add(button1.GetComponent<NumberScript>());
        buttons.Add(button2.GetComponent<NumberScript>());
        buttons.Add(button3.GetComponent<NumberScript>());
        buttons.Add(button4.GetComponent<NumberScript>());
        buttons.Add(button5.GetComponent<NumberScript>());
        buttons.Add(button6.GetComponent<NumberScript>());
        buttons.Add(button7.GetComponent<NumberScript>());
        buttons.Add(button8.GetComponent<NumberScript>());
        buttons.Add(button9.GetComponent<NumberScript>());
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void clearNumbersPressed() {
        for (int i = 0; i < 10; i++) {
            buttons[i].sr.sprite = buttons[i].unpressedSprite;
            buttons[i].pressed = false;
        }
    }

    public void resetLastNumbers() {
        am.SetLastNumbers(-1, -1, -1, -1);
    }

    public void ReceiveNumber(int number) {
        if (currentNumber.Count >= 4 && number == -2) {
            NumberSelected = true;
            UpdateAnswersManager(true);
        }
        if (number != -1 && number != -2 && currentNumber.Count < 4) {
            currentNumber.Add(number);
            buttons[number].sr.sprite = buttons[number].pressedSprite;
            buttons[number].pressed = true;
            UpdateAnswersManager();
        } else if (currentNumber.Count > 0 && number == -1 && won == false) {
            buttons[currentNumber[currentNumber.Count - 1]].sr.sprite = buttons[currentNumber[currentNumber.Count - 1]].unpressedSprite;
            buttons[currentNumber[currentNumber.Count - 1]].pressed = false;
            currentNumber.RemoveAt(currentNumber.Count - 1);
            UpdateAnswersManager();
        } 
    }

    public void ResetNumbers() {
        am.SetNumbers(-1, -1, -1, -1);
    }

    public void UpdateAnswersManager(bool submit=false) {
        switch (currentNumber.Count) {
            case 0:
                am.SetNumbers(-1, -1, -1, -1);
                break;
            case 1:
                am.SetNumbers(currentNumber[0], -1, -1, -1);
                break;
            case 2:
                am.SetNumbers(currentNumber[0], currentNumber[1], -1, -1);
                break;
            case 3:
                am.SetNumbers(currentNumber[0], currentNumber[1], currentNumber[2], -1);
                break;
            case 4:
                if (submit) {
                    am.SetNumbers(-1, -1, -1, -1);
                    am.SetLastNumbers(currentNumber[0], currentNumber[1], currentNumber[2], currentNumber[3]);
                    clearNumbersPressed();
                    break;
                }
                am.SetNumbers(currentNumber[0], currentNumber[1], currentNumber[2], currentNumber[3]);
                break;
        }
    }


}
