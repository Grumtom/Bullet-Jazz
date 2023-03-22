using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSender : MonoBehaviour
{

    private static BeatSender myInstance;
    public static BeatSender GiveInstance()
    {

        return myInstance;
    }

    public List<BeatReciver> recivers = new();

    public float bPM;
    public float secondsPerBeat;
    public int beatCount = 0;

    private void Awake()
    {
        if(myInstance == null)
            myInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        secondsPerBeat = 60f / bPM;

        Debug.Log(secondsPerBeat);
        BeatHappen();

    }

    public void BeatHappen()
    {
        beatCount++;
        if (beatCount >= 4)
            beatCount = 0;
        foreach (BeatReciver reciver in recivers)
            reciver.HearBeat();
        Invoke(nameof(BeatHappen), secondsPerBeat);


        Debug.Log("Beat");
    }

    
}
