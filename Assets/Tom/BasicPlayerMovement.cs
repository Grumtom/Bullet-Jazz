using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerSchmoover = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        playerSchmoover.Normalize();
        playerSchmoover *= moveSpeed;
        playerSchmoover += new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        gameObject.GetComponent<Rigidbody2D>().MovePosition(playerSchmoover);
    }
}
