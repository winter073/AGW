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
        ChangeGameState(false);
        timer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (RunBegan)
        {
            elapsedTime += Time.deltaTime;
            ts = TimeSpan.FromSeconds(elapsedTime);
            timer.text = String.Format("{0}:{1}.{2}", ts.Minutes, ts.Seconds.ToString("00"), ts.Milliseconds.ToString("000"));
        }
    }

    public void ChangeGameState(bool val, float elapsed)
    {
        RunBegan = val;
        // If the run ends (a false value),  we should calculate a score
        if (RunBegan == false)
        {
            // runtime is our time without adjusting for the missed targets
            string runtime = String.Format("{0}:{1}.{2}", ts.Minutes, ts.Seconds.ToString("00"), ts.Milliseconds.ToString("000"));
            // Score is our adjusted time (when it works)
            TimeSpan Score = TimeSpan.FromSeconds(elapsedTime);
            Score += TimeSpan.FromSeconds(0.5f * RemainingTargets);

            string runtimeAdjusted = String.Format("{0}:{1}.{2}", Score.Minutes, Score.Seconds.ToString("00"), Score.Milliseconds.ToString("000"));
            timer.text = String.Format("Run Time: {0} \n{1} targets missed (+{2} seconds) \nTotal Score: {3}", runtime, RemainingTargets, RemainingTargets * 0.5f, runtimeAdjusted);

        }
        // But if a run BEGINS, we need to enable our timer object and reset the value of it... Probably not in that order.
        else
        {
            timer.gameObject.SetActive(true);
            foreach (GameObject target in Targets)
            {
                target.SetActive(true);
            }
            RemainingTargets = Targets.Length;
            targetCount.text = RemainingTargets + " / " + Targets.Length;
            elapsedTime = 0.0f;
        }
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
}

