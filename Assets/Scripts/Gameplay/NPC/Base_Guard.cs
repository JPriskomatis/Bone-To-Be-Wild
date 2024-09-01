using PlayerSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCspace
{
    public class Base_Guard : MonoBehaviour
    {
        

        //[SerializeField] private Animator anim;

        [Header("References")]
        private Transform player; // Reference to the player's transform
        public LayerMask playerLayerMask; // Layer mask to detect the player

        [Header("Guard Settings")]
        public string npcName;
        public float moveSpeed = 3f;

        public float detectionRadius = 10f; // Radius to detect the player
        public float chaseSpeed = 5f; // Speed at which the guard chases the player
        public float stopChaseDistance = 2f; // Distance at which the guard stops chasing
        public float checkInterval = 0.5f; // How often to check for the player

        private bool isChasing = false;

        private Coroutine chaseCoroutine;


        private void Start()
        {

            player = FindObjectOfType<PlayerMovement>().transform;
            
        }




        public void StartChasing()
        {
            if (chaseCoroutine == null)
            {
                chaseCoroutine = StartCoroutine(ChasePlayerCoroutine());
            }
        }

        private IEnumerator ChasePlayerCoroutine()
        {
            while (true)
            {
                if (player == null) yield break;

                // Calculate the step for movement
                float step = chaseSpeed * Time.deltaTime;

                // Direction towards the player
                Vector3 directionToPlayer = player.position - transform.position;

                // Smoothly rotate the guard to face the player
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);

                // Check the distance to the player
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                // Move the guard towards the player
                if (distanceToPlayer > stopChaseDistance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.position, step);
                }
                else
                {
                    // Stop chasing if within the stopChaseDistance
                    Debug.Log("Guard has stopped chasing the player.");
                    isChasing = false;
                    chaseCoroutine = null; // Reset the coroutine reference
                    yield break; // Exit the coroutine
                }

                // Wait until the next frame before continuing the loop
                yield return null;
            }
        }

        public void StopChasing()
        {
            if (chaseCoroutine != null)
            {
                StopCoroutine(chaseCoroutine);
                chaseCoroutine = null;
            }
        }




    }
}

