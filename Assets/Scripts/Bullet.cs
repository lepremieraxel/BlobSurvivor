using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f; // Vitesse du projectile
    public float lifespan = 3f; // Durée de vie du projectile en secondes

    void Start()
    {
        StartCoroutine(DestroyAfterDelay(3f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        // Attendre pendant le délai spécifié
        yield return new WaitForSeconds(delay);

        // Détruire le GameObject après le délai
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        // Gérer la collision avec d'autres objets
        if (other.CompareTag("Player"))
        {
            // Détruire le projectile lorsqu'il touche le joueur
            Destroy(gameObject);
        }
    }
}
