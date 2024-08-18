using Damageables;
using Dialoguespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSpace;

namespace combat
{
    public class MeleeAttack : MonoBehaviour
    {
        [SerializeField] private Weapon_base weapon_base;
        [SerializeField] private Animator anim;

        [SerializeField] private float cooldownTime = 2f; // Cooldown time in seconds

        private float lastAttackTime = 0f;

        private BoxCollider boxCollider;
        private MeleeWeaponTrail meleeWeaponTrail;

        private void Start()
        {
            boxCollider = GetComponent<BoxCollider>();
            meleeWeaponTrail = GetComponentInChildren<MeleeWeaponTrail>();
            boxCollider.enabled = false;
        }
        private void Update()
        {
            if (Time.time >= lastAttackTime + cooldownTime && !DialogueManager.GetInstance().dialogueIsPlaying)
            {
                if (Input.GetMouseButtonDown(0)) 
                {
                    if (weapon_base != null)
                    {

                        Attack();
                    }
                }
            }
        }
        private void Attack()
        {
            anim.SetTrigger("attack");
            weapon_base.TryDoAttack();
            lastAttackTime = Time.time; // Update last attack time
            EnableWeaponAttack();
        }

        private void EnableWeaponAttack()
        {
            if (boxCollider != null)
            {
                boxCollider.enabled = true;
                meleeWeaponTrail.Emit = enabled;
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
            if (other.tag =="Enemy")
            {
                Debug.Log(other.transform.root.name);

                //When we attack someone we must check if they can be damaged by sword;
                //all these gameobjects implement the Interface IsSwordDamageable;
                //Therefore we check if our collider implements that interface, and if it does
                //we just call our SwordDamageable function;
                ISwordDamageable swordDamageable = other.GetComponent<ISwordDamageable>();
                if (swordDamageable != null)
                {
                    swordDamageable.SwordDamageable();
                }

            }
        }
    }

}