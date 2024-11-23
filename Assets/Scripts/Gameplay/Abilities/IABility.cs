using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace AbilitySpace
{
    /// <summary>
    /// All our abilities should implement this interface as they have the following common characteristics;
    /// </summary>
    public interface IABility
    {
        void Activate();
        void Deactivate();
        float Cooldown { get; }
        bool IsAvailable();

        Sprite GetImage();
        void SetAbilityIcon(Image icon);
        void SetCooldownIcon(GameObject go_cooldown);
    }

}
