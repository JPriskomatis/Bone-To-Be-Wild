using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace AbilitySpace
{
    public class ZapAbility : MonoBehaviour, IABility
    {
        public float Cooldown { get; private set; } = 2f;
        private bool isAvailable = true;

        [SerializeField] Animator anim;

        private Camera playerCamera;
        [SerializeField] private float radius = 2f;
        [SerializeField] private float maxDinstance = 100f;


        public GameObject vfxPrefab; 
        public Vector3 offset = new Vector3(0, 0, 1);

        private GameObject enemyToTrack;

        private void Start()
        {
            playerCamera = Camera.main;
        }
        public void Activate()
        {
            if(isAvailable)
            {
                Debug.Log("Zap");

                //We find the closest enemy;
                enemyToTrack = FindClosestTarget();

                if(enemyToTrack != null)
                {
                    //We check if the enemy has the ISpellDamageable interface, meaning if we can deal
                    //damage to them with our spells;
                    ISpellDamageable spellDamageable = enemyToTrack.GetComponent<ISpellDamageable>();
                    if (spellDamageable != null)
                    {
                        spellDamageable.SpellDamageable();
                    }
                }
                //Perform the animation;
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
        public void CastSpell()
        {


            //Instantiate the VFX prefab at the closest target's position if a target exists
            if (enemyToTrack != null)
            {
                Vector3 targetPosition = enemyToTrack.transform.position;
                GameObject fvxInstance = Instantiate(vfxPrefab, targetPosition, Quaternion.identity);
                Destroy(fvxInstance, 1f);
            }
            else
            {
                // If no target is found, instantiate VFX at the player's position (or handle accordingly)
                Vector3 spawnPosition = transform.position + transform.TransformDirection(offset);
                GameObject fvxInstance = Instantiate(vfxPrefab, spawnPosition, Quaternion.identity);
                Destroy(fvxInstance, 1f);
            }
        }
        private GameObject FindClosestTarget()
        {
            RaycastHit hit;
            

            // Perform the raycast
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, maxDinstance))
            {
                // Check if the hit object has the "Enemy" tag
                if (hit.collider.CompareTag("Enemy"))
                {
                    return hit.collider.gameObject; // Return the GameObject that was hit
                }
            }
            return null;
        }


    }

}
