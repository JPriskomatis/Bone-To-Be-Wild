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

        [SerializeField] private GameObject sheatedPosition;
        private bool sheathedSword;


        // Variables to store the original position, rotation, and parent of the sword
        private Vector3 originalPosition;
        private Quaternion originalRotation;
        private Transform originalParent;

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
                    if (weapon_base != null && !sheathedSword)
                    {

                        Attack();
                    }
                    else
                    {
                        StartCoroutine(UnSheathSword());
                    }
                }
                
                if (Input.GetMouseButtonDown(1))
                {
                    StartCoroutine(SheathSword());
                }
            }
        }

        private IEnumerator UnSheathSword()
        {
            anim.SetTrigger("unSheath");

            yield return new WaitForSeconds(0.51f);

            // Restore the sword's original parent, position, and rotation

            this.transform.SetParent(originalParent);

            this.transform.localPosition = new Vector3(-0.0770029873f, 0.196003392f, -0.0280032828f);
            this.transform.localRotation = new Quaternion(0.775775492f, -0.363568425f, 0.0150310155f, 0.515523553f);




            sheathedSword = false;
        }
        private IEnumerator SheathSword()
        {

            originalPosition = this.transform.position;
            originalRotation = this.transform.rotation;
            originalParent = this.transform.parent;

            anim.SetTrigger("sheath");

            yield return new WaitForSeconds(1f);

            this.transform.position = sheatedPosition.transform.position;
            this.transform.rotation = sheatedPosition.transform.rotation;

            // Make the sword a child of the sheathed position
            this.transform.SetParent(sheatedPosition.transform);

            sheathedSword = true;



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