using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Assets.Scripty {
    public class AudioManager {
        private AudioSource _audioSourceEfekt;
        private AudioSource _audioSourceHudba;
        private AudioSource _audioSourceZvuky;
        private AudioClip _defaultEfekt;

        private List<AudioClip> _hudba = new List<AudioClip>();

        public AudioManager(AudioSource[] audioSource) {
            _audioSourceEfekt = audioSource[0];
            _audioSourceHudba = audioSource[1];
            _audioSourceZvuky = audioSource[2];
            
            //public List<AudioClip> Hudba = new List<AudioClip>();

            string[] soundtrack = new[] {
                "Audio/Music/uvodni2",
                "Audio/Music/lala",
                "Audio/Music/uvodni1",
                "Audio/Music/Hudbik"
            };

            _hudba = soundtrack.Select(Resources.Load<AudioClip>).ToList();
            _defaultEfekt = _audioSourceEfekt.clip;
            _audioSourceHudba.clip = _hudba[0];
        }

        public void ZmenEfektNaDefault() {
            _audioSourceEfekt.clip = _defaultEfekt;
        }

        public void ZmenEfekt(AudioClip clip)
        {
            _audioSourceEfekt.clip = clip;
        }


        public void ZahrajZvuk(AudioClip clip) {
            _audioSourceZvuky.PlayOneShot(clip);
        }

        public void PustHudbu() {
            _audioSourceHudba.Play();
        }

        public void PustEfekt()
        {
            _audioSourceEfekt.Play();
        }

        public void ZmenHudbu() {
            var delka = _hudba.Count;
            _audioSourceHudba.clip = _hudba[UnityEngine.Random.Range(1, delka - 1)];
        }

        public void ZastavHudbu() {
            _audioSourceHudba.Stop();
        }

        public void ZastavEfekt()
        {
            _audioSourceEfekt.Stop();
        }

        public void ZtlumVse(float okolik) {
            _audioSourceEfekt.volume = okolik;
            _audioSourceHudba.volume = okolik;
        }
    }
}