using UnityEngine;

namespace AbilitySpace
{
    public class AcquireAbility : MonoBehaviour
    {
        [SerializeField] private AbilityManager abilityManager; // Reference to AbilityManager
        [SerializeField] private AbilityLibrary abilityLibrary; // Reference to AbilityLibrary


        private void Start()
        {
            if (abilityManager == null)
            {
                abilityManager = FindObjectOfType<AbilityManager>();
            }

            if (abilityLibrary == null)
            {
                abilityLibrary = FindObjectOfType<AbilityLibrary>();
            }
        }


        public void Acquire(string abilityName)
        {
            IABility ability = abilityLibrary.GetAbilityByName(abilityName);

            if (ability != null)
            {
                abilityManager.AddAbility(ability);
                Debug.Log($"{abilityName} has been acquired!");
            }
            else
            {
                Debug.LogWarning($"Ability '{abilityName}' could not be found in the AbilityLibrary.");
            }
        }
    }
}
