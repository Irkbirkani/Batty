using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        transform.parent.GetComponent<BoardController>().StartTimer();
    }
}
