using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorSpeeder : MonoBehaviour
{
    public Transform player; // Référence au transform du joueur
    public float moveSpeed = 2f; // Vitesse de déplacement de l'ennemi

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        // Déplacement de l'ennemi vers le joueur
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }
}
