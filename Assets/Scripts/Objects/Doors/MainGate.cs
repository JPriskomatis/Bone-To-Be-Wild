using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using PlayerSpace;
using Audio;
using Dialoguespace;

public class MainGate : Base_Door
{
    private Vector3 originalPos;
    private bool open;


    private void OnEnable()
    {
        Gate_Guard_Dialogue.OnGateOpen += UnlockGate;
    }

    private void OnDisable()
    {
        Gate_Guard_Dialogue.OnGateOpen -= UnlockGate;
    }
    private void Awake()
    {
        locked = true;
    }
    private void Start()
    {
        originalPos = transform.position;
        
    }
    public override void Interact()
    {
        if(Input.GetKeyDown(KeyCode.E) && !locked)
        {
            //Opening the Gate;
            open = true;
            MoveGateOverTime(originalPos, new Vector3(originalPos.x, originalPos.y + 15f, originalPos.z), 2.5f).Forget();
            //The .Forget() allows us to run an asynchronous task without awaiting it and without needing to handle the result;
        }
    }

    //Move the gate upwards;
    private async UniTask MoveGateOverTime(Vector3 start, Vector3 end, float duration)
    {
        //Play Audio
        PlayAudio();

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            //Now we wait till the next frame before entering the while loop again;
            await UniTask.Yield();
        }
        transform.position = end;

        
        //We start the checkPlayerDistance function to close the gate;
        if (open)
        {
            CheckPlayerDistance().Forget();

        }
    }

    private void PlayAudio()
    {
        AudioManager.instance.PlaySFX("OpenGate", 0.5f);
    }

    //Check if player is away from the gate;
    private async UniTask CheckPlayerDistance()
    {
        open = false;
        float dist = 20f;
        GameObject player = FindObjectOfType<PlayerMovement>().gameObject;
        while (Vector3.Distance(player.transform.position, this.transform.position) < dist)
        {
            //Check every 1 second;
            await UniTask.Delay(1);
        }
        MoveGateOverTime(this.transform.position, originalPos, 2.5f).Forget();
    }

    public void UnlockGate()
    {
        Debug.Log("Unlocked");
        locked = false;
    }
}
