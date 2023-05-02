using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody myRB;
    public BulletInfo info;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Color defaultCol;
    [SerializeField] private Color doubleCol;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        info = GetComponent<BulletInfo>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Color output = Color.Lerp(defaultCol, doubleCol, (info.damage - 1));
        output.a = 255;
        sprite.color = output;
        transform.parent = null;
        transform.localScale *= info.scale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = transform.right*info.speed;
        myRB.velocity = vel;
    }
}
