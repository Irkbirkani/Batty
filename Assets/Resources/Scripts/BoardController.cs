using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
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
        MakeLevel(Stats.Level);
	}
	
	// Update is called once per frame
	void Update () {

        movesText.text = "Moves: " + Stats.Moves;
        if (started)
        {
            Stats.Time += Time.deltaTime;
            timeText.text = Stats.FormatTime();
        }

        if (NumFinish.Cards.Count == level)
        {
            var arr = NumFinish.Cards.ToArray();
            bool sorted = Sorted(arr);
            if (sorted)
            {
                started = false;
                SceneManager.LoadScene("EndScene");
                Debug.Log("YAY");
            }
        }
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
