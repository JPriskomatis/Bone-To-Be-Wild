using NPCspace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToChase : MonoBehaviour
{
    public void TransitionAnimation()
    {
        GetComponentInChildren<Bandit>().CycleAnimation();
    }
}
