using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCspace
{
    public abstract class Base_Civilain : MonoBehaviour
    {
        [Header("NPC Characteristics")]
        public string npcName;
        public float moveSpeed = 3f;

        [SerializeField] private Animator anim;

        [Header("Detection Guard")]
        [SerializeField] private float detectionRadius;
        public LayerMask guardLayerMask;
        private Transform targetGuard;
        private bool reachedGuard;

        private void Update()
        {
            // Only search for a guard if the NPC hasn't already reached one
            if (!reachedGuard)
            {
                FindGuard();
            }
        }

        public void FindGuard()
        {
            Debug.Log("Hey There");
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, guardLayerMask);

            foreach (Collider collider in colliders)
            {
                Debug.Log("Guard Detected");
                targetGuard = collider.transform;
                anim.SetTrigger("Run"); // Trigger the run animation
                break; // Exit the loop after finding the first guard
            }

            // If a guard is detected, move towards it
            if (targetGuard != null)
            {
                MoveTowardsGuard();
            }
        }

        public void MoveTowardsGuard()
        {
            // Safety check to ensure targetGuard is not null
            if (targetGuard == null) return;

            float step = moveSpeed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, targetGuard.position, step);
            transform.LookAt(targetGuard);

            //Stop moving if the NPC is very close to the guard;
            if (Vector3.Distance(transform.position, targetGuard.position) < 5f)
            {
                Debug.Log("Reached the guard!");
                reachedGuard = true;
                targetGuard = null;
                anim.SetTrigger("talking");
                anim.ResetTrigger("Run");
            }
        }
    }
}