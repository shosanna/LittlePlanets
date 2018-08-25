using Assets.Scripty;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameState : MonoBehaviour {
    // Core
    private static GameState _instance = null;

    public Inventar Inventar;
    public AudioManager AudioManager;

    // Tutorial
    public bool RunTutorial = false;

    // Zvuky
    public bool BranaUspesneVytocena = false;

    // Casovac
    public float _ubehlyCas = 0f;

    private int _den = 0;
    private float _delkaDne = 10f;
    private float _procentoDne = 0f;

    public static GameState Instance {
        get { return _instance; }
    }

    public Sprite DefaultniDrevoObrazek;
    public Sprite DefaultniBoruvkoObrazek;

    void Start() {
        Inventar = new Inventar();
        AudioManager = new AudioManager(GetComponents<AudioSource>());
        AudioManager.PustHudbu();
    }

    void Awake() {
        // Hned znic jine instance GameState - tento musi byt unikantni (Singleton)
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);

            // Znic take vsechny napovedy - maji byt jen jednou
            var napovedy = GameObject.FindGameObjectsWithTag("Napoveda");

            foreach (var napoveda in napovedy) {
                Destroy(napoveda);
            }
            return;
        } else {
            _instance = this;
        }

        // Tento GameState se nikdy neznici
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Update() {
        // Casovac
        _ubehlyCas += Time.deltaTime;
        _procentoDne = (float) Math.Round(_ubehlyCas / _delkaDne, 2);

        // Inventar
        Inventar.MujUpdate();
    }

    public void NastavKonecDne() {
        _ubehlyCas = 0;
        _den++;
    }

    public float ProcentoDne() {
        return _procentoDne;
    }

    public int Den() {
        return _den;
    }

    public void SpustTutorial() {
        Fungus.Flowchart.BroadcastFungusMessage("SpustIntroNapovedu");
    }

    void OnGUI() {
        if (SceneManager.GetActiveScene().name != "intro") {
            if (GUI.Button(new Rect(Screen.width - 130, Screen.height - 50, 100, 30), "Save")) {
                SaveManager.Save();
            }
        }
    }

    public void NastavDen(int den) {
        _den = den;
    }
}