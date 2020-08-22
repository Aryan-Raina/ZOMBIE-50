using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    private float[] fireRate = new float[3] {3f, 15f, 1f};
    private float bulletForce = 17f;
    private int[] maxAmmo = new int[3] {25, 100, 25};
    private float[] reloadTime = new float[3] {3f, 12f, 3f};
    private int[] currentAmmo = new int[3] {25, 100, 25};
    [HideInInspector]
    public int[] totalAmmo = new int[3] {200, 200, 60};
    [HideInInspector]
    public bool shoping = false;

    private CameraShake cameraShake;
    public Transform firePoint;
    public GameObject bulletPrefab;
    [HideInInspector]
    public bool isReloading = false;
    [HideInInspector]
    public int selectedWeapon;

    public TMP_Text ammoUI;
    float timer = 0;
    GunManager gunManager;

    void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        gunManager = GetComponent<GunManager>();
        ammoUI.text = currentAmmo[selectedWeapon].ToString() + " / " + totalAmmo[selectedWeapon].ToString();
    }

    private void Update() 
    {
        timer += Time.deltaTime;

        if (isReloading)
        {
            return;
        }

        if (currentAmmo[selectedWeapon] <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButton(0) && timer >= 1f/fireRate[selectedWeapon] && !shoping)
        {
            Shoot();
            timer = 0;
        }  
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        currentAmmo[selectedWeapon]--;
        ammoUI.text = currentAmmo[selectedWeapon].ToString() + " / " + totalAmmo[selectedWeapon].ToString();

        if (selectedWeapon == 2)
        {
            StartCoroutine(cameraShake.Shake(.3f, .2f));
        }
    }

    public void GunChanged()
    {
        selectedWeapon = gunManager.selectedWeapon;
        ammoUI.text = currentAmmo[selectedWeapon].ToString() + " / " + totalAmmo[selectedWeapon].ToString();
    }

    IEnumerator Reload()
    {
        if (totalAmmo[selectedWeapon] <= 0)
            yield break;
        isReloading = true;
        ammoUI.text = "Reloading...";

        switch (selectedWeapon)
        {
            case 0:
                FindObjectOfType<AudioManager>().Play("PistolReload");
            break;
            case 1:
                FindObjectOfType<AudioManager>().Play("MachineGunReload");
            break;
            case 2:
                FindObjectOfType<AudioManager>().Play("ShotGunReload");
            break;
        }

        int reload_amount = maxAmmo[selectedWeapon];
        if (totalAmmo[selectedWeapon] < maxAmmo[selectedWeapon] - currentAmmo[selectedWeapon])
            reload_amount = totalAmmo[selectedWeapon];

        totalAmmo[selectedWeapon] += -reload_amount + currentAmmo[selectedWeapon];

        yield return new WaitForSeconds(reloadTime[selectedWeapon]);

        currentAmmo[selectedWeapon] = reload_amount;
        ammoUI.text = currentAmmo[selectedWeapon].ToString() + " / " + totalAmmo[selectedWeapon].ToString();
        isReloading = false;
    }
}
