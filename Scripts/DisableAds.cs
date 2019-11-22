using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAds : MonoBehaviour {

    public static bool advertise;

    public Sprite EnabledAdSprite;
    public Sprite DisabledAdSprite;

    SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        advertise = true;
    }

    void setSprite(bool enabled) {
        if (enabled) {
            sr.sprite = EnabledAdSprite;
        } else {
            sr.sprite = DisabledAdSprite;
        }
    }

    private void OnMouseDown() {
        advertise = !advertise;
        setSprite(advertise);
    }
}
