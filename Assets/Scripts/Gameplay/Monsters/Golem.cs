using System.Collections;
using System.Collections.Generic;
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
    }

}