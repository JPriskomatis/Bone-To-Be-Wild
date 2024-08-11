using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_NPC : MonoBehaviour, ISpellDamageable
{
    public void SpellDamageable()
    {
        Debug.Log(transform.root.gameObject.name + " was hurt!");
    }
}
