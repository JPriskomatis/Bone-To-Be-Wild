using combat;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using WeaponSpace;

namespace AbilitySpace
{
    public class FlamingStrike : Base_Ability, IABility
    {
        [Header("Settings")]
        [SerializeField] private float cooldown;
        private bool isAvailable = true;

        [Header("Linked Gameobjects")]
        [SerializeField] private GameObject flamingStrikeParticles;

        private MeleeAttack meleeAttack;

        public float Cooldown { get; private set; }
        [SerializeField] private GameObject cooldownTimer;

        private void Awake()
        {
            Cooldown = cooldown;
        }
        private void Start()
        {
            meleeAttack = FindObjectOfType<MeleeAttack>();
        }
        public void Activate()
        {
            if (isAvailable)
            {
                SetCurrentAbility();
                Debug.Log("Flame strike!");
                flamingStrikeParticles.SetActive(true);
                //Activate Flame VFX
                FindObjectOfType<Weapon_base>().AbilityAttack(5);

                StartCoroutine(StartCooldown());
            }
        }

        public void SetCurrentAbility()
        {
            meleeAttack.SetCurrentAbility(this);
        }
        public void Deactivate()
        {
            flamingStrikeParticles?.SetActive(false);
        }


        private IEnumerator StartCooldown()
        {
            isAvailable = false;
            float remainingCooldown = Cooldown;

            while (remainingCooldown > 0)
            {
                //Update the UI every frame from our abstract class method;
                UpdateAbilityUI(currentAbilityIcon, false, 0.05f, cooldownTimer, remainingCooldown);

                //Wait for the next frame
                yield return null;

                //Decrease the remaining cooldown
                remainingCooldown -= Time.deltaTime;
            }

            //Ensure cooldown is fully complete
            remainingCooldown = 0f;
            UpdateAbilityUI(currentAbilityIcon, true, 1f, cooldownTimer, remainingCooldown);
            cooldownTimer.SetActive(false);

            isAvailable = true; // Make the ability available again
        }

        public Sprite GetImage()
        {
            return abilityIcon;
        }

        public bool IsAvailable()
        {
            return isAvailable;
        }

        public void SetAbilityIcon(Image icon)
        {
            currentAbilityIcon = icon;
        }

        public void SetCooldownIcon(GameObject go_cooldown)
        {
            currentCooldownIcon = go_cooldown;
            cooldownTimer = go_cooldown;
        }
    }

}