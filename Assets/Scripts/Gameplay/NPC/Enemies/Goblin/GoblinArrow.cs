using PlayerSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCspace.goblin
{
    public class GoblinArrow : MonoBehaviour
    {
        [SerializeField] float f_arrowSpeed;
        [SerializeField] int i_arrowDamage;
        public void ShootProjectile(GameObject enemy)
        {
            this.transform.LookAt(enemy.transform.position);
            this.GetComponent<Rigidbody>().AddForce(transform.forward * f_arrowSpeed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Hit!");
                other.GetComponentInParent<AbilityScores>().TakeDamage(i_arrowDamage);
            }
        }
    }

}