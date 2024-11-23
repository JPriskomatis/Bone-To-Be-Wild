using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UI;
using UnityEngine.UI;

namespace AbilitySpace
{
    /// <summary>
    /// Script that allows us to acquire/remove and activate our abilities in-game;
    /// Player should have this script.
    /// Each time a new Ability is acquired (through AcquireAbility.cs) we assign it to a new keycode;
    /// The first ability goes to Q and the second ability goes to E;
    /// </summary>
    public class AbilityManager : MonoBehaviour
    {
        //List of our abilities;
        private List<IABility> abilities = new List<IABility>();

        //List of our keycodes for abilities;
        [SerializeField] private List<KeyCode> abilityKeys = new List<KeyCode> { KeyCode.Q, KeyCode.E };

        [SerializeField] private List<GameObject> go_abilityIcons = new List<GameObject>();

        private Dictionary<KeyCode, GameObject> abilityIcons = new Dictionary<KeyCode, GameObject>();

        private void Start()
        {
            for (int i = 0; i < abilityKeys.Count; i++)
            {
                abilityIcons[abilityKeys[i]] = go_abilityIcons[i];
            }
        }

        private void Update()
        {
            
            for (int i = 0; i < abilityKeys.Count; i++)
            {
                if (Input.GetKeyDown(abilityKeys[i]))
                {
                    ActivateAbility(i);
                }
            }
        }


        public void AddAbility(IABility ability)
        {
            if (!abilities.Contains(ability))
            {
                if (abilities.Count < abilityKeys.Count)
                {
                    
                    abilities.Add(ability);
                    Debug.Log($"Ability added: {ability.GetType().Name} assigned to key {abilityKeys[abilities.Count - 1]}");

                    //We want to add the ability to the correct UI gameobject now;
                    KeyCode assignedKey = abilityKeys[abilities.Count - 1];

                    if (abilityIcons.TryGetValue(assignedKey, out GameObject abilityUIObject))
                    {
                        //Activate the corresponding GameObject;
                        abilityUIObject.SetActive(true);
                        Image icon = abilityUIObject.GetComponent<Image>();

                        icon.sprite = ability.GetImage();
                        ability.SetAbilityIcon(icon);

                        Debug.Log($"Activated UI GameObject for {assignedKey}: {abilityUIObject.name}");

                        //Add the correct cooldownTime gameobject;

                        Debug.Log(abilityUIObject.transform.GetChild(1).gameObject.name);
                        ability.SetCooldownIcon(abilityUIObject.transform.GetChild(1).gameObject);
                        



                    }

                }

            }
        }



        public void RemoveAbility(IABility ability)
        {
            int index = abilities.IndexOf(ability);
            if (index != -1)
            {
                abilities.RemoveAt(index);
                Debug.Log($"Ability removed: {ability.GetType().Name}");
            }
            else
            {
                Debug.LogWarning("Ability not found.");
            }

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




    }

}
