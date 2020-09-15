using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("Sound Effects")]
        public AudioSource[] sfx;

        [Header("Background Music")]
        public AudioSource[] bgm;

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void PlaySFX(int soundToPlay)
        {
            if (soundToPlay < sfx.Length)
            {
                sfx[soundToPlay].Play();
            }
        }

        public void PlayBGM(int musicToPlay)
        {
            if (!bgm[musicToPlay].isPlaying)
            {
                StopMusic();

                if (musicToPlay < bgm.Length)
                {
                    bgm[musicToPlay].Play();
                }
            }
        }

        public void StopMusic()
        {
            for (int i = 0; i < bgm.Length; i++)
            {
                bgm[i].Stop();
            }
        }
    }
}