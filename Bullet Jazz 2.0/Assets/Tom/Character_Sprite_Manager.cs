using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Sprite_Manager : MonoBehaviour
{
    public float lookAngle;
    public GameObject aimer;
    private int pastDir;
    private int facingDir;
    private int weapon;

    [SerializeField]
        [Header("start at right, go counterclockwise")]
        private GameObject[] directions;

        
    [Header("guns")] 
    [SerializeField] 
    public GameObject[] guns;
    
    [SerializeField]
    private GameObject[] revolvers;
    [SerializeField]
    private GameObject[] trumpets;
    [SerializeField]
    private GameObject[] horns;
    [SerializeField]
    private GameObject[] aks;
    [SerializeField]
    private GameObject[] swords;


// Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            weapon += 1;
            if (weapon == guns.Length)
            {
                weapon -= guns.Length;}
            switchWeapons(weapon);
        }
        
        
        
        lookAngle = aimer.transform.localEulerAngles.z ;
        lookAngle /= 45f; // dividing it by 360 and multiplying by 8, to get a number between zero and 8
       // lookAngle -= 0.499f;
        if (lookAngle > 7.5) lookAngle -= 8;
        facingDir = Mathf.RoundToInt(lookAngle);
        if (facingDir == pastDir)
        {
            return;
        }

        pastDir = facingDir;
        

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

    void switchWeapons(int newGun)
    {
        for (int i = 0; i < directions.Length; i++)
        {
            revolvers[i].SetActive(false);
            trumpets[i].SetActive(false);
            horns[i].SetActive(false);
            aks[i].SetActive(false);
            swords[i].SetActive(false);
        }


        for (int i = 0; i < directions.Length; i++)
        {
            switch (newGun)
            {
                case 0:
                    revolvers[i].SetActive(true);
                    break;
                case 1:
                    trumpets[i].SetActive(true);
                    break;
                case 2:
                    horns[i].SetActive(true);
                    break;
                case 3:
                    aks[i].SetActive(true);
                    break;
                case 4:
                    swords[i].SetActive(true);
                    break;
                default:
                    print("thats not a gun");
                    return;
                    break;
            }
        }
        
    }
}
