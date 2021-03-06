﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripty {
    public class Inventar {
        private Dictionary<Materialy, Sprite> _obrazky = new Dictionary<Materialy, Sprite>();

        private List<Slot> _sloty = new List<Slot>();

        public Inventar() {
            for (int i = 1; i <= 3; i++) {
                _sloty.Add(new Slot(string.Format("Slot{0}", i)));
            }
            _obrazky[Materialy.Boruvka] = Resources.Load<Sprite>("Sprity/boruvka");
            _obrazky[Materialy.Drevo] = Resources.Load<Sprite>("Sprity/drevo");
            _obrazky[Materialy.Slunecnice] = Resources.Load<Sprite>("Sprity/slunecnice_hlava");
        }

        public void MujUpdate() {
            _sloty.ForEach(x => x.VykresliSe());
        }

        public void PridejDoVolnehoSlotu(Materialy material, int kolik) {
            var slot = SlotProMaterial(material);

            if (slot != null) {
                slot.PridejKExistujicimu(kolik);
            } else {
                var volnySlot = _sloty.FirstOrDefault(x => x.JeVolno());

                if (volnySlot != null) {
                    volnySlot.Obsazen(volnySlot.Name);
                    volnySlot.Pridej(material, kolik, _obrazky[material]);
                } else {
                    Debug.Log("Plno");
                }
            }
        }

        public Slot SlotProMaterial(Materialy material) {
            return _sloty.FirstOrDefault(x => !x.JeVolno() && x.Material() == material);
        }

        public int ZiskejPocet(Materialy material) {
            var slot = SlotProMaterial(material);

            if (slot != null) {
                return slot.ZiskejPocet();
            }
            else {
                return 0;
            }
        }

        public void Vynuluj() {
            _sloty = new List<Slot>();
            for (int i = 1; i <= 3; i++)
            {
                _sloty.Add(new Slot(string.Format("Slot{0}", i)));
            }
        }
    }
}