﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public int value;
    public Letter letter;
    public bool down = true;
    public BoardController board;
    private bool triggered = false;
    private GameObject collisionTarget;


    private bool clicked = false;
    // Use this for initialization
    void Start()
    {
        board = GameObject.Find("Board Controller").GetComponent<BoardController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                             Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                                             transform.position.z);
        }
    }


    private void OnMouseDown()
    {
        if (!board.started)
            board.StartTimer();

        if (down)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/" + value, typeof(Sprite)) as Sprite;
            down = false;
        }
        clicked = true;
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Moving";
    }

    private void OnMouseUp()
    {
        clicked = false;
        if ((transform.position - new Vector3(letter.transform.position.x, letter.transform.position.y - (0.35f * letter.CardDistance()))).magnitude >= 0.05f)
        {
            transform.position = new Vector2(letter.transform.position.x, letter.transform.position.y - (0.35f * letter.CardDistance()));
        }
        if (triggered)
            MakeMove(collisionTarget);
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Stationary";
    }

    public void NonMove()
    {
        Stats.Moves--;
        transform.position = new Vector2(letter.transform.position.x, letter.transform.position.y - (0.35f * letter.CardDistance()));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggered = true;
        collisionTarget = collision.gameObject;
    }

    public void MakeMove(GameObject go)
    {
        triggered = false;
        var sort = go.gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
        Stats.Moves++;
        switch (sort)
        {
            case "Letters":
                letter.RemoveCard();
                letter = go.GetComponent<Letter>();
                transform.position = letter.transform.position;
                letter.AddCard(this);
                return;
            case "Back":
                letter.RemoveCard();
                letter = go.GetComponent<Card>().letter;
                transform.position = letter.transform.position;
                letter.AddCard(this);
                return;
            case "Stationary":
                if (go.GetComponent<Card>().value < value)
                {
                    NonMove();
                    return;
                }
                else
                {
                    letter.RemoveCard();
                    letter = go.gameObject.GetComponent<Card>().letter;
                    letter.AddCard(this);
                    transform.position = new Vector3(letter.transform.position.x, letter.transform.position.y - (0.25f * letter.CardDistance()));
                    return;
                }
        }
    }

    public void MakeCard(int val, Letter letr)
    {
        this.GetComponent<Card>().value = val;
        this.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Back", typeof(Sprite)) as Sprite;
        this.name = val.ToString();
        this.GetComponent<SpriteRenderer>().sortingLayerName = "Back";
        this.GetComponent<Card>().letter = letr;
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.transform.position = letter.transform.position;
    }
}
