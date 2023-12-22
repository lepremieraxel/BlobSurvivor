using Unity.VisualScripting;
using UnityEngine;

public class PotionSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject potionR;

    [SerializeField]
    private GameObject potionV;

    [SerializeField]
    private GameObject potionB;

    [SerializeField]
    private Vector3 zoneSize;


    // Update is called once per frame
    void Start()
    {
        InvokeRepeating("SpawnPotion1", 0f, 5f);
        InvokeRepeating("SpawnPotion2", 0f, 15f);
        InvokeRepeating("SpawnPotion3", 0f, 20f);
    }

    void SpawnPotion1()
    {
        GameObject instantiated = Instantiate(potionR);

        instantiated.transform.position = new Vector3(
            Random.Range(transform.position.x - zoneSize.x / 2, transform.position.x + zoneSize.x / 2),
            Random.Range(transform.position.y - zoneSize.y / 2, transform.position.y + zoneSize.y / 2)
                );
    }

    void SpawnPotion2()
    {
        GameObject instantiated = Instantiate(potionV);

        instantiated.transform.position = new Vector3(
            Random.Range(transform.position.x - zoneSize.x / 2, transform.position.x + zoneSize.x / 2),
            Random.Range(transform.position.y - zoneSize.y / 2, transform.position.y + zoneSize.y / 2)
            );
    }

    void SpawnPotion3()
    {
        GameObject instantiated = Instantiate(potionB);
        instantiated.transform.position = new Vector3(
            Random.Range(transform.position.x - zoneSize.x / 2, transform.position.x + zoneSize.x / 2),
            Random.Range(transform.position.y - zoneSize.y / 2, transform.position.y + zoneSize.y / 2)
             );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, zoneSize);
    }


}
