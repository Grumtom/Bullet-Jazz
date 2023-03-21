using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunFire : MonoBehaviour
{
    [SerializeField] private GunType Type;
    public enum  GunType
    {
        NULL,
        REVOLVER,
        TRUMPET,
        HORN,
        AK47,//?
        SWORD//?
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Fire()
    {

    }
}
