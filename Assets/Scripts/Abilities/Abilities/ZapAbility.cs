using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySpace
{
    public class ZapAbility : MonoBehaviour, IABility
    {
        public float Cooldown { get; private set; } = 2f;
        private bool isAvailable = true;

        [SerializeField] Animator anim;

        private Camera playerCamera;

        private void Start()
        {
            playerCamera = Camera.main;
        }
        public void Activate()
        {
            if(isAvailable)
            {
                Debug.Log("Zap");

                GameObject closestEnemy = FindClosestTarget();

                if(closestEnemy != null)
                {
                    closestEnemy.GetComponent<TEST_ABILITY>().SpellDamageable();
                }
                //Perform the animation;
                anim.SetTrigger("spell");

                //Deal Damage;

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

        private GameObject FindClosestTarget()
        {
            RaycastHit hit;
            if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    return hit.collider.gameObject;
                }
            }
            return null;
        }
    }

}
