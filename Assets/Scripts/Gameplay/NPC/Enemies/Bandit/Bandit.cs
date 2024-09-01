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


        //[SerializeField] private GameObject banditObject;
        private bool runningTowardsPlayer;

        [SerializeField]
        AnimationState startingState;

        [SerializeField] private GameObject banditGameobject;

        private void OnEnable()
        {
            Base_Enemy.OnDeath += DeathEvent;
        }
        private void OnDisable()
        {
            Base_Enemy.OnDeath -= DeathEvent;
        }

        private void DeathEvent()
        {
            //Stop movign the bandit;
            StopAllCoroutines();
            GetComponent<Bandit>().enabled = false;
        }
        public override void CloseToPlayer(GameObject player)
        {
            if (!runningTowardsPlayer){

                runningTowardsPlayer = true;

                Debug.Log("Player is here");
                //Start walking towards Player;
                //anim.SetTrigger("startWalking");
                CycleAnimation();

                StartCoroutine(LookTowardsPlayer(player, 10f));
                StartCoroutine(MoveTowardsPlayer(player));
            }
        }

        protected override void PlayAnimation(AnimationState state)
        {
            // Set the parameter for transitioning;
            anim.SetInteger("AnimationState", (int)state);
            currentState = state;
            
        }


        protected override void InitializeAnimator()
        {
            currentState = startingState;

            PlayAnimation(currentState);
        }
        

        IEnumerator LookTowardsPlayer(GameObject player, float rotationSpeed)
        {
            // Calculate the direction to the player
            Vector3 relativePos = player.transform.position - banditGameobject.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);

            // Smoothly rotate towards the target rotation
            while (true)
            {
                // Update the direction to the player
                relativePos = player.transform.position - banditGameobject.transform.position;
                targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);

                // Interpolate towards the target rotation
                banditGameobject.transform.rotation = Quaternion.Lerp(
                    banditGameobject.transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );

                // Wait until the next frame before continuing the loop
                yield return null;
            }
        }

        IEnumerator MoveTowardsPlayer(GameObject player)
        {
            while (Vector3.Distance(banditGameobject.transform.position, player.transform.position) > 4f)
            {
                Debug.Log(Vector3.Distance(banditGameobject.transform.position, player.transform.position));

                // Calculate the step to move towards the target
                float step = speed * Time.deltaTime;

                // Move the object towards the target
                banditGameobject.transform.position = Vector3.MoveTowards(banditGameobject.transform.position, player.transform.position, step);

                // Wait until the next frame before continuing the loop
                yield return null;
            }
            Debug.Log("Bandit reached the player.");

            //This transitions to Idle now;
            //Make this start Combat;
            CycleAnimation();
        }

    }

}