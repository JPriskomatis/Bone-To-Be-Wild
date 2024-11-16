using Audio;
using NPCspace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCspace
{
    public class Civ_Dialogue : MonoBehaviour
    {
        //We don't want the civilian to always be greeting the player
        int randomNumber;
        int randomSpeech;
        [SerializeField] private string[] greetingMessages;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player");
                
                //20% chances of greeting
                randomNumber = Random.Range(0, 5);
                randomSpeech = Random.Range(0, greetingMessages.Length);
                if(randomNumber == 1)
                {
                    AudioManager.instance.PlaySFX(greetingMessages[randomSpeech]);
                }
            }
        }
    }

}