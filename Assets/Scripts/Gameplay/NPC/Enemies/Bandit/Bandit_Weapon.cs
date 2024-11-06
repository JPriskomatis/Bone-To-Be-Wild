using PlayerSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace combat
{
    public class Bandit_Weapon : MonoBehaviour
    {
        [SerializeField] private int weapon_damage;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<ICombat>().TakeDamage(weapon_damage);
            }
        }
    }

}