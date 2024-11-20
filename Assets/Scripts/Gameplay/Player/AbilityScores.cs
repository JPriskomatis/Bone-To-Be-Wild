using log4net.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        [Header("Stats UI")]
        [SerializeField] private TextMeshProUGUI powerTxt;
        [SerializeField] private TextMeshProUGUI vigorTxt;
        [SerializeField] private TextMeshProUGUI knowledgeTxt;
        [SerializeField] private TextMeshProUGUI fateTxt;
        [SerializeField] private TextMeshProUGUI charismaTxt;
        [SerializeField] private TextMeshProUGUI currentXPTxt;
        [SerializeField] private TextMeshProUGUI levelXPTxt;

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
        [System.Serializable]
        public class SecondaryStats
        {
            public int currentXP;
            public int LevelUpXP;
        }

        public MainStats mainStats;    //We create an instance of the stats so we can view them on the inspector;
        public SecondaryStats secondaryStats;

        //We use an enum so we can "select" which mainStat to modify;
        public enum StatType
        {
            Power,
            Vigor,
            Knowledge,
            Fate,
            Charisma,
            CurrentHP,
            MaxHP,
            currentXP,
            LevelUpXP,
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

            mainStats = new MainStats();
            secondaryStats = new SecondaryStats();

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

            //XP:
            secondaryStats.currentXP = 0;
            secondaryStats.LevelUpXP = 100;
        }
        private void Start()
        {
            SetUI();
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

                case StatType.currentXP:
                    if (secondaryStats.currentXP + increaseAmount > secondaryStats.LevelUpXP)
                    {
                        //Invoke the level up event;
                        increaseAmount = secondaryStats.LevelUpXP- secondaryStats.currentXP;
                        secondaryStats.currentXP = increaseAmount;
                        secondaryStats.LevelUpXP = secondaryStats.LevelUpXP * 2;
                    }
                    //Here invoke the Level up event
                    secondaryStats.currentXP+= increaseAmount;
                    break;
                default:
                    Debug.LogError("Unknown stat type.");
                    break;
            }
            SetUI();
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
            SetUI();
        }


        public void IncreaseLevel()
        {
            level++;
            secondaryStats.currentXP = secondaryStats.currentXP - secondaryStats.LevelUpXP;
            secondaryStats.LevelUpXP = secondaryStats.LevelUpXP * 2;

            mainStats.maxHP += 5;
            mainStats.currentHP += 5;
            OnLevelUp?.Invoke();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                GainXP(25);
            }
        }
        public void GainXP(int amount)
        {
            secondaryStats.currentXP += amount;
            if(secondaryStats.currentXP >= secondaryStats.LevelUpXP)
            {
                //Level up;
                IncreaseLevel();
            }
            SetUI();
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

        public void SetUI()
        {
            powerTxt.text = mainStats.power.ToString();
            vigorTxt.text = mainStats.vigor.ToString();
            knowledgeTxt.text = mainStats.knowledge.ToString();
            fateTxt.text = mainStats.fate.ToString();
            charismaTxt.text = mainStats.charisma.ToString();
            currentXPTxt.text = secondaryStats.currentXP.ToString()+"/ ";
            levelXPTxt.text = secondaryStats.LevelUpXP.ToString();
        }
    }


}