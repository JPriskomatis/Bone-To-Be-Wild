using monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace monster
{
    public class Spider : BaseMonster
    {
        protected override void CombatState()
        {
            base.CombatState();
            if (canAttack)
            {
                anim.SetTrigger("attack1");
                canAttack = false;
                anim.SetFloat("locomotion", 0f);
                StartCoroutine(StartAttackCooldown());
            }
        }
        protected override void ChaseState()
        {
            base.ChaseState();
            anim.SetFloat("locomotion", moveSpeed);
        }
        protected override void HurtState()
        {
            base.HurtState();
            anim.SetTrigger("gotHit");
        }

        protected override void DeathState()
        {
            base.DeathState();
            anim.SetTrigger("death1");
            Destroy(this);
        }
    }
}