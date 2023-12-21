using UnityEngine;

public class EnemyBehaviorShooter : MonoBehaviour
{
    public Transform player; // Référence au transform du joueur
    public float moveSpeed = 2f; // Vitesse de déplacement de l'ennemi
    public GameObject bulletPrefab; // Prefab du projectile
    public float fireRate = 1f; // Taux de tir en secondes
    private float nextFireTime = 1f; // Temps du prochain tir
    public float speed = 5f; // Vitesse du projectile

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Update()
    {
        // Déplacement de l'ennemi vers le joueur
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);


        // Tir de l'ennemi
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        // Instanciation du projectile au point de tir
        GameObject missile = Instantiate(bulletPrefab, transform.position, transform.rotation);
        missile.GetComponent<Rigidbody2D>().velocity = player.transform.position - transform.position;
    }
}
