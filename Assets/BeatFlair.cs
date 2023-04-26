using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatFlair : MonoBehaviour
{
    public bool beat = false;

    private void Update()
    {
        if (beat)
        {
            beatFlair();
            beat = false;
        }
    }

    public void beatFlair()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Flair");
    }
}
