using Ink.Parsed;
using NPCspace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    public class DetectEnemies : MonoBehaviour
    {
        [Header("Enemy Radiuses")]
        [SerializeField] float goblinRadius;
        [SerializeField] float banditRadius;
        [SerializeField] LayerMask enemyLayer;
        [SerializeField] private GameObject player;

        private void Start()
        {
            StartCoroutine(PeriodicDetection());
        }

        IEnumerator PeriodicDetection()
        {
            while (true)
            {
                DetectNearbyEnemies();
                yield return new WaitForSeconds(0.5f);
            }

        }

        private void DetectNearbyEnemies()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, Mathf.Max(goblinRadius, banditRadius), enemyLayer);

            foreach (Collider hit in hitColliders)
            {
                string enemyTag = hit.tag;
                float distance = Vector3.Distance(hit.transform.position, transform.position);

                switch (enemyTag)
                {
                    case "Goblin":
                        if (distance <= goblinRadius)
                        {
                            Debug.Log("Goblin!");
                            Goblin goblin = hit.GetComponent<Goblin>();
                            goblin?.StartShooting();
                        }
                        break;

                    case "Bandit":
                        if (distance <= banditRadius)
                        {
                            Bandit bandit = hit.GetComponent<Bandit>();
                            bandit.GetComponentInChildren<Bandit>().CloseToPlayer(player.transform.gameObject);

                        }
                        break;

                    // Add more cases for other enemy types as needed
                    default:
                        break;
                }
            }
            
        }
    }

}
