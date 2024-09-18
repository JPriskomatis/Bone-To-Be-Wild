using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialoguespace
{

    public interface OnDialogue
    {
        string npcName { get; set; }
        TextAsset inkJSON { get; set; }
        AudioSource greetingAudio { get; set; }
        
        void OnPlayerEnterRange(Collider Player);
    }

}