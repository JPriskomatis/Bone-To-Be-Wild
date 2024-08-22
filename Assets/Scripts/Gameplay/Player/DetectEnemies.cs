using NPCspace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    public class DetectEnemies : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bandit"))
            {
                other.GetComponentInParent<Bandit>().CloseToPlayer(transform.root.gameObject);
            }
        }
    }

}
