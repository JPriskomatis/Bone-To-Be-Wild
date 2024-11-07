using Damageables;
using System.Collections;
using UnityEngine;



namespace monster
{
    public class Golem : Base_Monster, ISwordDamageable, ISpellDamageable
    {

        protected override void ExitState(MonsterState newState)
        {
            base.ExitState(newState);

            if (currentState == MonsterState.Running && newState != MonsterState.Running)
            {
                StartCoroutine(SmoothlyTransitionLocomotionToZero(0.3f));
            }
        }
        public void SpellDamageable()
        {
            throw new System.NotImplementedException();
        }

        public void SwordDamageable(int damage)
        {
            Debug.Log("Got Hit");
            //Enter Hurt state;
            TransitionToState(MonsterState.Hurt);
        }

        protected override void HurtState()
        {
            base.HurtState();
            anim.SetTrigger("GotHit");
        }

        protected override void CombatState()
        {
            base.CombatState();
            anim.SetTrigger("Attack4");
            inAttack = true;
            StartCoroutine(AttackCoolDown());
        }

        #region Helper Class

        private IEnumerator AttackCoolDown()
        {
            yield return new WaitForSeconds(3.0f);
            inAttack = false;
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