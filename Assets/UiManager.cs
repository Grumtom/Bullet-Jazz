using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public int weaponIndex;
    public int health;
    public int pHealth;
    public Animator dmgUIAnim;
    public Character_Sprite_Manager playerSprites;
    public PlayerHealth PlayerHealth;
    public GameObject lowHP;
    public Image hearts;
    public Image revolver;
    public Image trumpet;
    public Image horn;
   
    
    // Update is called once per frame
    void Update()
    {
        health = PlayerHealth.HP;
        
        if (pHealth > health)
        {
            dmgUIAnim.SetTrigger("Damage");
        }

        pHealth = health;
        if (health < 1.1)
        {
            lowHP.SetActive(true);
        }
        else
        {
            lowHP.SetActive(false);
        }
        hearts.fillAmount = health / 6f;

        weaponIndex = playerSprites.weapon;

        switch (weaponIndex)
        {
            case 0:
                revolver.gameObject.SetActive(true);
                trumpet.gameObject.SetActive(false);
                horn.gameObject.SetActive(false);
                break;
            case 1:
                revolver.gameObject.SetActive(false);
                trumpet.gameObject.SetActive(true);
                horn.gameObject.SetActive(false);
                break;
            case 2:
                revolver.gameObject.SetActive(false);
                trumpet.gameObject.SetActive(false);
                horn.gameObject.SetActive(true);
                break;
        }
    }
}
