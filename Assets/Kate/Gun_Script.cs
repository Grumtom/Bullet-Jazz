using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Script : MonoBehaviour
{
   
    [SerializeField] public int weaponIndex = 0;
    [SerializeField] private GameObject[] bullet;
    [SerializeField] private GameObject[] beatBullet;
    [SerializeField] private float[] shotSpeed;
    [SerializeField] private float[] beatShotSpeed;
    [SerializeField] private GameObject target;
    public Character_Sprite_Manager man;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Reticule Pivot");
        man = transform.parent.GetComponentInChildren<Character_Sprite_Manager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(bool beat)
    {
        weaponIndex = man.weapon;
        GameObject shot;
        if (beat)
        {
            shot = Instantiate(beatBullet[(int)weaponIndex], transform.position, transform.rotation, transform);
            BulletInfo bulletDeets;
            if (shot.TryGetComponent<BulletInfo>(out bulletDeets))
                bulletDeets.speed = beatShotSpeed[(int)weaponIndex];
            shot.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.Cross(target.transform.position - shot.transform.position, Vector3.up));
        }
        else
        {
            shot = Instantiate(bullet[(int)weaponIndex], transform.position, transform.rotation, transform);
            BulletInfo bulletDeets;
            if (shot.TryGetComponent<BulletInfo>(out bulletDeets))
                bulletDeets.speed = shotSpeed[(int)weaponIndex];
            shot.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.Cross(target.transform.position - shot.transform.position, Vector3.up));
        }
        if (shot != null)
            shot.transform.parent = null;
    }
}
