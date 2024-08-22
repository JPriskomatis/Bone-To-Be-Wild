using UnityEngine;

namespace AbilitySpace
{
    public class AbilityLibrary : MonoBehaviour
    {
        //We reference all our abilities here;
        public IABility zapAbility;

        private void Awake()
        {
            //We instantiate our abilities here;
            zapAbility = GetComponent<ZapAbility>();

        }

        public IABility GetAbilityByName(string abilityName)
        {
            switch (abilityName)
            {
                case "ZapAbility":
                    //We activate the icon;
                    FindObjectOfType<ZapAbility>().ActivateIcon();
                    return zapAbility;
                default:
                    Debug.LogWarning("Ability not found!");
                    return null;
            }
        }
    }
}
