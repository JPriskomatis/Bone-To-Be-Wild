 using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GameToUI;


namespace UI
{
    public class Enemy_UI : MonoBehaviour
    {
        [Header("Monster UI")]
        public TextMeshProUGUI monsterName;
        public TextMeshProUGUI monsterLevel;
        public Image monsterIcon;
        [SerializeField] private Canvas monsterCanvas;
        public Slider slider;

        public Enemy_Level monsterLevelEnum;
        

        private void Start()
        {
            SetUI(monsterName.text, monsterIcon.sprite, monsterLevelEnum.level.ToString());
        }
        private void Update()
        {
            Vector3 directionToCamera = Camera.main.transform.position - monsterCanvas.transform.position;
            directionToCamera.y = 0; // Zero out the y-axis to prevent tilting

            monsterCanvas.transform.rotation = Quaternion.LookRotation(directionToCamera);
        }

        public void SetUI(string name, Sprite icon, string level)
        {
            monsterName.text = name;
            monsterIcon.sprite = icon;
            monsterLevel.text = level;
        }

        public void UpdateSlider(float currentHealth, int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = currentHealth;
        }



    }

}