using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Script : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject beatBullet;

    [SerializeField] private float shotSpeed;
    [SerializeField] private float beatShotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
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
        }
        else
        {
            shot = Instantiate(bullet, transform.position, transform.rotation, transform);
            Bullet bulletDeets = shot.GetComponent<Bullet>();
            bulletDeets.speed = shotSpeed;
        }
        if (shot != null)
            shot.transform.parent = null;
    }
}
