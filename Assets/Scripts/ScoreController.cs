using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreController : MonoBehaviour
{
    public event Action<float> OnScoreUpdated;
    public static ScoreController Instance;

    private float score;
    public float Score {
        get
        {
            return score;
        }
        set
        {
            score = value;

            if (OnScoreUpdated != null) //Update UI when score change
                OnScoreUpdated(score);
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    public void Restart()
    {
        Score = 0;
    }


    public void AddReward(float value)
    {
        Score += value;
    }



}
