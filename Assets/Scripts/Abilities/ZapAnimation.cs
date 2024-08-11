using PlayerSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapAnimation : MonoBehaviour
{
    private PlayerMovement PlayerMovement;

    private void Start()
    {
        PlayerMovement = GetComponentInParent<PlayerMovement>();
    }
    public void CastZap()
    {
        PlayerMovement.CastSpell();
    }
}
