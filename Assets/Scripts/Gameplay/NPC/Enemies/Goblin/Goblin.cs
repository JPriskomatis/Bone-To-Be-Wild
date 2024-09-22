using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCspace
{
    public class Goblin : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;
        [SerializeField] private Transform head; // Assign BN_Head in the Inspector

        [SerializeField] private bool lookAtPlayer;

        private void Update()
        {
            if (lookAtPlayer)
            {
                LookAtPlayer();

            }
        }

        private void LookAtPlayer()
        {
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
    }
}
