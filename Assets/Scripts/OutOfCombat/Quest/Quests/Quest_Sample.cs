using questSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace questSpace
{
    public class Quest_Sample : Base_Quest
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                SetUI();
            }
        }
    }

}