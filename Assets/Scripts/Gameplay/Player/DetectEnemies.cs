using NPCspace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    public class DetectEnemies : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bandit"))
            {
                other.GetComponentInChildren<Bandit>().CloseToPlayer(player.transform.gameObject);
                Debug.Log("dsf");
            }
        }
    }

}
