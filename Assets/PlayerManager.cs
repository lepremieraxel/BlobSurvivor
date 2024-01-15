using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private float augmentedSpeed = 14f;
    private float currentSpeed;
    private float multiplier = 0.01f;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float speedAugmentationDuration = 2f;
    private int heathPoint = 10;
    private int maxHealth = 10;
    private Text hpText;
    private bool isAttacking = false;
    [SerializeField]  private GameObject swordR;
    [SerializeField] private GameObject swordL;

    [SerializeField] private Sprite forward;
    [SerializeField] private Sprite backward;
    [SerializeField] private Sprite left;
    [SerializeField] private Sprite right;
    private Sprite lastSprite;

    private GameManager gameManager;
    private GameObject gameOverScreen;
    private Transform parent;
    private bool isSend = false;
    void Awake()
    {
        gameOverScreen = GameObject.Find("GameOverScreen");
        lastSprite = forward;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        hpText = GameObject.Find("HP").GetComponent<Text>();
        parent = GameObject.Find("Enemies").GetComponent<Transform>();
    }

    void Start()
    {
        hpText.text = "HP : "+heathPoint.ToString()+"/"+maxHealth.ToString();
        gameOverScreen.SetActive(false);
        currentSpeed = speed;
        swordR.SetActive(false);
        swordL.SetActive(false);
        StartCoroutine(Attack());
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        animator.SetFloat("inputX", inputX);
        animator.SetFloat("inputY", inputY);

        movement = new Vector2(currentSpeed * inputX * multiplier, currentSpeed * inputY * multiplier);

        switch (inputX)
        {
            case 0:
                spriteRenderer.sprite = lastSprite;
                break;
            case 1:
                spriteRenderer.sprite = right;
                lastSprite = right;
                break;
            case -1:
                spriteRenderer.sprite = left;
                lastSprite = left;
                break;
            default:
                spriteRenderer.sprite = lastSprite;
                break;
        }
        switch (inputY)
        {
            case 0:
                spriteRenderer.sprite = lastSprite;
                break;
            case 1:
                spriteRenderer.sprite = backward;
                lastSprite = backward;
                break;
            case -1:
                spriteRenderer.sprite = forward;
                lastSprite = forward;
                break;
            default:
                spriteRenderer.sprite = lastSprite;
                break;
        }
        if(heathPoint <= 0 && !isSend)
        {
            gameOverScreen.SetActive(true);
            gameManager.SaveEndGame(gameManager.startTimestamp, DateTimeOffset.Now.ToUnixTimeSeconds());
            while (parent.transform.childCount > 0)
            {
                DestroyImmediate(parent.transform.GetChild(0).gameObject);
            }
            StartCoroutine(gameManager.SendMetricsToWebService());
            isSend = true;
        }
    }

    void FixedUpdate()
    {
       transform.Translate(movement);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(isAttacking)
            {
                Destroy(collision.gameObject);
                gameManager.SaveEnemyKill(DateTimeOffset.Now.ToUnixTimeSeconds(), collision.gameObject.name, "sword");
            } else
            {
                heathPoint -= 1;
                hpText.text = "HP : " + heathPoint.ToString() + "/" + maxHealth.ToString();
                gameManager.SavePlayerHealth(DateTimeOffset.Now.ToUnixTimeSeconds(), heathPoint, maxHealth);
            }
        }

        if (collision.gameObject.CompareTag("PotionR"))
        {
            Destroy(collision.gameObject);
            if(heathPoint < maxHealth)
            {
                heathPoint += 1;
            }
            hpText.text = "HP : " + heathPoint.ToString() + "/" + maxHealth.ToString();
            gameManager.SavePlayerHealth(DateTimeOffset.Now.ToUnixTimeSeconds(), heathPoint, maxHealth);
            gameManager.SavePlayerPowerup(DateTimeOffset.Now.ToUnixTimeSeconds(), "health");
        }

        if (collision.gameObject.CompareTag("PotionV"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(SetSpeed());
            gameManager.SavePlayerPowerup(DateTimeOffset.Now.ToUnixTimeSeconds(), "speed");
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isAttacking)
            {
                Destroy(collision.gameObject);
                gameManager.SaveEnemyKill(DateTimeOffset.Now.ToUnixTimeSeconds(), collision.gameObject.name, "sword");
            }
            else
            {
                heathPoint -= 1;
                hpText.text = "HP : " + heathPoint.ToString() + "/" + maxHealth.ToString();
                gameManager.SavePlayerHealth(DateTimeOffset.Now.ToUnixTimeSeconds(), heathPoint, maxHealth);
                Destroy(collision.gameObject);
            }
        }
    }

    IEnumerator SetSpeed()
    {
        currentSpeed = augmentedSpeed;
        yield return new WaitForSeconds(speedAugmentationDuration);
        currentSpeed = speed;
    }

    IEnumerator Attack()
    {   
        while (true)
        {
            swordL.SetActive(true);
            swordR.SetActive(true);
            isAttacking = true;
            yield return new WaitForSeconds(1f);
            swordL.SetActive(false);
            swordR.SetActive(false);
            isAttacking = false;
            yield return new WaitForSeconds(2f);
        }
    }
}
