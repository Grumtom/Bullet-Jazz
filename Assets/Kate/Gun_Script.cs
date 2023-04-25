using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Script : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject beatBullet;
    [SerializeField] private GameObject target;
    [SerializeField] private float shotSpeed;
    [SerializeField] private float beatShotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Reticule Pivot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(bool beat)
    {
        GameObject shot;
        if (beat)
        {
            shot = Instantiate(beatBullet, transform.position, transform.rotation, transform);
            Bullet bulletDeets = shot.GetComponent<Bullet>();
            bulletDeets.speed = beatShotSpeed;
            //shot.transform.rotation = Quaternion.LookRotation(target.transform.position - shot.transform.position);
        }
        else
        {
            shot = Instantiate(bullet, transform.position, transform.rotation, transform);
            Bullet bulletDeets = shot.GetComponent<Bullet>();
            bulletDeets.speed = shotSpeed;
            //shot.transform.rotation = Quaternion.LookRotation(target.transform.position - shot.transform.position);
        }
        if (shot != null)
            shot.transform.parent = null;
    }
}
