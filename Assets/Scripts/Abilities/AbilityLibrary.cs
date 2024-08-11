using UnityEngine;

namespace AbilitySpace
{
    public class AbilityLibrary : MonoBehaviour
    {
        // References to all abilities
        public IABility zapAbility;

        // Add more abilities as needed...

        private void Awake()
        {
            // Assume the abilities are attached to the same GameObject or manually assigned
            zapAbility = GetComponent<ZapAbility>();
            // Initialize other abilities similarly...
        }

        public IABility GetAbilityByName(string abilityName)
        {
            switch (abilityName)
            {
                case "ZapAbility":
                    return zapAbility;
                default:
                    Debug.LogWarning("Ability not found!");
                    return null;
            }
        }
    }
}
