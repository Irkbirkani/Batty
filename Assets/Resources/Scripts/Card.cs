using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    public int value;
    public bool top = false;
    public Letter letter;

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
        clicked = true;
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Moving";
    }

    private void OnMouseUp()
    {
        clicked = false;
        if((transform.position - letter.transform.position).magnitude >= 0.05f)
        {
            transform.position = letter.transform.position;
        }
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Stationary";
    }

    public void NonMove()
    {
        transform.position = letter.transform.position;
    }

    public void SetTransform(int numCards)
    {
        transform.position = new Vector2(letter.transform.position.x, letter.transform.position.y - numCards);
    }

    private void NotTop()
    {
        top = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var sort = collision.gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
        if (clicked)
        {
            switch (sort)
            {
                case "Letters":
                    Debug.Log("Letters");
                    letter.RemoveCard();
                    letter = collision.GetComponent<Letter>();
                    transform.position = letter.transform.position;
                    letter.AddCard(this);
                    top = true;
                    break;
                case "Stationary":
                    Debug.Log("Stationary");
                    if (collision.GetComponent<Card>().value < value)
                    {
                        Debug.Log("Value");
                        NonMove();
                        break;
                    }
                    else
                    {
                        letter.RemoveCard();
                        letter = collision.gameObject.GetComponent<Card>().letter;
                        letter.AddCard(this);
                        transform.position = letter.transform.position;
                        collision.GetComponent<Card>().top = false;
                        top = true;
                        break;
                    }
            }
        }
    }
}
