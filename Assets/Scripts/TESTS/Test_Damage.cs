using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySpace;
using PlayerSpace;
public class Test_Damage : MonoBehaviour
{
    bool test;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (!test)
            {
                Debug.Log("Deal Damage");
                other.GetComponent<AbilityScores>().DecreaseStat(AbilityScores.StatType.CurrentHP, 5);
                test = true;
            }
            
        }

    }
}
