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

        private void Start()
        {
            boxCollider = GetComponent<BoxCollider>();
            boxCollider.enabled = false;
        }
        private void Update()
        {
            if (Time.time >= lastAttackTime + cooldownTime)
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
            EnableWeaponCollider();
        }

        private void EnableWeaponCollider()
        {
            if (boxCollider != null)
            {
                boxCollider.enabled = true;
                Invoke("DisableWeaponCollider", 1f); // Disable collider after a short delay (adjust this timing based on your animation)
            }
        }

        private void DisableWeaponCollider()
        {
            boxCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag =="Enemy")
            {
                Debug.Log(other.transform.root.name);
            }
        }
    }

}