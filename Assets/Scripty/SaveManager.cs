using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripty {
    class SaveManager {
        public static void Save() {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = new PlayerData();
            data.Planeta = SceneManager.GetActiveScene().name;
            data.RunTutorial = GameState.Instance.RunTutorial;
            data.UbehlyCas = GameState.Instance._ubehlyCas;
            data.Den = GameState.Instance.Den();
            Inventar inv = GameState.Instance.Inventar;
            data.PocetBoruvek = inv.ZiskejPocet(Materialy.Boruvka);
            data.PocetDreva = inv.ZiskejPocet(Materialy.Drevo);

            bf.Serialize(file, data);
            file.Close();
        }

        public static void Load() {
            if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
                PlayerData data = (PlayerData) bf.Deserialize(file);
                file.Close();

                GameState.Instance.RunTutorial = data.RunTutorial;
                GameState.Instance._ubehlyCas = data.UbehlyCas;
                GameState.Instance.NastavDen(data.Den);

                Inventar inv = GameState.Instance.Inventar;
                inv.Vynuluj();
                inv.PridejDoVolnehoSlotu(Materialy.Boruvka, data.PocetBoruvek);
                inv.PridejDoVolnehoSlotu(Materialy.Drevo, data.PocetDreva);

                SceneManager.LoadScene(data.Planeta);
            }
        }

        public static void SavePlanet() {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/planetInfo.dat", FileMode.Open);
            PlanetData data = new PlanetData();

            var mista = GameObject.FindGameObjectsWithTag("CropMisto");
            List<Misto> ulozenaMista = new List<Misto>();

            foreach (var misto in mista) {
                var m = new Misto {
                    index = misto.GetComponent<CropController>().index,
                    state = misto.GetComponent<CropController>().state
                };
                ulozenaMista.Add(m);
            }

            data.Mista = ulozenaMista;
            bf.Serialize(file, data);
            file.Close();
        }

        public static void LoadPlanet() {
            if (File.Exists(Application.persistentDataPath + "/planetInfo.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/planetInfo.dat", FileMode.Open);
                PlanetData data = (PlanetData)bf.Deserialize(file);
                file.Close();

                var mista = GameObject.FindGameObjectsWithTag("CropMisto");

                foreach (var misto in mista) {
                    foreach (var ulozeneMisto in data.Mista) {
                        if (ulozeneMisto.index == misto.GetComponent<CropController>().index) {
                            misto.GetComponent<CropController>().state = ulozeneMisto.state;
                        }
                    }
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
        public int Den;
    }

    [Serializable]
    class PlanetData {
        public List<Misto> Mista;
    }

    [Serializable]
    class Misto {
        public int index;
        public int state;
    }
}