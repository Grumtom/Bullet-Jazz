using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
    public Animator anim;
    public float moveSpeed = 1;
    public GameObject aimer;
    public float lookAngle;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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

    void animate()
    {
      //  anim.SetFloat("");
    }
}
