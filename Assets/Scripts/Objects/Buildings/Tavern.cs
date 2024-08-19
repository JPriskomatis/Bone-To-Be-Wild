using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class Tavern : Base_Building
    {
        private void OnEnable()
        {
            Tavern_Door.OnEntrance += EntranceMusic;
            Tavern_Door.OnExit += ExitMusic;

        }
        private void OnDisable()
        {
            Tavern_Door.OnEntrance -= EntranceMusic;
            Tavern_Door.OnExit -= ExitMusic;

        }
        public void EntranceMusic()
        {
            EntranceTheme("Tavern Background");
        }

        public void ExitMusic()
        {
            ExitTheme("Tavern Background");
        }
    }

}