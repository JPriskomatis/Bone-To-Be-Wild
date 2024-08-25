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
        float fadeDuration = 1f;

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

        public void PlayMusic(string name, float volume = 1f)
        {
            Sound s = Array.Find(musicSounds, x => x.name == name);

            if (s == null)
            {
                Debug.Log("Sound not found");
                return;
            }

            if (musicSource.isPlaying)
            {
                StartCoroutine(FadeOutAndIn(musicSource, s.clip, volume, fadeDuration));
            }
            else
            {
                StartCoroutine(FadeIn(musicSource, s.clip, volume, fadeDuration));
            }
        }

        private IEnumerator FadeOutAndIn(AudioSource audioSource, AudioClip newClip, float targetVolume, float fadeDuration)
        {
            // Fade out the current song
            float startVolume = audioSource.volume;
            while (audioSource.volume > 0f)
            {
                audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
                yield return null;
            }

            // Stop the current song completely
            audioSource.Stop();

            // Switch to the new song
            audioSource.clip = newClip;
            audioSource.volume = 0f;
            audioSource.Play();

            // Fade in the new song
            while (audioSource.volume < targetVolume)
            {
                audioSource.volume += targetVolume * Time.deltaTime / fadeDuration;
                yield return null;
            }

            audioSource.volume = targetVolume; // Ensure it's set to the target volume
        }

        private IEnumerator FadeIn(AudioSource audioSource, AudioClip newClip, float targetVolume, float fadeDuration)
        {
            audioSource.clip = newClip;
            audioSource.volume = 0f;
            audioSource.Play();

            // Fade in the new song
            while (audioSource.volume < targetVolume)
            {
                audioSource.volume += targetVolume * Time.deltaTime / fadeDuration;
                yield return null;
            }

            audioSource.volume = targetVolume; // Ensure it's set to the target volume
        }

        public void StopMusic(string name)
        {
            Sound s = Array.Find(musicSounds, x => x.name == name);
            musicSource.clip = s.clip;
            musicSource.Stop();

        }
        public void PlaySFX(string name, float volume = 1f)
        {
            Sound s = Array.Find(sfxSounds, x => x.name == name);
            if (s == null)
            {
                Debug.Log("Sound not found");
            }
            else
            {
                sfxSource.clip = s.clip;
                sfxSource.volume = volume;
                sfxSource.Play();
            }
        }
    }

}
