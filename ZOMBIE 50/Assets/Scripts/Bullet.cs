using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public GameObject impact;
    public GameObject flare;
    private int[] damages = new int[3] {10, 8, 40};
    public void Start()
    {
        int selectedWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<GunManager>().selectedWeapon;
        damage = damages[selectedWeapon];

        switch (selectedWeapon)
        {
            case 0:
                FindObjectOfType<AudioManager>().Play("Pistol");
            break;
            case 1:
                FindObjectOfType<AudioManager>().Play("MachineGun");
            break;
            case 2:
                FindObjectOfType<AudioManager>().Play("ShotGun");
            break;
        }

        Instantiate(flare, transform.position, transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Instantiate(impact, transform.position, transform.rotation);

        if (other.collider.CompareTag("Zombie"))
        {
            other.collider.GetComponent<Zombie>().Damage(damage);
        }

        Destroy(gameObject);
    }
}


