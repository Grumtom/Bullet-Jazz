using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private GameObject prefabBullet;
    // Update is called once per frame
    void Update()
    {

        transform.LookAt(target);

        
    }

    public void BeatHappened()
    {

        if (GetComponent<EnemyMovement>().myMode == EnemyMovement.EnemyMode.Strafe && BeatSender.GiveInstance().beatCount == 0) 
        {
            GameObject.Instantiate(prefabBullet, transform.position, transform.rotation).GetComponent<Rigidbody>().velocity = transform.forward * 8;
            GetComponent<EnemyMovement>().myMode = EnemyMovement.EnemyMode.Fire;
        }
        else
        {
            GetComponent<EnemyMovement>().myMode = EnemyMovement.EnemyMode.Fire;
        }

    }
}
