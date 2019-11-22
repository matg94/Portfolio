using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour {

    public static bool soundOn = true;
    public Sprite soundOffSprite;
    public Sprite soundOnSprite;

    SpriteRenderer sr;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        loadSettings();
        setSprite();
    }

    void setSprite() {
        if (soundOn) {
            sr.sprite = soundOnSprite;
        } else {
            sr.sprite = soundOffSprite;
        }
    }

    private void OnMouseDown() {
        soundOn = !soundOn;
        setSprite();
        saveSoundSetting();
    }

    void saveSoundSetting() {
        PlayerPrefs.SetString("Sound", Serializer.Serialize(soundOn));
    }

    void loadSettings() {
        if (PlayerPrefs.HasKey("Sound")) {
            soundOn = Serializer.Deserialize<bool>(PlayerPrefs.GetString("Sound"));
        } else {
            saveSoundSetting();
        }
    }



}
