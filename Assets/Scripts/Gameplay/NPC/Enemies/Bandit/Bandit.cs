using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

namespace NPCspace
{
    /// <summary>
    /// All bandits should have an animation called "death" as we use this specific one from the Base_Enemy;
    /// </summary>
    /// 
    public class Bandit : Base_Enemy
    {
        public enum BanditState { Idle, Running, Combat, Hurt };

        public BanditState state;

        //[SerializeField] private GameObject banditObject;
        private bool runningTowardsPlayer;

        private GameObject playerToFollow;

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

        private async void Update()
        {
            if (state == BanditState.Idle)
            {
                IdleState();
            }
            else if (state == BanditState.Running)
            {

                RunningState();
            }
            else if (state == BanditState.Hurt)
            {
                HurtState();
            }
            else if (state == BanditState.Combat)
            {
                await CombatStateAsync();
            }
        }

        private void IdleState()
        {
            PlayAnimation(AnimationState.Idle);
        }

        private void RunningState()
        {
            PlayAnimation(AnimationState.Running);

        }
        private async Task CombatStateAsync()
        {
            if (canAttack)
            {
                canAttack = false;
                PlayAnimation(AnimationState.Combat);
                await DelayState(144);
                canAttack = true;
                if (Vector3.Distance(playerToFollow.transform.position, transform.position) > 5f)
                {
                    Debug.Log("Distance greater that 5");
                    StartCoroutine(MoveTowardsPlayer(playerToFollow));
                }
                else
                {
                    state = BanditState.Combat;
                }

            }
        }

        private void HurtState()
        {
            PlayAnimation(AnimationState.Hurt);
        }

        private void Start()
        {
            state = BanditState.Idle;
            canAttack = true;
        }
        private void DeathEvent()
        {
            //Stop movign the bandit;
            StopAllCoroutines();
            GetComponent<Bandit>().enabled = false;
        }
        public override void CloseToPlayer(GameObject player)
        {
            if (!runningTowardsPlayer)
            {

                runningTowardsPlayer = true;

                //Start walking towards Player;

                StartCoroutine(LookTowardsPlayer(player, 10f));
                StartCoroutine(MoveTowardsPlayer(player));
            }
        }


        IEnumerator LookTowardsPlayer(GameObject player, float rotationSpeed)
        {
            playerToFollow = player;
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
            state = BanditState.Combat;
            //This transitions to Idle now;
            //Make this start Combat;
            //CycleAnimation();
        }

        public override async void SwordDamageable()
        {
            base.SwordDamageable();
            state = BanditState.Hurt;
            await DelayState(58);
        }

        private async UniTask DelayState(int delay)
        {
            await UniTask.Delay(delay);
            state = BanditState.Idle;
        }
        protected override void InitializeAnimator()
        {
            Debug.Log("lol");
        }
    }

}