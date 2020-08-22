using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int attack;
    public int maxHealth;
    private int currentHealth;
    public float moveSpeed;
    private Transform target;
    public GameObject blood;
    public GameObject groundBreak;
    Rigidbody2D rb;
    Vector2 move;
    CameraShake cameraShake;
    bool attacking;
    bool active;
    string type;
    AudioManager audioManager;
    Shop shop;
    int rewardMoney;

    private void Awake()
    {
        active = false;
        SpriteRenderer skin = this.GetComponent<SpriteRenderer>();
        audioManager = FindObjectOfType<AudioManager>();

        if (Random.Range(1, 80) == 1)
        {
            // Boss
            type = "boss";
            attack = 25;
            maxHealth = 500;
            moveSpeed = 1f;
            rewardMoney = Random.Range(15, 50);
        }
        else if (Random.Range(1, 20) == 1)
        {
            // Fast
            type = "fast";
            attack = 3;
            maxHealth = 30;
            moveSpeed = 3.2f;
            skin.color = Color.yellow;
            rewardMoney = Random.Range(3, 8);
        }
        else if (Random.Range(1, 10) == 1)
        {
            // Strong
            type = "strong";
            attack = 20;
            maxHealth = 20;
            moveSpeed = 2f;
            skin.color = Color.red;
            rewardMoney = Random.Range(3, 8);
        }
        else
        {
            // Normal
            type = "normal";
            attack = 5;
            maxHealth = 30;
            moveSpeed = 1.5f;
            rewardMoney = Random.Range(1, 5);
        }


        Instantiate(groundBreak, transform.position, Quaternion.identity);
        StartCoroutine(BreakOutOfGround());
    }

    private void Start() 
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        cameraShake = Camera.main.GetComponent<CameraShake>();
        shop = FindObjectOfType<Shop>();
    }
    private void Update()
    {
        if (currentHealth <= 0)
        {
            Instantiate(blood, transform.position, Quaternion.identity);
            shop.ChangeMoney(rewardMoney);
            Camera.main.GetComponent<CameraFollow>().enabled = true;
            Destroy(gameObject);
        }

        move = target.position - transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.tag == "Player")
        {
            attacking = true;
            StartCoroutine(Attack(other.collider));
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        attacking = false;
    }
    
    private void FixedUpdate() 
    {
        float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        if (active)
        {
            move.Normalize();
            rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);  
        }
        
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        if (Random.Range(1, 4) == 1)
            audioManager.Play("ZombieMoan");
        StartCoroutine(cameraShake.Shake(.15f, .12f));
    }

    IEnumerator Attack(Collider2D player)
    {
        while (attacking)
        {
            player.GetComponent<PlayerMain>().Damage(attack);
            if (Random.Range(1, 4) == 1)
                audioManager.Play("ZombieAttack");
            yield return new WaitForSeconds(2.2f);
        }   
    }

    IEnumerator BreakOutOfGround()
    {
        float timer = 0f;
        while (timer < 4)
        {
            transform.localScale = new Vector3(timer/4, timer/4, 1);
            timer += Time.deltaTime;

            yield return null;
        }

        transform.localScale = new Vector3(1, 1, 1);

        if (type == "boss")
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
        active = true;
    }
}
