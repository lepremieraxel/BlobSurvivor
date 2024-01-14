using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonManager : MonoBehaviour
{
    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Clique()
    {
        gameManager.SendMetricsToWebService();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
