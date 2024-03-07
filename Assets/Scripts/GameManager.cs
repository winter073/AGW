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
    float AverageAcc; // Accounts for all shots fired
    int ShotsFired; // How many total shots
    int HitAcc; // How accurate the shots that hit were
    int ShotsHit; // How many shots actually hit the target
    public GameObject[] Targets;
    TimeSpan ts;
    [SerializeField] TextMeshProUGUI timer, targetCount;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Target");
        Targets = temp;
        ChangeGameState(true);
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
            foreach (GameObject target in Targets)
            {
                target.SetActive(true);
            }
            RemainingTargets = Targets.Length;
            targetCount.text = RemainingTargets + " / " + Targets.Length;
            elapsedTime = 0.0f;
        }
        // In retrospect we should be setting it's active state to always be the game state, lol
        timer.gameObject.SetActive(val);
    }
    // basically a failsafe if you're calling it to reset the game, and the obvious timer should be zero.
    public void ChangeGameState(bool val)
    {
        ChangeGameState(val, 0.0f);
    }

    public void TargetDie()
    {
        RemainingTargets -= 1;
        targetCount.text = RemainingTargets + " / " + Targets.Length;
    }
    public void TargetDie(float Acc)
    {

    }
}

