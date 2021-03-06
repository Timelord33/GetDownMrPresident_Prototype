﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScore : MonoBehaviour {

    public static GameScore main;

    public float defaultRoundTime = 10;
    float roundTime;
    public Text roundTimeText;

    public int firstPlayerScore = 0;
    public int secondPlayerScore = 0;
    public Text scoreOne;
    public Text scoreTwo;

    bool firstPlayerIsAssassin = true;

    RoundManager roundManager;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (main == null) { main = this; }
        else { Destroy(gameObject); }
    }

    // Use this for initialization
    void Start ()
    {
        roundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RoundManager>();

        scoreOne.text = firstPlayerScore.ToString();
        scoreTwo.text = secondPlayerScore.ToString();
        newRoundStarted();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (roundManager == null) {
            roundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RoundManager>();
        }

        RoundManager.RoundState roundState = roundManager.GetRoundState();

        if (roundState == RoundManager.RoundState.Playing)
        {
            roundTime -= Time.deltaTime;
            roundTimeText.text = "" + Mathf.Round(roundTime);
            if (roundTime < 0)
            {
                roundManager.OutOfTime();
            }            
        }
    }

    public void newRoundStarted()
    {
        roundTime = defaultRoundTime;
        roundTimeText.text = "" + Mathf.Round(roundTime);
    }
    
    public void bodyguardWins()
    {
        if (firstPlayerIsAssassin)
        {
            secondPlayerScore++;
        }
        else
        {
            firstPlayerScore++;
        }
        scoreOne.text = firstPlayerScore.ToString();
        scoreTwo.text = secondPlayerScore.ToString();
    }

    public void assassinWins()
    {
        if (firstPlayerIsAssassin)
        {
            firstPlayerScore++;
        }
        else
        {
            secondPlayerScore++;
        }
        scoreOne.text = firstPlayerScore.ToString();
        scoreTwo.text = secondPlayerScore.ToString();
    }
    

    public void newRoundStart()
    {
        firstPlayerIsAssassin = !firstPlayerIsAssassin;
    }
}
