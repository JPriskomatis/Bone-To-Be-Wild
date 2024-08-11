using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySpace
{
    /// <summary>
    /// Script that allows us to acquire/remove and activate our abilities in-game;
    /// Player should have this script 
    /// </summary>
    public class AbilityManager : MonoBehaviour
    {
        private List<IABility> abilities = new List<IABility>();

        public void AddAbility(IABility ability)
        {
            abilities.Add(ability);
        }

        public void RemoveAbility(IABility ability)
        {

        abilities.Remove(ability); 

        }

        public void ActivateAbility(int index)
        {
            if (index >= 0 && index < abilities.Count)
            {
                abilities[index].Activate();
            }
            else
            {
                Debug.Log("No abilities found!");
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                ActivateAbility(0);
            }
        }


    }

}
