using combat;
using PlayerSpace;
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

        private void OnEnable()
        {
            MeleeAttack.OnAttack += FindGuard;
        }

        private void OnDisable()
        {
            MeleeAttack.OnAttack -= FindGuard;
        }

        private void Update()
        {
            
            if (targetGuard != null && !reachedGuard)
            {
                MoveTowardsGuard();
            }
        }

        public void FindGuard()
        {
            // Reset reachedGuard if we're looking for a new guard
            reachedGuard = false;

            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, guardLayerMask);

            foreach (Collider collider in colliders)
            {
                Debug.Log("Guard Detected");
                targetGuard = collider.transform;
                anim.SetTrigger("Run");
                break;
            }
        }

        public void MoveTowardsGuard()
        {
            if (targetGuard == null)
            {
                //TODO:
                //Add a begging mechanic or running away;
                return;
            }


            float step = moveSpeed * Time.deltaTime;

            //We move the civilian towards the guard;
            transform.position = Vector3.MoveTowards(transform.position, targetGuard.position, step);

            //Smoothly rotate the NPC to face the guard;
            Vector3 directionToGuard = targetGuard.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToGuard);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);

            //Stop moving if the NPC is very close to the guard;
            if (Vector3.Distance(transform.position, targetGuard.position) <= 6f)
            {
                Debug.Log("Reached the guard!");
                reachedGuard = true;
                anim.SetTrigger("talking");
                anim.ResetTrigger("Run");
                targetGuard = null; //Stop moving by nullifying the target;

                //TODO:
                //Alert Guard Script;
            }
        }
    }
}
