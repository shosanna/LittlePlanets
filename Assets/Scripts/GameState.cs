using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    public bool RunTutorial = true;
    public bool BranaUspesneVytocena = false;

    private static GameState _instance = null;

    public static GameState Instance
    {
        get { return _instance; }
    }

    void Start () {
        // Run the tutorial only for the first time
		if (RunTutorial == true)
        {
            Fungus.Flowchart.BroadcastFungusMessage("SpustIntroNapovedu");
        }

        GetComponent<AudioSource>().Play();
	}

    void Awake()
    {
        // Immidiately destroy any other instance of game state - it must be only one (the first one)
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            _instance = this;
        }

        // Make the first game state live forever :)
        DontDestroyOnLoad(transform.gameObject);
    }
}
