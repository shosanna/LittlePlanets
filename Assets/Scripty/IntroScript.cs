using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI() {

        if (GUI.Button(new Rect(350, 560, 100, 30), "New Game")) {
            GameState.Instance.RunTutorial = true;
            SceneManager.LoadScene("planet1");
        }

        if (GUI.Button(new Rect(350, 600, 100, 30), "Load Game")) {
            GameState.Instance.Load();
        }
    }
}
