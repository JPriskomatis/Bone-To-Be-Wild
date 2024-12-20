using AbilitySpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_ABILITY : MonoBehaviour
{
    private AcquireAbility acquireAbility;

    [SerializeField] private string abilityName;

    private void Start()
    {
        acquireAbility = FindObjectOfType<AcquireAbility>();
    }

    
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.G))
        {
            acquireAbility.Acquire(abilityName);
        }
    }


}
