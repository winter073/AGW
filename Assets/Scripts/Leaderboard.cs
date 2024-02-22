using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI names, times;

    List<string> scores = new List<string>();
    List<string> playerNames = new List<string>();

    SaveData save;
    string savePath;

    // Start is called before the first frame update
    void Start()
    {
        // This needs to be right here or Unity will have a conniption.
        // It's actually because where the persistentDataPath is varies on every machine.
        savePath = Application.persistentDataPath + "/Save.json";
        save.names = new string[10] {"", "", "", "", "", "", "", "", "", ""};
        save.times = new string[10] { "", "", "", "", "", "", "", "", "", "" };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SaveMyGame(SaveData info)
    {
        // Convert our data into something usable by the JSON format.
        string tempData = JsonUtility.ToJson(info);
        File.WriteAllText(savePath, tempData);
    }

    void LoadMyGame()
    {
        // If a save file even exists, we should pull from it since that will have our scores in it. Hopefully. Soon. Working on that.
        if (File.Exists(savePath))
        {
            string tempData = File.ReadAllText(savePath);
            save = JsonUtility.FromJson<SaveData>(tempData);
        }
    }
}

public class SaveData
{
    public string[] names;
    public string[] times;
}
