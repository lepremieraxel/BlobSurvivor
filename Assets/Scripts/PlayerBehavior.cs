/* Author : Raphaël Marczak - 2018/2020, for MIAMI Teaching (IUT Tarbes) and MMI Teaching (IUT Bordeaux Montaigne)
 * 
 * This work is licensed under the CC0 License. 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents the cardinal directions (South, North, West, East)
public enum CardinalDirections { CARDINAL_S, CARDINAL_N, CARDINAL_W, CARDINAL_E };

public class PlayerBehavior : MonoBehaviour
{
    public float m_speed = 0.1f; // Speed of the player when he moves
    private CardinalDirections m_direction; // Current facing direction of the player
    public int exp = 0; //experience of the player
    
    public Sprite m_frontSprite = null;
    public Sprite m_leftSprite = null;
    public Sprite m_rightSprite = null;
    public Sprite m_backSprite = null;

    public GameObject m_map = null;
    public bool invincible = false;
    public float attackTime; 
    public float startTimeAttack;
    public float invincibilityDeltaTime;
    Rigidbody2D m_rb2D;
    SpriteRenderer m_renderer;
    [SerializeField]
    private GameObject luciole;
    public int enemyKilled;

    void Awake()
    {
        m_rb2D = gameObject.GetComponent<Rigidbody2D>();
        m_renderer = gameObject.GetComponent<SpriteRenderer>();

       
    }

    // This update is called at a very precise and constant FPS, and
    // must be used for physics modification
    // (i.e. anything related with a RigidBody)
    void FixedUpdate()
    {
             // Moves the player regarding the inputs
        Move();
        ChangeSpriteToMatchDirection();
    }

    private void Move()
    {
        float horizontalOffset = Input.GetAxis("Horizontal");
        float verticalOffset = Input.GetAxis("Vertical");

        // Translates the player to a new position, at a given speed.
        Vector2 newPos = new Vector2(transform.position.x + horizontalOffset * m_speed,
                                     transform.position.y + verticalOffset * m_speed);
        m_rb2D.MovePosition(newPos);

        // Computes the player main direction (North, Sound, East, West)
        if (Mathf.Abs(horizontalOffset) > Mathf.Abs(verticalOffset))
        {
            if (horizontalOffset > 0)
            {
                m_direction = CardinalDirections.CARDINAL_E;
            }
            else
            {
                m_direction = CardinalDirections.CARDINAL_W;
            }
        }
        else if (Mathf.Abs(horizontalOffset) < Mathf.Abs(verticalOffset))
        {
            if (verticalOffset > 0)
            {
                m_direction = CardinalDirections.CARDINAL_N;
            }
            else
            {
                m_direction = CardinalDirections.CARDINAL_S;
            }
        }
    }


    // This update is called at the FPS which can be fluctuating
    // and should be called for any regular actions not based on
    // physics (i.e. everything not related to RigidBody)
    private void Update()
    {
        attackTime -= Time.deltaTime;
    }

    // Changes the player sprite regarding it position
    // (back when going North, front when going south, right when going east, left when going west)
    private void ChangeSpriteToMatchDirection()
    {
        if (m_direction == CardinalDirections.CARDINAL_N)
        {
            m_renderer.sprite = m_backSprite;
        }
        else if (m_direction == CardinalDirections.CARDINAL_S)
        {
            m_renderer.sprite = m_frontSprite;
        }
        else if (m_direction == CardinalDirections.CARDINAL_E)
        {
            m_renderer.sprite = m_rightSprite;
        }
        else if (m_direction == CardinalDirections.CARDINAL_W)
        {
            m_renderer.sprite = m_leftSprite;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PotionB")
        {
             m_speed = m_speed + 0.01f;
            Destroy(other.gameObject);
            Debug.Log("You're faster !");
        }
        if (other.gameObject.tag == "exp")
        {
            exp++;
            Destroy(other.gameObject);
            Debug.Log("You gain 1 exp !");
        }

        if(other.gameObject.tag == "Arme")
        {
            startTimeAttack--;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "enemy" && attackTime <= 0)
        {
            StartCoroutine(Attack());
            Instantiate(luciole, other.gameObject.transform.position, Quaternion.identity);
            enemyKilled++;
        }   
            
        IEnumerator Attack()
        {
            invincible = true;

            // Or any more specific condition
            for (float i = 0; i < invincibilityDeltaTime; i ++)
            {
                
                Destroy(other.gameObject);
                Debug.Log("Enemy killed");
                attackTime = startTimeAttack;
                
                yield return new WaitForSeconds(i);
               
            }
            invincible = false;
            //Debug.Log(invincible);
        }

    }
}

