using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DropdownSelect(int level)
    {
        Stats.Level = level + 2;
        SceneManager.LoadScene("MainScene");
    }
}
