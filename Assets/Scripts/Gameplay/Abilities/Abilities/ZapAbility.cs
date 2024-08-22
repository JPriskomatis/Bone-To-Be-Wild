using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Damageables;
using System;
using UnityEngine.UI;
using TMPro;
using Audio;

namespace AbilitySpace
{
    public class ZapAbility : Base_Ability, IABility
    {

        [SerializeField] private float cooldown;
        public float Cooldown { get; private set; }


        private bool isAvailable = true;

        [SerializeField] Animator anim;

        private Camera playerCamera;
        [SerializeField] private float maxDinstance = 100f;


        public GameObject vfxPrefab;
        public Vector3 offset = new Vector3(0, 0, 1);

        private GameObject enemyToTrack;

        //UI
        [SerializeField] private Image abilityIcon;
        [SerializeField] private GameObject cooldownTimer;


        private void Awake()
        {
            Cooldown = cooldown;
        }
        private void Start()
        {
            playerCamera = Camera.main;
            abilityIcon.gameObject.SetActive(true);
        }

        public void ActivateIcon()
        {
            abilityIcon.gameObject.SetActive(true);
        }

        public void Activate()
        {
            if (isAvailable)
            {
                // Find the closest enemy and check if it's damageable
                enemyToTrack = FindClosestTarget();

                if (enemyToTrack != null)
                {
                    // Check if the enemy has the ISpellDamageable interface
                    ISpellDamageable spellDamageable = enemyToTrack.GetComponent<ISpellDamageable>();
                    if (spellDamageable != null)
                    {
                        spellDamageable.SpellDamageable();
                    }
                }

                // Perform the animation
                anim.SetTrigger("spell");

                // Start cooldown process
                StartCoroutine(StartCooldown());
            }
        }

        public void Deactivate()
        {
            throw new System.NotImplementedException();
        }

        public bool IsAvailable()
        {
            return isAvailable;
        }

        private IEnumerator StartCooldown()
        {
            isAvailable = false;
            float remainingCooldown = Cooldown;

            while (remainingCooldown > 0)
            {
                //Update the UI every frame from our abstract class method;
                UpdateAbilityUI(abilityIcon, false, 0.05f, cooldownTimer, remainingCooldown);

                //Wait for the next frame
                yield return null;

                //Decrease the remaining cooldown
                remainingCooldown -= Time.deltaTime;
            }

            //Ensure cooldown is fully complete
            remainingCooldown = 0f;
            UpdateAbilityUI(abilityIcon, true, 1f, cooldownTimer, remainingCooldown);
            cooldownTimer.SetActive(false);

            isAvailable = true; // Make the ability available again
        }

        public void CastSpell()
        {
            // Instantiate the VFX prefab at the closest target's position if a target exists
            if (enemyToTrack != null)
            {
                Vector3 targetPosition = enemyToTrack.transform.position;
                GameObject fvxInstance = Instantiate(vfxPrefab, targetPosition, Quaternion.identity);

                AudioManager.instance.PlaySFX("Zap Ability", 0.5f);
                Destroy(fvxInstance, 1f);
            }
            else
            {
                // If no target is found, instantiate VFX at the player's position
                Vector3 spawnPosition = transform.position + transform.TransformDirection(offset);
                GameObject fvxInstance = Instantiate(vfxPrefab, spawnPosition, Quaternion.identity);

                //Audio
                AudioManager.instance.PlaySFX("Zap Ability", 0.5f);
                Destroy(fvxInstance, 1f);
            }
        }

        private GameObject FindClosestTarget()
        {
            RaycastHit hit;

            int enemyLayer = LayerMask.NameToLayer("Enemy");
            int layerMask = 1 << enemyLayer;

            // Perform the raycast
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, maxDinstance, layerMask))
            {
                ISpellDamageable spellDamageable = hit.collider.GetComponentInChildren<ISpellDamageable>();
                if (spellDamageable != null)
                {
                    return hit.collider.gameObject; // Return the GameObject that was hit
                }
            }

            return null;
        }
    }

}
