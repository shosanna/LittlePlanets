using System.Collections;
using System.Collections.Generic;
using Assets.Scripty;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.fontSize = 36;
        style.fontStyle = FontStyle.Bold;

        if (GUI.Button(new Rect(350, 400, 300, 90), "New Game", style)) {
            GameState.Instance.AudioManager.ZmenHudbu();
            GameState.Instance.AudioManager.PustHudbu();
            GameState.Instance.RunTutorial = true;
            SaveManager.VycistiPlanety();
            SceneManager.LoadScene("planet1");
        }

        if (GUI.Button(new Rect(690, 400, 300, 90), "Load Game", style)) {
            GameState.Instance.AudioManager.ZmenHudbu();
            GameState.Instance.AudioManager.PustHudbu();
            SaveManager.Load();
        }
    }
}
