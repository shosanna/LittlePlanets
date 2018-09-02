using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripty {
    class SaveManager {
        public static void Save() {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.OpenOrCreate);
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

        public static void SavePlanet(string name) {
            var mista = GameObject.FindGameObjectsWithTag("CropMisto");

            if (mista.Length > 0) {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + name + ".dat",
                    FileMode.OpenOrCreate);
                PlanetData data = new PlanetData();
                List<Misto> ulozenaMista = new List<Misto>();

                foreach (var misto in mista) {
                    var controller = misto.GetComponent<CropController>();
                    var m = new Misto
                    {
                        Index = controller.Index,
                        State = controller.State,
                        DenZasazeni = controller.DenZasazeni,
                        CasZasazeni = controller.CasZasazeni
                    };
                    ulozenaMista.Add(m);
                }

                data.Mista = ulozenaMista;
                Debug.Log("SAVE:");
                Debug.Log(data.ToString());
                bf.Serialize(file, data);
                file.Close();
            } else {
                Debug.Log("Zadna mista pro ulozeni");
            }
        }

        public static void LoadPlanet(string name) {
            if (File.Exists(Application.persistentDataPath + "/" + name + ".dat")) {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + name + ".dat", FileMode.Open);
                PlanetData data = (PlanetData) bf.Deserialize(file);
                file.Close();

                Debug.Log("LOAD:");
                Debug.Log(data.ToString());

                var mista = GameObject.FindGameObjectsWithTag("CropMisto");

                foreach (var misto in mista) {
                    var controller = misto.GetComponent<CropController>();

                    foreach (var ulozeneMisto in data.Mista) {
                        if (ulozeneMisto.Index == controller.Index) {
                            controller.State = ulozeneMisto.State;
                            controller.DenZasazeni = ulozeneMisto.DenZasazeni;
                            controller.CasZasazeni = ulozeneMisto.CasZasazeni;
                            break;
                        }
                    }
                }
            } else {
                Debug.Log("Soubor neexistuje: " + Application.persistentDataPath + "/" + name + ".dat");
            }
        }

        internal static void VycistiPlanety()
        {
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
                if (scene.enabled) {
                    string name = scene.path.Substring(scene.path.LastIndexOf('/') + 1);
                    name = name.Substring(0, name.Length - 6);

                    if (File.Exists(Application.persistentDataPath + "/" + name + ".dat")) {
                        File.Delete(Application.persistentDataPath + "/" + name + ".dat");
                        Debug.Log("Smazan soubor pro planetu: " + name);
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

        public override string ToString() {
            String result = "Mist celkem: " + Mista.Count + "\n";
            foreach (var misto in Mista) {
                result += string.Format("Misto {0}, state: {1}, den: {2}, cas: {3} \n", misto.Index, misto.State,
                    misto.DenZasazeni, misto.CasZasazeni);
            }
            return result;
        }
    }

    [Serializable]
    class Misto {
        public int Index;
        public int State;
        public int? DenZasazeni;
        public Den.Cas? CasZasazeni;
    }
}