using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripty {
    public class Slot {
        private bool _jeObsazen;
        private Materialy _material;
        private int _pocet;
        private string _id;
        private Sprite _obrazek;

        public string Name {
            get { return _id; }
        }

        public Slot(string id) {
            _id = id;
        }

        public int ZiskejPocet()
        {
            return _pocet;
        }

        internal void Pridej(Materialy material, int kolik, Sprite obrazek) {
            _material = material;
            _pocet = kolik;
            _obrazek = obrazek;
            VykresliSe();
        }

        internal void PridejKExistujicimu(int kolik) {
            _pocet += kolik;
            VykresliSe();
        }

        internal bool JeVolno() {
            return !_jeObsazen;
        }

        internal Materialy Material() {
            return _material;
        }

        internal void Obsazen(string id) {
            _id = id;
            _jeObsazen = true;
        }

        public void VykresliSe() {
            GameObject gameObject = GameObject.Find(_id).gameObject;
            var renderer = gameObject.GetComponent<SpriteRenderer>();

            renderer.sprite = _obrazek;
            var text = gameObject.GetComponentInChildren<Text>();
            text.text = _pocet.ToString();
        }
    }
}