using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTimer : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed /= BeatSender.GiveInstance().secondsPerBeat;
    }


}
