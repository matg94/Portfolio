using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour {

    public int number;

    SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSprite() {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = AnswerManager.answers[number];
    }
}
