using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {

    public static int GetBalls(List<int> guess, List<int> answer) {
        int balls = 0;
        for (int i = 0; i < guess.Count; i++) {
            if (answer.IndexOf(guess[i]) != i && answer.IndexOf(guess[i]) != -1) {
                balls += 1;
            }
        }
        return balls;
    }

    public static int GetStrikes(List<int> guess, List<int> answer) {
        int strikes = 0;
        for (int i = 0; i < guess.Count; i++) {
            if (answer.IndexOf(guess[i]) == i) {
                strikes += 1;
            }
        }
        return strikes;
    }

    public static List<int> GetNumber() {
        List<int> number = new List<int>();
        while (number.Count < 4) {
            int num = (int)(Random.value * (9));
            if (number.IndexOf(num) == -1) {
                number.Add(num);
            }
        }
        Debug.Log("Number" + number);
        return number;
    }

}
