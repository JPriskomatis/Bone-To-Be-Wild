using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Required for handling UI elements like Slider
using PlayerSpace;
using TMPro;

namespace UI
{
    [ExecuteInEditMode]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider; // Reference to the UI Slider component
        private AbilityScores abilityScores;
        [SerializeField] private float smoothSpeed = 0.5f; // Speed for smooth transition

        //Sprite change when low health;
        [SerializeField] private Sprite lowHealthIcon;
        [SerializeField] private Sprite highHealthIcon;

        [SerializeField] private Image heroIcon;
        [SerializeField] private TextMeshProUGUI heroLevel;

        private void OnEnable()
        {
            AbilityScores.OnCurrentHealthIncrease += IncreaseHealth;
            AbilityScores.OnCurrentHealthDecrease += DecreaseHealth;
            AbilityScores.OnLevelUp += IncreaseLevelTxt;
        }

        private void OnDisable()
        {
            AbilityScores.OnCurrentHealthIncrease -= IncreaseHealth;
            AbilityScores.OnCurrentHealthDecrease -= DecreaseHealth;
            AbilityScores.OnLevelUp -= IncreaseLevelTxt;
        }

        void Start()
        {
            abilityScores = FindObjectOfType<AbilityScores>();

            if (abilityScores == null) return;

            // Set the slider's max value to match the max health
            float maxHealth = abilityScores.mainStats.maxHP;
            float currentHealth = abilityScores.mainStats.currentHP;

            if (healthSlider != null)
            {
                healthSlider.maxValue = maxHealth;
                healthSlider.value = currentHealth;
            }
            heroLevel.text = abilityScores.level.ToString();
        }

        // This method is called when health is increased, with an integer parameter representing the health increase amount
        public void IncreaseHealth(int health)
        {
            if (abilityScores == null || healthSlider == null) return;

            float targetHealth = abilityScores.mainStats.currentHP + health;
            targetHealth = Mathf.Clamp(targetHealth, 0, abilityScores.mainStats.maxHP);
            StartCoroutine(SmoothHealthChange(targetHealth));
        }

        // This method is called when health is decreased, with an integer parameter representing the health decrease amount
        public void DecreaseHealth(int health)
        {
            if (abilityScores == null || healthSlider == null) return;

            float targetHealth = abilityScores.mainStats.currentHP - health;
            targetHealth = Mathf.Clamp(targetHealth, 0, abilityScores.mainStats.maxHP);
            StartCoroutine(SmoothHealthChange(targetHealth));
        }

        // Coroutine to smoothly transition the slider's value to the target value
        private IEnumerator SmoothHealthChange(float targetHealth)
        {
            float initialHealth = healthSlider.value;
            float elapsedTime = 0f;

            while (elapsedTime < smoothSpeed)
            {
                elapsedTime += Time.deltaTime;
                healthSlider.value = Mathf.Lerp(initialHealth, targetHealth, elapsedTime / smoothSpeed);
                yield return null;
            }

            // Set the final value to avoid small precision errors
            healthSlider.value = targetHealth;
            if(healthSlider.value < healthSlider.maxValue * 0.25)
            {
                heroIcon.sprite = lowHealthIcon;
            } else if(healthSlider.value > healthSlider.maxValue * 0.26)
            {
                heroIcon.sprite = highHealthIcon;
            }
        }

        private void IncreaseLevelTxt()
        {
            heroLevel.text = abilityScores.level.ToString();
        }
    }
}
