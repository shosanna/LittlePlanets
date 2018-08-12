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

    // Tutorial
    public bool RunTutorial = false;

    // Zvuky
    public bool BranaUspesneVytocena = false;

    private AudioSource _audioSource;
    private AudioSource _audioSource2;

    // Casovac
    private float _ubehlyCas = 0f;
    private float _delkaDne = 100f;
    private float _procentoDne = 0f;

    public static GameState Instance {
        get { return _instance; }
    }

    public Sprite DefaultniDrevoObrazek;
    public Sprite DefaultniBoruvkoObrazek;

    void Start() {
        _audioSource = GetComponents<AudioSource>()[0];
        _audioSource2 = GetComponents<AudioSource>()[1];

        Debug.Assert(_audioSource != null);
        Debug.Assert(_audioSource2 != null);

        Inventar = new Inventar();

        PustHudbu();
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
        PustHudbu();
    }

    public float ProcentoDne() {
        return _procentoDne;
    }

    public void PustHudbu() {
        _audioSource.Play();
        _audioSource2.Play();
    }

    public void ZastavHudbu() {
        _audioSource.Stop();
    }

    public void ZtlumHudbu(float okolik) {
        _audioSource.volume = okolik;
    }

    public void SpustTutorial() {
        Fungus.Flowchart.BroadcastFungusMessage("SpustIntroNapovedu");
    }

    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
        PlayerData data = new PlayerData();
        data.Planeta = SceneManager.GetActiveScene().name;
        data.RunTutorial = GameState.Instance.RunTutorial;
        data.UbehlyCas = GameState.Instance._ubehlyCas;

        Inventar inv = GameState.Instance.Inventar;
        data.PocetBoruvek = inv.ZiskejPocet(Materialy.Boruvka);
        data.PocetDreva = inv.ZiskejPocet(Materialy.Drevo);

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {
            Debug.Log("File existuje, loaduju");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData) bf.Deserialize(file);
            file.Close();

            GameState.Instance.RunTutorial = data.RunTutorial;
            GameState.Instance._ubehlyCas = data.UbehlyCas;

            Inventar inv = GameState.Instance.Inventar;
            inv.Vynuluj();
            inv.PridejDoVolnehoSlotu(Materialy.Boruvka, data.PocetBoruvek, DefaultniBoruvkoObrazek);
            inv.PridejDoVolnehoSlotu(Materialy.Drevo, data.PocetDreva, DefaultniDrevoObrazek);

            SceneManager.LoadScene(data.Planeta);
        }
    }

    void OnGUI() {
        if (SceneManager.GetActiveScene().name != "intro") {
            if (GUI.Button(new Rect(Screen.width - 130, Screen.height - 50, 100, 30), "Save")) {
                Save();
            }
        }
    }
}

[Serializable]
class PlayerData {
    public float UbehlyCas;
    public bool RunTutorial;
    public int PocetDreva;
    public int PocetBoruvek;
    public string Planeta;
}