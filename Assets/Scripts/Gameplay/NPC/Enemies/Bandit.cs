using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace NPCspace
{
    /// <summary>
    /// All bandits should have an animation called "death" as we use this specific one from the Base_Enemy;
    /// </summary>
    /// 
    public class Bandit : Base_Enemy
    {
        [SerializeField] private GameObject banditObject;
        private bool runningTowardsPlayer;
        public override void CloseToPlayer(GameObject player)
        {
            if (!runningTowardsPlayer){
                runningTowardsPlayer = true;
                Debug.Log("Player is here");
                //Start walking towards Player;
                anim.SetTrigger("startWalking");

                StartCoroutine(LookTowardsPlayer(player, 5f));
                StartCoroutine(MoveTowardsPlayer(player));
            }

            


        }

        IEnumerator LookTowardsPlayer(GameObject player, float rotationSpeed)
        {
            // Calculate the direction to the player
            Vector3 relativePos = player.transform.position - banditObject.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);

            // Smoothly rotate towards the target rotation
            while (Quaternion.Angle(banditObject.transform.rotation, targetRotation) > 0.1f)
            {
                // Update the direction to the player
                relativePos = player.transform.position - banditObject.transform.position;
                targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);

                // Interpolate towards the target rotation
                banditObject.transform.rotation = Quaternion.Lerp(
                    banditObject.transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );

                // Wait until the next frame before continuing the loop
                yield return null;
            }

            // Ensure final rotation is set exactly to the target rotation
            banditObject.transform.rotation = targetRotation;
        }

        IEnumerator MoveTowardsPlayer(GameObject player)
        {

            while (Vector3.Distance(banditObject.transform.position, player.transform.position) > 0.1f)
            {
                // Calculate the step to move towards the target
                float step = speed * Time.deltaTime;

                // Move the object towards the target
                banditObject.transform.position = Vector3.MoveTowards(banditObject.transform.position, player.transform.position, step);

                // Wait until the next frame before continuing the loop
                yield return null;
            }
        }

    }

}