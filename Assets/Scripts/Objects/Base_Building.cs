using Audio;
using Interaction;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

/// <summary>
/// What we want a building to have:
/// 1) Music On Entrance and On Exit;
/// 2) Basic Information about the building;
/// </summary>
/// 
public abstract class Base_Building : MonoBehaviour
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }

    public AudioSource audioSource;

    public void EntranceTheme(string name, float volume=1f)
    {
        AudioManager.instance.PlayMusic(name, volume);
    }

    public void ExitTheme(string name)
    {
        AudioManager.instance.StopMusic(name);
    }

    public void BuildingAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void StopBuildingAudio()
    {
        audioSource.Stop();
    }


}
