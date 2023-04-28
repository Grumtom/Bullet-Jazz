using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody myRB;
    public BulletInfo info;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        info = GetComponent<BulletInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = transform.right*info.speed;
        myRB.velocity = vel;
    }
}
