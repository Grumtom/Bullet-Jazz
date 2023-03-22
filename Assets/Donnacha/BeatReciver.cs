using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatReciver : MonoBehaviour
{

    private void Start()
    {

        BeatSender.GiveInstance().recivers.Add(GetComponent<BeatReciver>());
        
    }

    public void HearBeat()
    {

        gameObject.SendMessage("BeatHappened", null, SendMessageOptions.DontRequireReceiver);

    }

}
