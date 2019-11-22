using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ButtonSwitch : MonoBehaviour {

    public int location;
    public GameObject cam;
    CameraScript camscript;
    public GameObject gamemanager;
    GameManager gm;

    public float volume = 1.0f;

    public AudioClip buttonPressSound;
    private AudioSource audioSource;

    public string condition;
    public static bool tutorial = false;

	// Use this for initialization
	void Start () {
        camscript = cam.GetComponent<CameraScript>();
        gm = gamemanager.GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown() {
        if (SoundButton.soundOn && (tutorial == false || condition == "TUTORIAL")) {
            audioSource.PlayOneShot(buttonPressSound, volume);
        }
        if (tutorial == false) {
            camscript.SwitchScreen(location);
        }
        if (condition == "RESTART" && tutorial == false) {
            gm.NewGame();
        }
        else if (condition == "TUTORIAL") {
            if (tutorial == true) {
                gm.attemptDisplay.HideTutorial();
            } else {
                gm.attemptDisplay.ShowTutorial();
            }
            tutorial = !tutorial;
        }
    }

}
