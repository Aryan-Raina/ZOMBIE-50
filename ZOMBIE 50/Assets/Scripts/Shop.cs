using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public PlayerMain player;
    public GameObject ShopUI;
    public GunManager gunManager;
    public Gun gun;
    public int money;
    public TMP_Text medKitUI;
    public Button[] buyButtons; 
    public TMP_Text moneyUI;
    private int[] cost = new int[3] {0, 600, 250};
    int medKitCost = 100;
    public AudioManager audioManager;
    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.Space))
        {
            ShopUI.SetActive(true);
            gun.shoping = true;
            Time.timeScale = 0f;
        }
    }

    private void Start() 
    {
        PlayerPrefs.SetInt("money", money);
    }

    public void Back()
    {
        ShopUI.SetActive(false);
        gun.shoping = false;
        Time.timeScale = 1f;
    }

    public void BuyGun(int gunNumber)
    {
        if (money < cost[gunNumber])
        {
            Error();
            return;
        }

        gunManager.haveGun[gunNumber] = true;
        ChangeMoney(-cost[gunNumber]);
        audioManager.Play("Buy");
        Disable(gunNumber - 1);
    }

    public void BuyAmmo(int gunNumber)
    {
        if (money < 10)
        {
            Error();
            return;
        }
        gun.totalAmmo[gunNumber] += 50;
        ChangeMoney(-10);
        if (gunNumber == gun.selectedWeapon)
        {
            gun.GunChanged();
        } 
    }

    public void BuyMedKit()
    {
        if (money < medKitCost)
        {
            Error();
            return;
        }
        player.medKits += 1;
        medKitUI.text = "* " + player.medKits.ToString();
        ChangeMoney(-medKitCost);
    }

    private void Disable(int buttonNumber)
    {
        buyButtons[buttonNumber].interactable = false;
    }

    public void ChangeMoney(int change)
    {
        money += change;
        moneyUI.text = "$" + money.ToString();
        PlayerPrefs.SetInt("money", money);
    }

    void Error()
    {
        Debug.Log("Not Enough Money!");
        audioManager.Play("Error");
    }
}
