﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

    public Letter NumStart, NumFinish;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MakeLevel(int level)
    {
        List<Card> cards = new List<Card>();
        for(int i = 1; i <= level-1; ++i)
        {
            GameObject card;
            card = Instantiate(Resources.Load("Prefabs/Card", typeof(GameObject)) as GameObject);
            card.GetComponent<Card>().value = i;
            card.GetComponent<SpriteRenderer>().sprite =Resources.Load("Sprites/Back", typeof(Sprite)) as Sprite;
            card.GetComponent<SpriteRenderer>().sortingLayerName = "Stationary";
            card.GetComponent<Card>().letter = NumStart;
            card.transform.position = NumStart.transform.position;
            cards.Add(card.GetComponent<Card>());
        }

        System.Random rng = new System.Random();
        int n = level - 1;
        while(n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card val = cards[k];
            cards[k] = cards[n];
            cards[n] = val;
        }

        GameObject highCard = Instantiate(Resources.Load("Prefabs/Card", typeof(GameObject)) as GameObject);
        highCard.GetComponent<Card>().value = level;
        highCard.GetComponent<SpriteRenderer>().sprite =Resources.Load("Sprites/Back", typeof(Sprite)) as Sprite;

        NumStart.AddCard(highCard.GetComponent<Card>());
        foreach(Card c in cards)
        {
            NumStart.AddCard(c);
        }
    }
}