using PlayerSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    public void CastSpellInstance()
    {
        GetComponentInParent<PlayerMovement>().CastSpell();
    }
}
