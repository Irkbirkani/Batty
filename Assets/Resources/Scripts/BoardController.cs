using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour {

    public Letter NumStart, NumFinish;
    public Text movesText, timeText;
    public bool started = false;

    private float timeElapsed;
    private int level;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        movesText.text = "Moves: " + Stats.Moves;
        if (started)
        {
            timeElapsed += Time.deltaTime;
            timeText.text = FormatTime(timeElapsed);
        }

        if (NumFinish.Cards.Count == level)
        {
            var arr = NumFinish.Cards.ToArray();
            bool sorted = Sorted(arr);
            if (sorted)
            {
                started = false;
                //TODO Change to end scene
                Debug.Log("YAY");
            }
        }
	}

    string FormatTime(float value)
    {
        TimeSpan t = TimeSpan.FromSeconds(value);
        return string.Format("Time: {0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
    }

    public void MakeLevel(int lvl)
    {
        level = lvl;
        List<Card> cards = new List<Card>();
        for(int i = 1; i <= lvl-1; ++i)
        {
            GameObject card;
            card = Instantiate(Resources.Load("Prefabs/Card", typeof(GameObject)) as GameObject);
            card.GetComponent<Card>().MakeCard(i, NumStart);
            card.GetComponent<SpriteRenderer>().sortingOrder = lvl - i;
            cards.Add(card.GetComponent<Card>());
        }

        System.Random rng = new System.Random();
        int n = lvl - 1;
        while(n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card val = cards[k];
            cards[k] = cards[n];
            cards[n] = val;
        }

        GameObject highCard = Instantiate(Resources.Load("Prefabs/Card", typeof(GameObject)) as GameObject);
        highCard.GetComponent<Card>().MakeCard(lvl, NumStart);
        NumStart.AddCard(highCard.GetComponent<Card>());
        foreach(Card c in cards)
        {
            NumStart.AddCard(c);
        }
        NumStart.Cards.Peek().GetComponent<BoxCollider2D>().enabled = true;
    }

    public void StartTimer()
    {
        started = true;
        transform.Find("TimerStarter").gameObject.SetActive(false);
    }

    private bool Sorted(Card[] arr)
    {
        for(int i = 1; i < arr.Length; ++i)
        {
            if (arr[i - 1].value > arr[i].value)
            {
                return false;
            }
        }
        return true;
    }
}
