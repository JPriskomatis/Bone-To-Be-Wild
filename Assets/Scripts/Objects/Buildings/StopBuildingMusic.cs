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
                this.gameObject.SetActive(false);
            }
        }

    }

}