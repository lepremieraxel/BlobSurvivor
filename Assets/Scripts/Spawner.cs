using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] 
    private GameObject Blob;

    [SerializeField]
    private GameObject BlobShooter;

    [SerializeField]
    private GameObject BlobSpeeder;

    [SerializeField]
    private Vector3 zoneSize;


    // Update is called once per frame
    void Start()
    {
        InvokeRepeating("SpawnPrefab1", 0f, 5f);
        InvokeRepeating("SpawnPrefab2", 10f, 15f);
        InvokeRepeating("SpawnPrefab3", 20f, 10f);
    }

    void SpawnPrefab1()
    {
        GameObject instantiated = Instantiate(Blob);

        instantiated.transform.position = new Vector3(
            Random.Range(transform.position.x - zoneSize.x / 2, transform.position.x + zoneSize.x / 2),
            Random.Range(transform.position.y - zoneSize.y / 2, transform.position.y + zoneSize.y / 2)
                );
    }

    void SpawnPrefab2()
    {
        GameObject instantiated = Instantiate(BlobShooter);

           instantiated.transform.position = new Vector3(
               Random.Range(transform.position.x - zoneSize.x /2, transform.position.x + zoneSize.x /2),
               Random.Range(transform.position.y - zoneSize.y / 2, transform.position.y + zoneSize.y / 2)
               );
    }

    void SpawnPrefab3()
    {
        GameObject instantiated = Instantiate(BlobSpeeder);
           instantiated.transform.position = new Vector3(
               Random.Range(transform.position.x - zoneSize.x / 2, transform.position.x + zoneSize.x / 2),
               Random.Range(transform.position.y - zoneSize.y / 2, transform.position.y + zoneSize.y / 2)
                );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, zoneSize);
    }


}
