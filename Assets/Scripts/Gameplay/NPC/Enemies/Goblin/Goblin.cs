using stateMachine;
using System.Collections;
using UI;
using UnityEngine;

namespace NPCspace.goblin
{
    public abstract class Goblin : Enemy
    {
        

        [Header("Goblin Settings")]
        public Sprite icon;
        public Enemy_Level.Level level = Enemy_Level.Level.Grunts;
        protected GameObject enemy;
        [SerializeField] private Transform head;
        public Animator anim;
        protected bool lookAtPlayer;


        [Header("Arrow Settings")]
        public GameObject p_arrow;
        public Transform bow;
        [SerializeField] private float maxDistance;

        private bool hasDetectedPlayer;

        public Enemy_UI enemy_ui;



  

        private void Update()
        {
            if (lookAtPlayer)
            {
                LookAtPlayer();

            }

        }

        public abstract void PerformAction();
        public void LookAtPlayer()
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


        public void DetectPlayer()
        {
            if (!hasDetectedPlayer)
            {
                hasDetectedPlayer = true;
                LookAtPlayer();
                
            }
            PerformAction();

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
