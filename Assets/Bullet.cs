using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f; // Vitesse du projectile
    public float lifespan = 5f; // Durée de vie du projectile en secondes

    void Start()
    {
        // Détruire le projectile après un certain temps pour éviter les fuites mémoire
        Destroy(gameObject, lifespan);
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
