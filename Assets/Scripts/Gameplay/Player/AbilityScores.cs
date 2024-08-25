using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    public class AbilityScores : MonoBehaviour
    {
        public static event Action<int> OnCurrentHealthIncrease;
        public static event Action<int> OnCurrentHealthDecrease;

        [System.Serializable]   //We make them serializable so that we can modify them through inspector;
        public class MainStats
        {
            public int maxHP;
            public int currentHP;

            //How powerful our melee attacks are;
            public int strength = 10;
            //How agile our player moves;
            public int agility;
            //How much damage our player can withstand;
            public int endurance;
            //How good our player is in social encoutners - get discounts, more dialogue options;
            public int charm;
            //How powerful our player's magical attacks are;
            public int arcana;
            //How lucky our player is - better loot drops, weapons, critical strikes;
            public int luck;

        }



        public MainStats mainStats;    //We create an instance of the stats so we can view them on the inspector;

        //We use an enum so we can "select" which mainStat to modify;
        public enum StatType
        {
            Strength,
            Agility,
            Endurance,
            Charm,
            Arcana,
            Luck,
            CurrentHP,
            MaxHP
        }

        private void Awake()
        {
            //Initialize all ability scores to 10;
            mainStats.strength = 10;
            mainStats.agility = 10;
            mainStats.endurance = 10;
            mainStats.charm = 10;
            mainStats.arcana = 10;
            mainStats.luck = 10;

            mainStats.maxHP = 25;

            //Set our Health;
            mainStats.currentHP = mainStats.maxHP;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
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
                case StatType.Strength:
                    mainStats.strength += increaseAmount;
                    break;
                case StatType.Agility:
                    mainStats.agility += increaseAmount;
                    break;
                case StatType.Endurance:
                    mainStats.endurance += increaseAmount;
                    break;
                case StatType.Charm:
                    mainStats.charm += increaseAmount;
                    break;
                case StatType.Arcana:
                    mainStats.arcana += increaseAmount;
                    break;
                case StatType.Luck:
                    mainStats.luck += increaseAmount;
                    break;
                case StatType.CurrentHP:
                    OnCurrentHealthIncrease?.Invoke(increaseAmount);
                    mainStats.currentHP -= increaseAmount;
                    break;
                case StatType.MaxHP:
                    mainStats.maxHP -= increaseAmount;
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
                case StatType.Strength:
                    mainStats.strength -= decreaseAmount;
                    break;
                case StatType.Agility:
                    mainStats.agility -= decreaseAmount;
                    break;
                case StatType.Endurance:
                    mainStats.endurance -= decreaseAmount;
                    break;
                case StatType.Charm:
                    mainStats.charm -= decreaseAmount;
                    break;
                case StatType.Arcana:
                    mainStats.arcana -= decreaseAmount;
                    break;
                case StatType.Luck:
                    mainStats.luck -= decreaseAmount;
                    break;
                case StatType.CurrentHP:
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

    }

}