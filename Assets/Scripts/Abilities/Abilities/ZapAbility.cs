using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySpace
{
    public class ZapAbility : MonoBehaviour, IABility
    {
        public float Cooldown { get; private set; } = 2f;
        private bool isAvailable = true;

        public void Activate()
        {
            if(isAvailable)
            {
                Debug.Log("Zap");

                //Perform the animation;
                Animator anim = GetComponentInChildren<Animator>();

                anim.SetTrigger("spell");

                //Start Cooldown process;
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
            yield return new WaitForSeconds(Cooldown);
            isAvailable = true;
        }
    }

}
