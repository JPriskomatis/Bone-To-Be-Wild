using log4net.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace PlayerSpace
{
    public class AbilityScores : MonoBehaviour
    {
        public static AbilityScores Instance;


        public static event Action<int> OnCurrentHealthIncrease;
        public static event Action<int> OnCurrentHealthDecrease;
        public static event Action OnLevelUp;

        [System.Serializable]   //We make them serializable so that we can modify them through inspector;
        public class MainStats
        {
            public int maxHP;
            public int currentHP;

            public int power = 10;
            public int vigor;
            public int knowledge;
            public int fate;
            public int charisma;
        }
        public int level;



        public MainStats mainStats;    //We create an instance of the stats so we can view them on the inspector;

        //We use an enum so we can "select" which mainStat to modify;
        public enum StatType
        {
            Power,
            Vigor,
            Knowledge,
            Fate,
            Charisma,
            CurrentHP,
            MaxHP
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            //Initialize all ability scores to 10;
            mainStats.power = 10;
            mainStats.vigor = 10;
            mainStats.knowledge = 10;
            mainStats.fate = 10;
            mainStats.charisma = 10;

            mainStats.maxHP = 25;

            //Set our Health;
            mainStats.currentHP = mainStats.maxHP;
            level = 1;
        }

        //ONLY FOR TESTING PURPOSES;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                //IncreaseLevel();
                DecreaseStat(AbilityScores.StatType.CurrentHP, 5);

            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                IncreaseStat(AbilityScores.StatType.CurrentHP, 5);

            }
        }

        //If we want to increase a player's stat
        //In order to use it, we call it as:
        //IncreaseStat(AbilityScores.StatType.Luck, 5);
        public void IncreaseStat(StatType statType, int increaseAmount)
        {
            switch (statType)
            {
                case StatType.Power:
                    mainStats.power += increaseAmount;
                    break;
                case StatType.Vigor:
                    mainStats.vigor += increaseAmount;
                    break;
                case StatType.Knowledge:
                    mainStats.knowledge += increaseAmount;
                    break;
                case StatType.Fate:
                    mainStats.fate += increaseAmount;
                    break;
                case StatType.Charisma:
                    mainStats.charisma += increaseAmount;
                    break;
                case StatType.CurrentHP:
                    if(mainStats.currentHP +  increaseAmount > mainStats.maxHP)
                    {
                        increaseAmount =  mainStats.maxHP - mainStats.currentHP;
                    }
                    OnCurrentHealthIncrease?.Invoke(increaseAmount);
                    mainStats.currentHP += increaseAmount;
                    break;
                case StatType.MaxHP:
                    mainStats.maxHP += increaseAmount;
                    break;
                default:
                    Debug.LogError("Unknown stat type.");
                    break;
            }
        }

        public void DecreaseStat(StatType statType, int decreaseAmount)
        {
            switch (statType)
            {
                case StatType.Power:
                    mainStats.power -= decreaseAmount;
                    break;
                case StatType.Vigor:
                    mainStats.vigor -= decreaseAmount;
                    break;
                case StatType.Knowledge:
                    mainStats.knowledge -= decreaseAmount;
                    break;
                case StatType.Fate:
                    mainStats.fate -= decreaseAmount;
                    break;
                case StatType.Charisma:
                    mainStats.charisma -= decreaseAmount;
                    break;
                case StatType.CurrentHP:
                    if (mainStats.currentHP - decreaseAmount < 0)
                    {
                        decreaseAmount = mainStats.currentHP;
                    }
                    OnCurrentHealthDecrease?.Invoke(decreaseAmount);
                    mainStats.currentHP -= decreaseAmount;
                    break;
                case StatType.MaxHP:
                    mainStats.maxHP -= decreaseAmount;

                    break;
                default:
                    Debug.LogError("Unknown stat type.");
                    break;
            }
        }

        public void IncreaseLevel()
        {
            level++;
            OnLevelUp?.Invoke();
        }

        public void TakeDamage(int damageTaken)
        {
            DecreaseStat(StatType.CurrentHP, damageTaken);
            UpdateBloodScreen(mainStats.currentHP, mainStats.maxHP);
        }
        public void UpdateBloodScreen(float currentHealth, float maxHealth)
        {
            float healthPercentage = (currentHealth / maxHealth) * 100f;
            BloodSplash.instance.SetBloodScreen(healthPercentage);
        }
    }

}