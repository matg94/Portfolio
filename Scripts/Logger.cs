using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour {

    public GameObject logEntry;

    static int count = 0;
    static int totalCount = 1;
    public List<LogEntry> logs;

	// Use this for initialization
	void Start () {
        logs = new List<LogEntry>();
    }
	
	public void AddResult(List<int> guess, int strikes, int balls) {
        if (count >= 11) {
            for (int i = 0; i < logs.Count; i++) {
                Transform t = logs[i].GetComponent<Transform>();
                t.position = new Vector2(t.position.x, (t.position.y + 1));
            }
            Destroy(logs[0].gameObject);
            logs.RemoveAt(0);
            count -= 1;
        }
        GameObject newEntry = Instantiate(logEntry, new Vector3(11.7f, ((-1.0f*count)+1)), Quaternion.identity);
        LogEntry entry = newEntry.GetComponent<LogEntry>();
        entry.set(totalCount, guess, strikes, balls);
        logs.Add(entry);
        count += 1;
        totalCount += 1;
    }

    public void ClearLogs() {
        count = 0;
        totalCount = 1;
        for (int i = 0; i < logs.Count; i++) {
            Destroy(logs[i].gameObject);
        }
        logs.Clear();
    }

}
