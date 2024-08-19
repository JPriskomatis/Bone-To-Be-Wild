using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// We utilize the Singleton patter design for the AudioManager class;
    /// </summary>
    
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        public Sound[] musicSounds, sfxSounds;
        public AudioSource musicSource, sfxSource;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayMusic(string name)
        {
            Sound s = Array.Find(musicSounds, x=> x.name == name);

            if (s == null)
            {
                Debug.Log("Sound not found");
            }
            else
            {
                musicSource.clip = s.clip;
                musicSource.Play();
            }
        }

        public void StopMusic(string name)
        {
            Sound s = Array.Find(musicSounds, x => x.name == name);
            musicSource.clip = s.clip;
            musicSource.Stop();

        }
        public void PlaySFX(string name)
        {
            Sound s = Array.Find(sfxSounds, x => x.name == name);
            if (s == null)
            {
                Debug.Log("Sound not found");
            }
            else
            {
                sfxSource.clip = s.clip;
                sfxSource.Play();
            }
        }
    }

}
