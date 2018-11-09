﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour {

    public Stack<Card> Cards = new Stack<Card>();
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddCard(Card card)
    {
        Cards.Push(card);
        MoveCheck();
    }

    public void RemoveCard()
    {
        Cards.Pop();
        MoveCheck();
    }
    private void MoveCheck()
    {
        if(Cards.Count == 0)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
