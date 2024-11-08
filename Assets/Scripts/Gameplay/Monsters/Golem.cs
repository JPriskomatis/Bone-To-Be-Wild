using combat;
using Damageables;
using System.Collections;
using UnityEngine;



namespace monster
{
    public class Golem : Base_Monster
    {

        protected override void ExitState(MonsterState newState)
        {
            base.ExitState(newState);

            if (currentState == MonsterState.Running && newState != MonsterState.Running)
            {
                StartCoroutine(SmoothlyTransitionLocomotionToZero(0.3f));
            }
        }
       

        protected override void HurtState()
        {
            base.HurtState();
            if (currentHealth > 1)
            {
                anim.SetTrigger("GotHit");

            }


        }
        protected override void DeathState()
        {
            base.DeathState();
            Debug.Log("Second death");
            anim.SetTrigger("Death2");
            anim.SetFloat("Locomotion", 0f);
            DisableAllComponents();
        }
        protected override void CombatState()
        {
            base.CombatState();
            anim.SetTrigger("Attack4");
            inAttack = true;
            SetAttackCollider();
            StartCoroutine(AttackCoolDown());
        }

        #region Helper Class

        private IEnumerator AttackCoolDown()
        {
            yield return new WaitForSeconds(3.0f);
            inAttack = false;
            if (attackCollider.enabled == true)
            {
                SetAttackCollider();
            }
        }
        private IEnumerator SmoothlyTransitionLocomotionToZero(float duration)
        {
            float elapsed = 0f;
            float initialLocomotion = anim.GetFloat("Locomotion");

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float newLocomotionValue = Mathf.Lerp(initialLocomotion, 0f, elapsed / duration);
                anim.SetFloat("Locomotion", newLocomotionValue);
                yield return null;
            }

            anim.SetFloat("Locomotion", 0f);
        }


        #endregion
    }

}