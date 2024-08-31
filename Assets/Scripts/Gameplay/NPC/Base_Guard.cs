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


        private void Start()
        {

            player = FindObjectOfType<PlayerMovement>().transform;
            
        }



        private void ChasePlayer()
        {
            if (player == null) return;

            // Move towards the player
            float step = chaseSpeed * Time.deltaTime;
            Vector3 directionToPlayer = player.position - transform.position;

            // Move the guard towards the player
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);

            // Smoothly rotate the guard to face the player
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);

            // Stop chasing if within stopChaseDistance
            if (Vector3.Distance(transform.position, player.position) <= stopChaseDistance)
            {

                Debug.Log("Guard has stopped chasing the player.");
                isChasing = false;
            }
        }

        


    }
}

