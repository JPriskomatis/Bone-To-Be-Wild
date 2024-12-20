using Audio;
using Damageables;
using Dialoguespace;
using gameStateSpace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSpace;

namespace combat
{
    public class MeleeAttack : MonoBehaviour
    {
        public static event Action OnAttack;
        [SerializeField] private Weapon_base weapon_base;
        [SerializeField] private Animator anim;
        [SerializeField] private float cooldownTime = 2f; // Cooldown time in seconds

        private float lastAttackTime = 0f;

        private BoxCollider boxCollider;
        private MeleeWeaponTrail meleeWeaponTrail;

        [SerializeField] private GameObject sheathedPosition; // Correct spelling: sheathed
        private bool sheathedSword;

        // Variables to store the original position, rotation, and parent of the sword
        private Vector3 originalPosition;
        private Quaternion originalRotation;
        public Transform originalParent;

        public Transform swordTransform;
        private Vector3 initialSwordPosition;
        private Quaternion initialSwordRotation;

        private bool paused;


        [SerializeField] private float detectionRadius;
        [SerializeField] LayerMask civilianLayerMask;

        int currentDamage;
        private void Start()
        {
            boxCollider = GetComponent<BoxCollider>();
            meleeWeaponTrail = GetComponentInChildren<MeleeWeaponTrail>();
            boxCollider.enabled = false;

            initialSwordPosition = swordTransform.localPosition;
            initialSwordRotation = swordTransform.localRotation;
        }
        private void Update()
        {
            if(!(GameStatController.Instance.currentState == GameStatController.CurrentGameState.Paused))
            {
                if (Time.time >= lastAttackTime + cooldownTime && !DialogueManager.GetInstance().dialogueIsPlaying)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (weapon_base != null && !sheathedSword)
                        {
                            Attack();
                        }
                        else
                        {
                            StartCoroutine(UnSheathSword());
                        }
                    }

                    if (Input.GetMouseButtonDown(1))
                    {
                        StartCoroutine(SheathSword());
                    }
                }
            }

            
            
        }

        private IEnumerator UnSheathSword()
        {
            anim.SetTrigger("unSheath");

            yield return new WaitForSeconds(0.45f);
            AudioManager.instance.PlaySFX("UnsheathSword", 0.15f);
            yield return new WaitForSeconds(0.51f);

            if (swordTransform != null)
            {
                swordTransform.SetParent(originalParent);
                swordTransform.localPosition = initialSwordPosition;
                swordTransform.localRotation = initialSwordRotation;

                
            }

            sheathedSword = false;
        }

        private IEnumerator SheathSword()
        {
            anim.SetTrigger("sheath");

            yield return new WaitForSeconds(1f);

            // Set position and rotation based on sheathedPosition
            this.transform.position = sheathedPosition.transform.position;
            this.transform.rotation = sheathedPosition.transform.rotation;

            // Make the sword a child of the sheathed position
            this.transform.SetParent(sheathedPosition.transform);

            sheathedSword = true;
        }

        private void Attack()
        {
            //Check if peasants are nearby
            CheckForNearbyPeasants();
            

            anim.SetTrigger("attack");
            currentDamage = weapon_base.TryDoAttack();

            AudioManager.instance.PlaySFX("SwordSwing", 0.3f);
            lastAttackTime = Time.time; // Update last attack time
            StartCoroutine(EnableWeaponAttack());
        }

        private void CheckForNearbyPeasants()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, civilianLayerMask);

            foreach (Collider collider in colliders)
            {
                Debug.Log("Civilian Detected");
                OnAttack?.Invoke();
                break;
            }

        }

        private IEnumerator EnableWeaponAttack()
        {
            yield return new WaitForSeconds(0.2f);
            if (boxCollider != null)
            {
                boxCollider.enabled = true;
                meleeWeaponTrail.Emit = true; // Fixed from `enabled` to `true`
                Invoke("DisableWeaponAttack", 1f); // Disable collider after a short delay (adjust this timing based on your animation)
            }
        }

        private void DisableWeaponAttack()
        {
            boxCollider.enabled = false;
            meleeWeaponTrail.Emit = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy")) // Use CompareTag for better performance
            {
                Debug.Log(other.transform.root.name);

                ISwordDamageable swordDamageable = other.GetComponentInParent<ISwordDamageable>();
                if (swordDamageable != null)
                {
                    Debug.Log(currentDamage);
                    swordDamageable.SwordDamageable(currentDamage);
                }
                DisableWeaponAttack();
            }
        }
    }
}
