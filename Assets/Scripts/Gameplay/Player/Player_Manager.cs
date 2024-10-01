using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    public class Player_Manager : MonoBehaviour
    {
        public static Player_Manager instance { get; private set; }

        private void Awake()
        {
            if(instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }
    }

}