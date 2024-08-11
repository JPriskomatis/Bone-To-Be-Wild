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
        private void Update()
        {
            if (Time.time >= lastAttackTime + cooldownTime)
            {
                if (Input.GetMouseButtonDown(0)) 
                {
                    if (weapon_base != null)
                    {
                        anim.SetTrigger("attack");
                        weapon_base.TryDoAttack();
                        lastAttackTime = Time.time; // Update last attack time
                    }
                }
            }
        }
    }

}