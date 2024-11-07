using Ink.Parsed;
using monster;
using NPCspace;
using NPCspace.goblin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using monster;

namespace PlayerSpace
{
    public class DetectEnemies : MonoBehaviour
    {
        [Header("Enemy Radiuses")]
        [SerializeField] float goblinRadius;
        [SerializeField] float banditRadius;
        [SerializeField] float golemRadius;
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
                            Goblin_Combat goblin = hit.GetComponent<Goblin_Combat>();
                            goblin?.DetectPlayer();
                        }
                        break;

                    case "Bandit":
                        if (distance <= banditRadius)
                        {
                            Bandit bandit = hit.GetComponentInParent<Bandit>();
                            bandit.GetComponent<Bandit>().CloseToPlayer(player.transform.gameObject);
                        }
                        break;
                    case "Golem":
                        if (distance <= golemRadius)
                        {
                            Golem golem = hit.GetComponentInParent<Golem>();
                            golem.GetComponent<Golem>().CloseToPlayer(player.transform.gameObject);
                        }
                        break;

                    default:
                        break;
                }
            }
            
        }
    }

}
