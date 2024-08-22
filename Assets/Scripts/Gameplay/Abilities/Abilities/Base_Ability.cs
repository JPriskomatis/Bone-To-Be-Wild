using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AbilitySpace
{
    public abstract class Base_Ability : MonoBehaviour
    {
        protected void UpdateAbilityUI(Image abilityIcon, bool isAvailable, float alphaValue, GameObject cooldownTimer, float cooldownValue)
        {
            cooldownTimer.SetActive(true);
            Color color = abilityIcon.color;
            color.a = alphaValue;
            abilityIcon.color = color;

            TextMeshPro textMeshPro = cooldownTimer.GetComponent<TextMeshPro>();
            if (textMeshPro != null)
            {
                
                int roundedCooldownValue = Mathf.RoundToInt(cooldownValue);
                textMeshPro.text = roundedCooldownValue.ToString();
            }
        }
    }

}
