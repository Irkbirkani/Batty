using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    public int value;
    public Letter letter;
    public bool down = true;

    private bool clicked = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (clicked)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                             Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                                             transform.position.z);
        }
	}


    private void OnMouseDown()
    {
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
        if((transform.position - new Vector3(letter.transform.position.x, letter.transform.position.y - (0.25f * (letter.Cards.Count-1)))).magnitude >= 0.05f)
        {
            transform.position = new Vector2(letter.transform.position.x, letter.transform.position.y - (0.25f * (letter.Cards.Count-1)));
        }
        Stats.Moves++;
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Stationary";
    }

    public void NonMove()
    {
        Stats.Moves--;
        transform.position = new Vector2(letter.transform.position.x, letter.transform.position.y - (0.25f * (letter.Cards.Count-1)));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var sort = collision.gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
        switch (sort)
        {
            case "Letters":
                letter.RemoveCard();
                letter = collision.GetComponent<Letter>();
                transform.position = letter.transform.position;
                letter.AddCard(this);
                break;
            case "Back":
                letter.RemoveCard();
                letter = collision.GetComponent<Card>().letter;
                transform.position = letter.transform.position;
                letter.AddCard(this);
                break;
            case "Stationary":
                if (collision.GetComponent<Card>().value < value)
                {
                    NonMove();
                    break;
                }
                else
                {
                    letter.RemoveCard();
                    letter = collision.gameObject.GetComponent<Card>().letter;
                    letter.AddCard(this);
                    transform.position = new Vector3(letter.transform.position.x, letter.transform.position.y - (0.25f * (letter.Cards.Count-1)));
                    break;
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
