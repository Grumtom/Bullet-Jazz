using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Sprite_Manager : MonoBehaviour
{
    public float lookAngle;
    public GameObject aimer;
    private int pastDir;
    private int facingDir;
    public int weapon;
    public int weaponsCount = 5;

    [SerializeField]
        [Header("start at right, go counterclockwise")]
        private GameObject[] directions;

        
    [Header("guns")] 
    [SerializeField] 
    public GameObject[] guns;
    
    [SerializeField]
    private List<GameObject> revolvers;
    [SerializeField]
    private List<GameObject> trumpets;
    [SerializeField]
    private List<GameObject> horns;
    [SerializeField]
    private List<GameObject> aks;
    [SerializeField]
    private List<GameObject> swords;

    private void Start()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            revolvers.Add(guns[i].transform.Find("Revolver").gameObject);
            trumpets.Add(guns[i].transform.Find("Trumpet").gameObject);
            horns.Add(guns[i].transform.Find("Horn").gameObject);
            aks.Add(guns[i].transform.Find("Ak 47").gameObject);
            swords.Add(guns[i].transform.Find("Sword").gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            weapon += 1;
            if (weapon == weaponsCount)
            {
                weapon -= weaponsCount;
                
            }
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
        for (int i = 0; i < guns.Length; i++)
        {
            revolvers[i].SetActive(false);
            trumpets[i].SetActive(false);
            horns[i].SetActive(false);
            aks[i].SetActive(false);
            swords[i].SetActive(false);
        }


        for (int i = 0; i < guns.Length; i++)
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
            }
        }
        
    }
}
