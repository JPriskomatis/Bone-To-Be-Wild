using combat;
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
    }

}