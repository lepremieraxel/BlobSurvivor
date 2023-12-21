using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f; // Vitesse du projectile
    public float lifespan = 5f; // Dur�e de vie du projectile en secondes

    void Start()
    {
        // D�truire le projectile apr�s un certain temps pour �viter les fuites m�moire
        Destroy(gameObject, lifespan);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // G�rer la collision avec d'autres objets
        if (collision.collider.CompareTag("Player"))
        {
            // D�truire le projectile lorsqu'il touche le joueur
            Destroy(gameObject);
        }
    }
}
