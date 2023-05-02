using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBulletSpawn : MonoBehaviour
{
    public float Spread;
    public int BulletCount;
    public GameObject BulletType;
    public GameObject target;
    public BulletInfo info;
    // Start is called before the first frame update
    void Start()
    {
        //Direction given by spawner
        float SpreadInRad = Spread * Mathf.Deg2Rad;
        float cA = -Spread / 2; 
        for (int i = 0; i < BulletCount; i++)
        {
            GameObject newbullet = Instantiate(BulletType, transform.position, transform.rotation, transform);
            newbullet.transform.RotateAround(newbullet.transform.position, Vector3.down, cA);
            newbullet.GetComponent<BulletInfo>().speed = info.speed;
            newbullet.GetComponent<BulletInfo>().scale = info.scale;
            newbullet.GetComponent<BulletInfo>().damage = info.damage;
            cA += Spread / (BulletCount - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
