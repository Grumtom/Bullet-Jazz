using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun_Script : MonoBehaviour
{
   
    [SerializeField] public int weaponIndex = 0;
    [SerializeField] private GameObject[] bullet;
    [SerializeField] private GameObject[] beatBullet;
    [SerializeField] private float[] shotSpeed;
    [SerializeField] private float[] beatShotSpeed;
    [SerializeField] private GameObject target;
    public Character_Sprite_Manager man;
    [Header("0-3 is first wepon (damage buff), 4-7 is second (speed) and so on")]
    public float[] comboEffects;
    public Image[] combometers;
    public int[] combos;//full level0-12
    public int[] comboLevels;//0-3
    public float lerpAggro = 0.25f;

    public 
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Reticule Pivot");
        man = transform.parent.GetComponentInChildren<Character_Sprite_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < combometers.Length; i++)
            combometers[i].fillAmount = Mathf.Lerp(combometers[i].fillAmount, combos[i] / 12f, lerpAggro);
    }

    public void Shoot(bool beat)
    {
        weaponIndex = man.weapon;
        if (beat)
        {
            combos[weaponIndex] = MinMin(12, combos[weaponIndex]+1); //increase by 1 max 12
        }
        else
        {
            combos[weaponIndex] = MaxMax(((combos[weaponIndex] - 1) / 4) * 4, 0); //reduce to the next highest multiple of 4
        }
        fixLevels();//move this if want to change when combo update happen
        GameObject shot;
        if (beat)
        {
            shot = Instantiate(beatBullet[(int)weaponIndex], transform.position, transform.rotation, transform);
            BulletInfo bulletDeets;
            if (shot.TryGetComponent<BulletInfo>(out bulletDeets))
            {
                bulletDeets.speed = beatShotSpeed[(int)weaponIndex] * comboEffects[weaponIndex * 4 + comboLevels[0]];
                bulletDeets.damage = comboEffects[weaponIndex * 4 + comboLevels[1]]; //if extra on beat damage add that here
                bulletDeets.scale = comboEffects[weaponIndex * 4 + comboLevels[2]];
            }
            shot.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.Cross(target.transform.position - shot.transform.position, Vector3.up));

        }
        else
        {
            shot = Instantiate(bullet[(int)weaponIndex], transform.position, transform.rotation, transform);
            BulletInfo bulletDeets;
            if (shot.TryGetComponent<BulletInfo>(out bulletDeets))
            {
                bulletDeets.speed = beatShotSpeed[(int)weaponIndex] * comboEffects[weaponIndex * 4 + comboLevels[0]];
                bulletDeets.damage = comboEffects[weaponIndex * 4 + comboLevels[1]];
                bulletDeets.scale = comboEffects[weaponIndex * 4 + comboLevels[2]];
            }
            shot.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.Cross(target.transform.position - shot.transform.position, Vector3.up));
        }
    }

    private void fixLevels()
    {
        for(int i = 0; i < comboLevels.Length; i++)
        {
            comboLevels[i] = combos[i] / 4;
        }
    }

    private int MinMin(int v1, int v2)
    {
        if (v1 < v2)
        {
            return v1;
        }
        return v2;
    }
    private int MaxMax(int v1, int v2)
    {
        if (v1 < v2)
        {
            return v2;
        }
        return v1;
    }
}
