using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public Sprite[] weapons;
    public SpriteRenderer player;
    public int selectedWeapon = 0;
    public bool[] haveGun = new bool[3] {true, false, false};

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        bool condition = !(this.GetComponent<Gun>().isReloading);
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1) && condition && haveGun[0])
        
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && condition && haveGun[1])
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && condition && haveGun[2])
        {
            selectedWeapon = 2;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Sprite weapon in weapons)
        {
            if (i == selectedWeapon)
            {
                player.sprite = weapon;
            }
            i++;
        }

        Gun g = gameObject.GetComponent<Gun>();
        g.GunChanged();
    }
}
