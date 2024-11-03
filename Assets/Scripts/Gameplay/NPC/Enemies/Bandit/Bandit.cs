using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace NPCspace
{
    /// <summary>
    /// All bandits should have an animation called "death" as we use this specific one from the Base_Enemy;
    /// </summary>
    /// 
    public class Bandit : Base_Enemy
    {
        public enum BanditState  {Idle, Running, Combat, Hurt};

        public BanditState state;

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

        private void Update()
        {
            if(state == BanditState.Idle)
            {
                Debug.Log("Idle");
                PlayAnimation(AnimationState.Idle);
            }
            else if(state == BanditState.Running)
            {
                Debug.Log("Running");
                PlayAnimation(AnimationState.Running);
            }
            else if(state == BanditState.Hurt)
            {
                Debug.Log("Combat");
                PlayAnimation(AnimationState.Hurt);

            }
        }

        private void Start()
        {
            state = BanditState.Idle;
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

                //Start walking towards Player;

                StartCoroutine(LookTowardsPlayer(player, 10f));
                StartCoroutine(MoveTowardsPlayer(player));
            }
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
            state = BanditState.Running;
            while (Vector3.Distance(banditGameobject.transform.position, player.transform.position) > 4f)
            {
                // Calculate the step to move towards the target
                float step = speed * Time.deltaTime;

                // Move the object towards the target
                banditGameobject.transform.position = Vector3.MoveTowards(banditGameobject.transform.position, player.transform.position, step);

                // Wait until the next frame before continuing the loop
                yield return null;
            }
            Debug.Log("Bandit reached the player.");
            state = BanditState.Idle;
            //This transitions to Idle now;
            //Make this start Combat;
            //CycleAnimation();
        }

        public override async void SwordDamageable()
        {
            base.SwordDamageable();
            state = BanditState.Hurt;
            await DelayState();
        }

        private async UniTask DelayState()
        {
            await UniTask.Delay(58);
            state = BanditState.Idle;
        }
        protected override void InitializeAnimator()
        {
            Debug.Log("lol");
        }
    }

}