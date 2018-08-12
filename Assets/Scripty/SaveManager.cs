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

                Inventar inv = GameState.Instance.Inventar;
                inv.Vynuluj();
                inv.PridejDoVolnehoSlotu(Materialy.Boruvka, data.PocetBoruvek);
                inv.PridejDoVolnehoSlotu(Materialy.Drevo, data.PocetDreva);

                SceneManager.LoadScene(data.Planeta);
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
}