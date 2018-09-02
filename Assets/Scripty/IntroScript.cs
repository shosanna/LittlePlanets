using System.Collections;
using System.Collections.Generic;
using Assets.Scripty;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    SaveManager.VycistiPlanety();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI() {

        if (GUI.Button(new Rect(350, 560, 100, 30), "New Game")) {
            GameState.Instance.AudioManager.ZmenHudbu();
            GameState.Instance.AudioManager.PustHudbu();
            GameState.Instance.RunTutorial = true;
            SceneManager.LoadScene("planet1");
        }

        if (GUI.Button(new Rect(350, 600, 100, 30), "Load Game")) {
            GameState.Instance.AudioManager.ZmenHudbu();
            GameState.Instance.AudioManager.PustHudbu();
            SaveManager.Load();
        }
    }
}
