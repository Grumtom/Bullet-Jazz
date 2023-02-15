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
    private float secondsPerBeat;
    public int beatCount = 0;

    private void Awake()
    {
        myInstance = GetComponent<BeatSender>();
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

        foreach (BeatReciver reciver in recivers)
            reciver.HearBeat();
        Invoke(nameof(BeatHappen), secondsPerBeat);

    }

    
}
