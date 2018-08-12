using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Assets.Scripty {
    public class AudioManager {
        private AudioSource _audioSource;
        private AudioSource _audioSource2;
        private List<AudioClip> _hudba = new List<AudioClip>();

        public AudioManager(AudioSource[] audioSource) {
            _audioSource = audioSource[0];
            _audioSource2 = audioSource[1];
            

            //public List<AudioClip> Hudba = new List<AudioClip>();

            string[] soundtrack = new[] {
                "Audio/Music/uvodni2",
                "Audio/Music/lala",
                "Audio/Music/uvodni1",
                "Audio/Music/Hudbik"
            };

            _hudba = soundtrack.Select(Resources.Load<AudioClip>).ToList();

            _audioSource2.clip = _hudba[0];
        }


        public void PustHudbu() {
            _audioSource.Play();
            _audioSource2.Play();
        }

        public void ZmenHudbu() {
            var delka = _hudba.Count;
            _audioSource2.clip = _hudba[UnityEngine.Random.Range(1, delka - 1)];
        }

        public void ZastavHudbu() {
            _audioSource.Stop();
        }

        public void ZtlumHudbu(float okolik) {
            _audioSource.volume = okolik;
        }
    }
}