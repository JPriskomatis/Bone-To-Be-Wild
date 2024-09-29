using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCspace.goblin
{
    public abstract class Goblin : MonoBehaviour
    {
        [Header("Goblin Settings")]
        protected GameObject enemy;
        [SerializeField] private Transform head;
        public Animator anim;
        protected bool lookAtPlayer;

        [Header("Arrow Settings")]
        public GameObject p_arrow;
        public Transform bow;
        [SerializeField] private float maxDistance;

  

        private void Start()
        {
            //TODO:
            //Maybe try another way to get the player?;
            enemy = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (lookAtPlayer)
            {
                LookAtPlayer();

            }
        }

        private void LookAtPlayer()
        {
            lookAtPlayer = true;
            if (head != null && enemy != null)
            {
                // Calculate direction to the enemy but ignore the vertical difference (Y)
                Vector3 direction = new Vector3(enemy.transform.position.x - head.position.x, 0, enemy.transform.position.z - head.position.z);

                // Only rotate the head on the Y-axis
                if (direction.magnitude > 0) // Ensure the direction is valid
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    head.rotation = Quaternion.Euler(head.rotation.eulerAngles.x, targetRotation.eulerAngles.y, head.rotation.eulerAngles.z);
                }
            }
        }



        protected IEnumerator RotateGoblin()
        {
            yield return new WaitForSeconds(1.3f);
            float startRotation = transform.eulerAngles.y;
            float endZRot = startRotation + 90f;
            float duration = 1.7f;
            float t = 0;
            while (t < duration)
            {
                t += Time.deltaTime;
                float yRotation = Mathf.Lerp(startRotation, endZRot, t / duration);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
                yield return null;
            }
            // Ensure final rotation is exactly 90 degrees added
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, endZRot, transform.eulerAngles.z);
        }

    }
}
