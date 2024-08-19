using AbilitySpace;
using PlayerSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapAnimation : MonoBehaviour
{
    [SerializeField] private ZapAbility zap;


    public void CastZap()
    {
        zap.CastSpell();
    }
}
