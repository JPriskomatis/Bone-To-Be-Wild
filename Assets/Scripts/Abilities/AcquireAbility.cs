using UnityEngine;

namespace AbilitySpace
{
    public class AcquireAbility : MonoBehaviour
    {
        [SerializeField] private AbilityManager abilityManager; // Reference to AbilityManager
        [SerializeField] private AbilityLibrary abilityLibrary; // Reference to AbilityLibrary
        [SerializeField] private string abilityName; // The name of the ability to acquire

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
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G)){
                Acquire();
            }
        }

        public void Acquire()
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
