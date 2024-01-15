using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotionSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> potionsPrefab;
    [SerializeField] private Vector2 spawnZone;
    private Transform parent;
    private GameObject player;

    private void Awake()
    {
        parent = GameObject.Find("Potions").GetComponent<Transform>();
        player = GameObject.Find("Player");
    }

    void Start()
    {
        InvokeRepeating("SpawnPotionR", 0f, 5f);
        InvokeRepeating("SpawnPotionV", 0f, 15f);
    }

    void Update()
    {
        transform.position = player.transform.position;
    }

    void SpawnPotionR()
    {
        GameObject instantiated = Instantiate(potionsPrefab[0], parent);

        instantiated.transform.position = new Vector3(
            Random.Range(transform.position.x - spawnZone.x / 2, transform.position.x + spawnZone.x / 2),
            Random.Range(transform.position.y - spawnZone.y / 2, transform.position.y + spawnZone.y / 2)
                );
    }

    void SpawnPotionV()
    {
        GameObject instantiated = Instantiate(potionsPrefab[1], parent);

        instantiated.transform.position = new Vector3(
            Random.Range(transform.position.x - spawnZone.x / 2, transform.position.x + spawnZone.x / 2),
            Random.Range(transform.position.y - spawnZone.y / 2, transform.position.y + spawnZone.y / 2)
            );
    }

    void SpawnPotionB()
    {
        GameObject instantiated = Instantiate(potionsPrefab[2], parent);
        instantiated.transform.position = new Vector3(
            Random.Range(transform.position.x - spawnZone.x / 2, transform.position.x + spawnZone.x / 2),
            Random.Range(transform.position.y - spawnZone.y / 2, transform.position.y + spawnZone.y / 2)
             );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, spawnZone);
    }


}
