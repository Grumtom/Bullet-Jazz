using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShot : MonoBehaviour
{
    public float scaleMod = 0.5f;
    public GameObject BulletType;
    public GameObject target;
    public BulletInfo info;
    public float timeWaited = 0f;
    public float timeToWait = 1f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject newbullet = Instantiate(BulletType, transform.position, transform.rotation, transform);
        newbullet.GetComponent<BulletInfo>().speed = info.speed;
        newbullet.GetComponent<BulletInfo>().scale = info.scale;
        newbullet.GetComponent<BulletInfo>().damage = info.damage;
        newbullet.transform.parent = null;

        float bPM = BeatSender.GiveInstance().bPM;

        //calculate expected seconds between beats
        float bps = bPM / 60f; //BPM divided by SPM(seconds per minute) [the units mason]
        float spb = 1 / bps; //seconds per beat
        timeToWait = 0.5f * spb;
    }

    // Update is called once per frame
    void Update()
    {
        timeWaited += Time.deltaTime;
        if(timeWaited>timeToWait)
        {
            GameObject newbullet = Instantiate(BulletType, transform.position, transform.rotation, transform);
            newbullet.GetComponent<BulletInfo>().speed = info.speed;
            newbullet.GetComponent<BulletInfo>().scale = info.scale * scaleMod;
            newbullet.GetComponent<BulletInfo>().damage = info.damage;
            newbullet.transform.parent = null;
            Destroy(gameObject);
        }
    }
}
