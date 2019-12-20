using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberScript : MonoBehaviour {

    public int number;

    public GameObject go;
    DigitManager dm;

    AudioSource source;
    public AudioClip sound;

    public float volume = 1.0f;

    public bool pressed = false;

    public SpriteRenderer sr;
    public Sprite pressedSprite;
    public Sprite unpressedSprite;

	// Use this for initialization
	void Start () {
        dm = go.GetComponent<DigitManager>();
        sr = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void OnMouseDown() {
        if (pressed == false) {
            if (SoundButton.soundOn) {
                source.PlayOneShot(sound, volume);
            }
            dm.ReceiveNumber(number);
        }
    }
}
