using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Enemy_UI : MonoBehaviour
    {
        [Header("Monster UI")]
        public TextMeshProUGUI monsterName;
        public TextMeshProUGUI monsterLevel;
        public Image monsterIcon;
        [SerializeField] private Canvas monsterCanvas;
        public void SetUI(string name, Sprite icon, string level)
        {
            monsterName.text = name;
            monsterIcon.sprite = icon;
            monsterLevel.text = level;
        }

        private void Update()
        {
            Vector3 directionToCamera = Camera.main.transform.position - monsterCanvas.transform.position;
            directionToCamera.y = 0; // Zero out the y-axis to prevent tilting

            monsterCanvas.transform.rotation = Quaternion.LookRotation(directionToCamera);
        }

    }

}