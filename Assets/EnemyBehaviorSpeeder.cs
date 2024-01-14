using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorSpeeder : MonoBehaviour
{
    public Transform player; // R�f�rence au transform du joueur
    public float moveSpeed = 2f; // Vitesse de d�placement de l'ennemi

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        // D�placement de l'ennemi vers le joueur
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }
}
