using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class HistoryManager : MonoBehaviour {

    List<EntryData> entries;
    List<HistoryEntry> existingEntries;
    public GameObject histEntry;

    void Start() {
        entries = new List<EntryData>();
        existingEntries = new List<HistoryEntry>();
        LoadEntryData();
        UpdateDisplay();
    }


    public void AddNewEntry(int attemptsLeft, int attemptsRight, int time, List<int> answer, int points) {
        EntryData tmp = new EntryData();
        tmp.attemptsLeft = attemptsLeft;
        tmp.attemptsRight = attemptsRight;
        tmp.TimeTakenSeconds = time;
        tmp.answer = answer;
        tmp.points = points;
        entries.Add(tmp);
        if (entries.Count > 13) {
            entries.RemoveAt(0);
        }
        UpdateDisplay();
        Save();
    }

    public void UpdateDisplay() {
        for (int i = 0; i < existingEntries.Count; i++) {
            Destroy(existingEntries[i].gameObject);
        }
        existingEntries.Clear();
        for (int i = 0; i < entries.Count; i++) {
            GameObject tmp = Instantiate(histEntry, new Vector3(23.4f, ((-0.8f * i) + 5 )), Quaternion.identity);
            HistoryEntry display = tmp.GetComponent<HistoryEntry>();
            display.SetData(entries[i].attemptsLeft, entries[i].attemptsRight, entries[i].TimeTakenSeconds, entries[i].answer, entries[i].points);
            existingEntries.Add(display);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Save() {
        PlayerPrefs.SetString("Entries", Serializer.Serialize(entries));
    }

    void LoadEntryData() {
        if (PlayerPrefs.HasKey("Entries")) {
            entries = Serializer.Deserialize<List<EntryData>>(PlayerPrefs.GetString("Entries"));
        } else {
            Save();
        }
    }
}

[Serializable]
public class EntryData {
    public int attemptsLeft;
    public int attemptsRight;
    public int TimeTakenSeconds;
    public int points;
    public List<int> answer;
}