using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public PlayerBehavior playerBehavior;
    public int damagesTaken = 0;
    [SerializeField]
    private GameObject dieScreen;

    //public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
       // healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void TakeDamage(int damage){
        currentHealth = currentHealth - damage;
        Debug.Log("You took 1 damage");
        damagesTaken++;
        // healthBar.SetHealth(currentHealth);
        // OnTriggerEnter2D(other.gameObject.CompareTag);
    }

 
    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("collision"+ playerBehavior.invincible);
        if ((other.gameObject.tag == "enemy" || other.gameObject.tag == "bullet") && playerBehavior.invincible == false)
        {            
                TakeDamage(1);
                
                Death();
        }
        if (other.gameObject.tag == "PotionR" && currentHealth != maxHealth)
        {
            currentHealth++;
            Destroy(other.gameObject);
            Debug.Log("You've been healed !");
        }
        if (other.gameObject.tag == "PotionV")
        {
            maxHealth++;
            currentHealth++;
            Destroy(other.gameObject);
            Debug.Log("You gain 1 max health");
        }
    }

    public void Death(){
        if(currentHealth == 0){
            Debug.Log("You are bad haha noob");
            Instantiate(dieScreen, transform.position, Quaternion.identity);
        }
    }
    
}
