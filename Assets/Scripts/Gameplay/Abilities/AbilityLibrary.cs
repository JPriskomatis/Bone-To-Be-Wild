using UnityEngine;

namespace AbilitySpace
{
    public class AbilityLibrary : MonoBehaviour
    {
        //We reference all our abilities here;
        public IABility zapAbility, flamingStrike;

        private void Awake()
        {
            //We instantiate our abilities here;
            zapAbility = GetComponent<ZapAbility>();
            flamingStrike = GetComponent<FlamingStrike>();

        }

        public IABility GetAbilityByName(string abilityName)
        {
            switch (abilityName)
            {
                case "ZapAbility":
                    
                    FindObjectOfType<ZapAbility>().enabled = true;
                    return zapAbility;
                case "FlamingStrike":

                    FindObjectOfType<FlamingStrike>().enabled = true;
                    return flamingStrike;
                default:
                    Debug.LogWarning("Ability not found!");
                    return null;
            }
        }
    }
}
