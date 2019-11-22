using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    Transform t;

	// Use this for initialization
	void Start () {
        t = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchScreen(int location) {
        switch (location) {
            case 0:
                t.position = new Vector3(0.0f, 0.0f, -10.0f);
                break;
            case 1:
                t.position = new Vector3(11.7f, 0.0f, -10.0f);
                break;
            case -1:
                t.position = new Vector3(-11.7f, 0.0f, -10.0f);
                break;
            case 2:
                t.position = new Vector3(23.4f, 0.0f, -10.0f);
                break;
        }

    }
}
