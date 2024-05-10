using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    float elapsedTime = 0.0f;
    bool RunState = false;
    int RemainingTargets;
    public GameObject[] Targets;
    TimeSpan ts;
    [SerializeField] TextMeshProUGUI timer, targetCount, calculationText;
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
        if (RunState)
        {
            elapsedTime += Time.deltaTime;
            ts = TimeSpan.FromSeconds(elapsedTime);
            timer.text = GetFormattedTime(ts);
        }
    }

    public void ChangeGameState(bool SentState, float elapsed)
    {
        CameraScript CS = GameObject.Find("Camera").GetComponent<CameraScript>();
        // If the run state is CHANGING to false, and wasn't already such, commence the run time calculation.
        if (SentState != RunState && SentState == false)
        {
            CS.SetAudio(false);
            CS.RunEnds();
            RunState = false;
            // runtime is our time without adjusting for the missed targets
            string runtime = GetFormattedTime(ts);
            // Score is our adjusted time (when it works)
            TimeSpan Score = TimeSpan.FromSeconds(elapsedTime);
            Score += TimeSpan.FromSeconds(0.5f * RemainingTargets);

            string runtimeAdjusted = GetFormattedTime(Score);
            timer.text = runtimeAdjusted;
            calculationText.text = String.Format("Run Time: {0} \n{1} targets missed (+{2} seconds) \nTotal Score: {3}", runtime, RemainingTargets, RemainingTargets * 0.5f, runtimeAdjusted);

        }
        // But if a run BEGINS, we need to enable our timer object and reset the value of it... Probably not in that order.
        else if (SentState != RunState && SentState == true)
        {
            CS.SetAudio(true);
            RunState = true;
            timer.gameObject.SetActive(true);
            foreach (GameObject target in Targets)
            {
                target.SetActive(true);
            }
            RemainingTargets = Targets.Length;
            targetCount.text = RemainingTargets + " / " + Targets.Length;
            elapsedTime = 0.0f;
            calculationText.text = String.Empty;
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

    public string GetFormattedTime(TimeSpan inputTime)
    {

        return String.Format("{0}:{1}.{2}", inputTime.Minutes, inputTime.Seconds.ToString("00"), inputTime.Milliseconds.ToString("000"));
    }
}

