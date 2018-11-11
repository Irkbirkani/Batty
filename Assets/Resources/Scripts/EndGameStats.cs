using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EndGameStats : MonoBehaviour {

    public Text Moves, Level, Time;

	// Use this for initialization
	void Start () {
        Moves.text = "Moves: " + Stats.Moves;
        Level.text = "Level: " + Stats.Level;
        Time.text  = Stats.FormatTime();
        Stats.Moves = 0;
        Stats.Time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
