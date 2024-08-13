using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_NPC : MonoBehaviour, ISpellDamageable
{
    [SerializeField] Animator anim;
    public void SpellDamageable()
    {
        Debug.Log(transform.root.gameObject.name + " was hurt!");
    }

    public void Death()
    {
        anim.SetTrigger("death");
    }
}
