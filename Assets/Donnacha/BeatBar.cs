using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBar : MonoBehaviour
{

    public float timeAcross;
    private List<Vector3> startingPoints = new List<Vector3>();
    [SerializeField] Transform endPoint;
    [SerializeField] List<Transform> beatMovers = new List<Transform>();
    private float startTime;

    private List<float> speeds = new List<float>();

    private void Start()
    {

        timeAcross = BeatSender.GiveInstance().secondsPerBeat;
        startingPoints.Add(beatMovers[0].position);
        startingPoints.Add(beatMovers[1].position);

        speeds.Add(Vector3.Distance(startingPoints[0], endPoint.position) / timeAcross);
        speeds.Add(Vector3.Distance(startingPoints[1], startingPoints[0]) / timeAcross);

    }

    public void BeatHappened()
    {

        beatMovers[0].position = startingPoints[0];
        beatMovers[1].position = startingPoints[1];
        startTime = Time.time;

        Invoke(nameof(AnimBeat), timeAcross / 6);


    }

    private void FixedUpdate()
    {

        float distancePercent0 = (speeds[0] * (Time.time - startTime))/ Vector3.Distance(startingPoints[0], endPoint.position);
        float distancePercent1 = (speeds[1] * (Time.time - startTime))/ Vector3.Distance(startingPoints[1], startingPoints[0]);

        beatMovers[0].position = Vector3.Lerp(startingPoints[0], endPoint.position, distancePercent0);
        beatMovers[1].position = Vector3.Lerp(startingPoints[1], startingPoints[0], distancePercent1);

    }

    private void AnimBeat()
    {
        GetComponent<Animator>().SetTrigger("BeatHappen");
    }

}
