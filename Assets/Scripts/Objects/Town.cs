using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{

    public class Town : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.instance.PlayMusic("TownBackground", 0.3f);
        }
    }

}