using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatTracker : MonoBehaviour
{


    private BeatSender beatSender;
    [SerializeField]private float rectXStart;
    [SerializeField]private float rectXEnd;
    [SerializeField] private int beatCount;

    public Image slider;

    private void Start()
    {

        beatSender = BeatSender.GiveInstance();

    }

    public void BeatHappened()
    {
        
        beatCount++;
        if (beatCount > 4)
        {
            beatCount = 1;
            slider.rectTransform.localPosition = new Vector3(rectXStart, slider.rectTransform.localPosition.y);
        }
    }

    private void FixedUpdate()
    {

        if(beatCount < 4)
        {
            slider.rectTransform.localPosition += Vector3.right * DistanceOverTime() * Time.deltaTime;
        }

    }

    private float DistanceOverTime()
    {

        float speed = rectXEnd - rectXStart;
        speed /= 3;
        speed /= (60 / beatSender.bPM);
        Debug.Log(speed);

        return speed;

    }

}
