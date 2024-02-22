using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    float elapsedTime = 0.0f;
    bool RunBegan = false;
    int RemainingTargets;
    int TotalTargets;
    TimeSpan ts;
    [SerializeField] TextMeshProUGUI timer; 
    // Start is called before the first frame update
    void Start()
    {
        TotalTargets = GameObject.FindGameObjectsWithTag("Target").Length;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        ts = TimeSpan.FromSeconds(elapsedTime);
        timer.text = String.Format("{0}:{1}.{2}", ts.Minutes, ts.Seconds.ToString("00"), ts.Milliseconds.ToString("000"));
    }

    public void ChangeGameState(bool val, float elapsed)
    {
        RunBegan = val;
        // If the run ends (a false value),  we should see if this run is actually a leaderboard worthy run.
        if (RunBegan == false)
        {
            // Calculate penalties and such for every missed target here
            // We are assuming the leaderboard has been sorted already. And it should if I did my job right.
            // if (elapsed < save.times.Min) {Prompt player for name and populate the leaderboard with new score.}
        }
        // But if a run BEGINS, we need to enable our timer object and reset the value of it... Probably not in that order.
        else
        {
            RemainingTargets = TotalTargets;
            elapsedTime = 0.0f;
        }
        // In retrospect we should be setting it's active state 
        timer.gameObject.SetActive(val);
    }
}