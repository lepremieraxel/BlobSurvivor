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
