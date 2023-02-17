using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Sprite_Manager : MonoBehaviour
{
    public float lookAngle;
    public GameObject aimer;
    public int facingDir;
    
    [Header("start at right, go counterclockwise")]
    public GameObject[] directions;

    // Update is called once per frame
    void Update()
    {
        lookAngle = aimer.transform.localEulerAngles.z ;
        lookAngle /= 45f; // dividing it by 360 and multiplying by 8, to get a number between zero and 8
       // lookAngle -= 0.499f;
        if (lookAngle > 7.5) lookAngle -= 8;
        facingDir = Mathf.RoundToInt(lookAngle);


        for (int i = 0; i < directions.Length; i++)
        {
            if (i == facingDir)
            {
                directions[i].SetActive(true);
            }
            else
            {
                directions[i].SetActive(false);
            }
        }
    }
}
