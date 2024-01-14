using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 spawnZone = new Vector2(15, 10);
    private GameObject player;
    [SerializeField] private List<GameObject> enemiesPrefab;
    private GameManager gameManager;
    private Transform parent;

    private void Awake()
    {
        parent = GameObject.Find("Enemies").GetComponent<Transform>();
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Start()
    {
        InvokeRepeating("SpawnBlob", 2f, 5f);
        InvokeRepeating("SpawnShooter", 20f, 10f);
        InvokeRepeating("SpawnSpeeder", 10f, 15f);
    }
    void Update()
    {
        transform.position = player.transform.position;
    }

    private void SpawnBlob()
    {
        GameObject instance = Instantiate(enemiesPrefab[0], parent);
        string name = "vert";
        instance.transform.position = new Vector2(Random.Range(transform.position.x - spawnZone.x / 2, transform.position.x + spawnZone.x / 2), Random.Range(transform.position.y - spawnZone.y / 2, transform.position.y + spawnZone.y / 2));
        gameManager.SaveEnemySpawn(DateTimeOffset.Now.ToUnixTimeSeconds(), name);
    }
    private void SpawnShooter()
    {
        GameObject instance = Instantiate(enemiesPrefab[1], parent);
        string name = "rouge";
        instance.transform.position = new Vector2(Random.Range(transform.position.x - spawnZone.x / 2, transform.position.x + spawnZone.x / 2), Random.Range(transform.position.y - spawnZone.y / 2, transform.position.y + spawnZone.y / 2));
        gameManager.SaveEnemySpawn(DateTimeOffset.Now.ToUnixTimeSeconds(), name);
    }
    private void SpawnSpeeder()
    {
        GameObject instance = Instantiate(enemiesPrefab[2], parent);
        string name = "violet";
        instance.transform.position = new Vector2(Random.Range(transform.position.x - spawnZone.x / 2, transform.position.x + spawnZone.x / 2), Random.Range(transform.position.y - spawnZone.y / 2, transform.position.y + spawnZone.y / 2));
        gameManager.SaveEnemySpawn(DateTimeOffset.Now.ToUnixTimeSeconds(), name);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnZone);
    }
}
