using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update

    public int HP = 3;
    [SerializeField] private GameObject destroyOnDeath;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy Bullet"))
        {
            HP--;
            Destroy(other.gameObject);
        }
        if(HP<=0)
        {
            Destroy(destroyOnDeath);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy Aoe"))
        {
            HP--;
            other.gameObject.SetActive(false);
        }
        if (HP <= 0)
        {
            Destroy(destroyOnDeath);
        }

    }
}