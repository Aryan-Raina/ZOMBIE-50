using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMain : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int maxHealth = 100;
    private int currentHealth;
    public Rigidbody2D rb;

    public GameObject blood;
    public HealthBar healthBar;
    public AudioManager audioManager;
    [HideInInspector]
    public int medKits = 0;
    public TMP_Text medKitUI;


    Vector2 movement;
    Vector2 mousePos;

    private void Start() 
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (currentHealth <= 0)
        {
            Debug.Log("RESPAWN");
            SceneManager.LoadScene("DeathScene");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            UseMedKit();
        }
    }

    private void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        Instantiate(blood, transform.position, Quaternion.identity);
    }

    private void UseMedKit()
    {
        if (medKits < 1)
            return;
        medKits -= 1;
        audioManager.Play("Heal");
        medKitUI.text = "* " + medKits.ToString();
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth); 
    }
}
