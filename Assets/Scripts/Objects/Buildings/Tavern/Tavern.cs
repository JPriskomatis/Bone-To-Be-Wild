using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class Tavern : Base_Building
    {
        [SerializeField] private string entranceTheme;

        [SerializeField] private AudioClip peopleTalking;
        private void OnEnable()
        {
            Tavern_Door.OnEntrance += EntranceMusic;
            StopBuildingMusic.OnExit += ExitMusic;

        }
        private void OnDisable()
        {
            Tavern_Door.OnEntrance -= EntranceMusic;
            StopBuildingMusic.OnExit -= ExitMusic;

        }
        public void PeopleTalking()
        {
            BuildingAudio(peopleTalking);
        }
        public void EntranceMusic()
        {
            EntranceTheme(entranceTheme, 0.25f);
            PeopleTalking();

        }

        public void ExitMusic()
        {
            StopBuildingAudio();
        }
    }

}