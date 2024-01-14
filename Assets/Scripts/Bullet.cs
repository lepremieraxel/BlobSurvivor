using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f; // Vitesse du projectile
    public float lifespan = 3f; // Dur�e de vie du projectile en secondes

    void Start()
    {
        StartCoroutine(DestroyAfterDelay(3f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        // Attendre pendant le d�lai sp�cifi�
        yield return new WaitForSeconds(delay);

        // D�truire le GameObject apr�s le d�lai
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        // G�rer la collision avec d'autres objets
        if (other.CompareTag("Player"))
        {
            // D�truire le projectile lorsqu'il touche le joueur
            Destroy(gameObject);
        }
    }
}
