using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI names, times;

    SaveData save;
    string savePath;

    // Start is called before the first frame update
    void Start()
    {
        // This needs to be right here or Unity will have a conniption.
        // It's actually because where the persistentDataPath is varies on every machine.
        savePath = Application.persistentDataPath + "/Save.json";
        save.runs = new List<Run>();
        LoadMyGame();
        save.runs.Sort();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveMyGame(SaveData info)
    {
        // Convert our data into something usable by the JSON format.
        string tempData = JsonUtility.ToJson(info);
        File.WriteAllText(savePath, tempData);
    }

    public void LoadMyGame()
    {
        // If a save file even exists, we should pull from it since that will have our scores in it. Hopefully. Soon. Working on that.
        if (File.Exists(savePath))
        {
            string tempData = File.ReadAllText(savePath);
            save = JsonUtility.FromJson<SaveData>(tempData);
        }
    }
    public void UpdateLeaderBoard()
    {

    }
}

public class SaveData
{
    public List<Run> runs;
}
[System.Serializable]
public class Run : IComparable
{
    string Name;
    float time;
    
    public int CompareTo (object obj)
    {
        var a = this;
        var b = obj as Run;
        if (a.time < b.time)
        {
            return -1;
        }
        if (a.time > b.time)
        {
            return 1;
        }
        return 0;
    }

    public string getConvTime()
    {
        TimeSpan ts = TimeSpan.FromMilliseconds(time);
        return String.Format("{0}:{1}.{2}", ts.Minutes, ts.Seconds.ToString("00"), ts.Milliseconds.ToString("000"));
    }
}
