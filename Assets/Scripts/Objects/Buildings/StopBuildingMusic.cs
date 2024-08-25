using Audio;
using System;
using UnityEngine;

namespace Buildings
{

    public class StopBuildingMusic : MonoBehaviour
    {
        public static event Action OnExit;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnExit?.Invoke();
                AudioManager.instance.PlayMusic("TownBackground", 0.3f);
                this.gameObject.SetActive(false);
            }
        }

    }

}