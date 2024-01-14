using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private float multiplier = 0.01f;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float invincibleTime = 1f;
    private bool isInvicible = false;
    private int heathPoint = 10;
    private int maxHealth = 10;
    private int xp = 0;
    private int level = 0;
    private Text hpText;
    private Text levelText;
    private Text xpText;

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
        levelText = GameObject.Find("Level").GetComponent<Text>();
        xpText = GameObject.Find("XP").GetComponent<Text>();
        parent = GameObject.Find("Enemies").GetComponent<Transform>();
    }

    void Start()
    {
        hpText.text = "HP : "+heathPoint.ToString()+"/"+maxHealth.ToString();
        levelText.text = "Level : "+level.ToString();
        xpText.text = "XP : "+xp.ToString();
        gameOverScreen.SetActive(false);
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        animator.SetFloat("inputX", inputX);
        animator.SetFloat("inputY", inputY);

        movement = new Vector2(speed * inputX * multiplier, speed * inputY * multiplier);

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
            StartCoroutine(SetInvincible());
            heathPoint -= 1;
            hpText.text = "HP : "+heathPoint.ToString()+"/"+maxHealth.ToString();
            gameManager.SavePlayerHealth(DateTimeOffset.Now.ToUnixTimeSeconds(), heathPoint, maxHealth);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(SetInvincible());
            heathPoint -= 1;
            hpText.text = "HP : " + heathPoint.ToString() + "/" + maxHealth.ToString();
            gameManager.SavePlayerHealth(DateTimeOffset.Now.ToUnixTimeSeconds(), heathPoint, maxHealth);
            Destroy(collision.gameObject);
        }
    }

    IEnumerator SetInvincible()
    {
        if(!isInvicible)
        {
            isInvicible = true;
            yield return new WaitForSeconds(invincibleTime);
            isInvicible = false;
        }
    }
}
