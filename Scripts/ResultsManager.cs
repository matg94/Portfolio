using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsManager : MonoBehaviour {

    public GameObject SlotOne;
    public GameObject SlotTwo;

    SpriteRenderer slot_One;
    SpriteRenderer slot_Two;

    public Sprite spriteOut;

    public Sprite ballOne;
    public Sprite ballTwo;
    public Sprite ballThree;
    public Sprite ballFour;

    public Sprite strikeOne;
    public Sprite strikeTwo;
    public Sprite strikeThree;


    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool Display(int strikes, int balls) {
        slot_One = SlotOne.GetComponent<SpriteRenderer>();
        slot_Two = SlotTwo.GetComponent<SpriteRenderer>();
        switch (strikes) {
            case 0:
                slot_One.sprite = spriteOut;
                break;
            case 1:
                slot_One.sprite = strikeOne;
                break;
            case 2:
                slot_One.sprite = strikeTwo;
                break;
            case 3:
                slot_One.sprite = strikeThree;
                break;
            case 4:
                slot_One.sprite = strikeOne;
                slot_Two.sprite = strikeOne;
                return true;
        }

        switch (balls) {
            case 0:
                slot_Two.sprite = spriteOut;
                break;
            case 1:
                slot_Two.sprite = ballOne;
                break;
            case 2:
                slot_Two.sprite = ballTwo;
                break;
            case 3:
                slot_Two.sprite = ballThree;
                break;
            case 4:
                slot_Two.sprite = ballFour;
                break;
        }
        return false;
    }
}
